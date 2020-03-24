/* Author:          ezhex1991@outlook.com
 * CreateTime:      2017-01-16 16:16:01
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using XLua;

namespace EZhex1991.XLuaExtension
{
    public delegate LuaTable LuaRequire(string moduleName);
    public delegate void LuaAction();
    public delegate void LuaAction<T>(T arg);
    public delegate AssetBundle BundleSource(string bundleName);

    public class EZLuaManager : EZMonoBehaviourSingleton<EZLuaManager>
    {
        private EZLuaSettings ezLuaSettings { get { return EZLuaSettings.Instance; } }
        public event BundleSource BundleSourceLocal;
        public event BundleSource BundleSourceRemote;

        private static LuaEnv m_LuaEnv;
        public LuaEnv luaEnv
        {
            get
            {
                if (m_LuaEnv == null)
                {
                    m_LuaEnv = new LuaEnv();
                    //luaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
                    switch (ezLuaSettings.luaSourceMode)
                    {
                        case LuaSourceMode.Develop:
                            m_LuaEnv.AddLoader(LoadLuaFromFile);
                            break;
                        case LuaSourceMode.Local:
                            m_LuaEnv.AddLoader(LoadLuaFromBundle);
                            break;
                        case LuaSourceMode.Remote:
                            m_LuaEnv.AddLoader(LoadLuaFromBundle);
                            break;
                    }
                }
                return m_LuaEnv;
            }
        }

        private LuaRequire m_LuaRequire;
        public LuaRequire luaRequire
        {
            get
            {
                if (m_LuaRequire == null)
                {
                    m_LuaRequire = luaEnv.Global.Get<LuaRequire>("require");
                }
                return m_LuaRequire;
            }
        }

        // require大小写敏感而文件路径大小写不敏感，还有点"."和斜线"/"的混用，会造成重复加载等一系列问题
        // 这里提前把所有文件记录于Dictionary中，直接对CustomLoader传入的参数进行TryGetValue，保证lua侧require参数的统一
        private Dictionary<string, string> m_LuaFiles;
        private Dictionary<string, string> luaFiles
        {
            get
            {
                if (m_LuaFiles == null)
                {
                    m_LuaFiles = new Dictionary<string, string>();
                    for (int i = 0; i < ezLuaSettings.luaFolders.Length; i++)
                    {
                        string dir = Path.Combine(Application.dataPath, ezLuaSettings.luaFolders[i]);
                        try
                        {
                            IEnumerable<string> files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
                                .Where(path => path.EndsWith(".lua") || path.EndsWith(".lua.txt"));
                            foreach (string filePath in files)
                            {
                                string key = filePath.Substring(dir.Length + 1).Replace("\\", "/").Replace("/", ".").Replace(".lua.txt", "").Replace(".lua", "");
                                m_LuaFiles.Add(key, filePath);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning(e.Message);
                        }
                    }
                    if (m_LuaFiles.Count == 0)
                    {
                        Debug.LogErrorFormat(ezLuaSettings, "No lua file found, make sure you have your lua source configured in [EZhex1991 -> XLuaExtension -> LuaSettings]");
                    }
                }
                return m_LuaFiles;
            }
        }
        // 另外CustomLoader不能是异步方法，如果是从Bundle或zip文件加载，提前加载所有Lua代码相关资源既规避了异步写法，也能保证后续加载顺畅
        private Dictionary<string, TextAsset> m_LuaAssets;
        private Dictionary<string, TextAsset> luaAssets
        {
            get
            {
                if (m_LuaAssets == null)
                {
                    m_LuaAssets = new Dictionary<string, TextAsset>();
                    for (int i = 0; i < ezLuaSettings.luaBundles.Length; i++)
                    {
                        string bundleName = ezLuaSettings.luaBundles[i].ToLower();
                        AssetBundle bundle = null;

                        if (ezLuaSettings.luaSourceMode == LuaSourceMode.Local)
                        {
                            if (BundleSourceLocal != null)
                            {
                                bundle = BundleSourceLocal(bundleName);
                            }
                            if (bundle == null)
                            {
                                string bundlePath = Path.Combine(Application.streamingAssetsPath, bundleName);
                                bundle = AssetBundle.LoadFromFile(bundlePath);
                            }
                        }
                        else if (ezLuaSettings.luaSourceMode == LuaSourceMode.Remote)
                        {
                            // bundle = ezResources.LoadBundle(bundleName);
                            if (BundleSourceRemote != null)
                            {
                                bundle = BundleSourceRemote(bundleName);
                            }
                            if (bundle == null)
                            {
                                string bundlePath = Path.Combine(Application.persistentDataPath, bundleName);
                                bundle = AssetBundle.LoadFromFile(bundlePath);
                            }
                        }

                        if (bundle == null) continue;
                        TextAsset[] assets = bundle.LoadAllAssets<TextAsset>();
                        for (int j = 0; j < assets.Length; j++)
                        {
                            string key = assets[j].name.Substring(0, assets[j].name.Length - 4).Replace("__", ".");
                            m_LuaAssets.Add(key, assets[j]);
                        }
                    }
                    if (m_LuaAssets.Count == 0)
                    {
                        Debug.LogErrorFormat(ezLuaSettings, "No lua bundle found, make sure you have your lua source configured in [EZhex1991 -> XLuaExtension -> LuaSettings]");
                    }
                }
                return m_LuaAssets;
            }
        }

        private byte[] LoadLuaFromFile(ref string fileKey)
        {
            string filePath;
            if (luaFiles.TryGetValue(fileKey, out filePath))
            {
                try
                {
                    // File.ReadAllBytes返回值可能会带有BOM（0xEF，0xBB，0xBF），这会导致脚本加载出错（<\239>）
                    byte[] script = System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(filePath));
                    return script;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        private byte[] LoadLuaFromBundle(ref string fileKey)
        {
            TextAsset luaText;
            if (luaAssets.TryGetValue(fileKey, out luaText))
            {
                return luaText.bytes;
            }
            else
            {
                return null;
            }
        }

        void Update()
        {
            if (luaEnv != null)
            {
                luaEnv.Tick();
            }
        }
    }
}
