/* Author:          ezhex1991@outlook.com
 * CreateTime:      2018-10-10 14:51:50
 * Organization:    #ORGANIZATION#
 * Description:     
 */
#pragma warning disable 0414
using UnityEngine;

namespace EZhex1991.XLuaExtension
{
    public enum LuaSourceMode
    {
        Develop = 0,
        Local = 1,
        Remote = 2
    }

    public class EZLuaSettings : EZScriptableObjectSingleton<EZLuaSettings>
    {
        [SerializeField]
        private LuaSourceMode m_LuaSourceModeInEditor = LuaSourceMode.Develop;
        [SerializeField, Tooltip("Don't use 'Develop Mode' here.")]
        private LuaSourceMode m_LuaSourceMode = LuaSourceMode.Local;
        public LuaSourceMode luaSourceMode
        {
#if UNITY_EDITOR
            get { return m_LuaSourceModeInEditor; }
#else
            get { return m_LuaSourceMode; }
#endif
        }

        [SerializeField]
        private string[] m_LuaFolders = new string[] { "Script_Lua" };
        public string[] luaFolders { get { return m_LuaFolders; } set { m_LuaFolders = value; } }
        [SerializeField]
        private string[] m_LuaBundles = new string[] { "script_lua" };
        public string[] luaBundles { get { return m_LuaBundles; } set { m_LuaBundles = value; } }
    }
}
