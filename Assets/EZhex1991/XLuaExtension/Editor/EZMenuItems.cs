/* Author:          ezhex1991@outlook.com
 * CreateTime:      2017-01-06 10:44:41
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EZhex1991.XLuaExtension
{
    public static class EZMenuItems
    {
        private const string ROOT_NAME = "EZhex1991/XLuaExtension/";

        [MenuItem(ROOT_NAME + "Lua Settings", false, 18000)]
        private static void LuaSettings()
        {
            Selection.activeObject = EZLuaSettings.Instance;
        }

        // [EZhex1991.EZUnity.Builder.EZBundleBuilder.OnPreBuild]
        [MenuItem(ROOT_NAME + "'.lua' To '.txt'", false, 18001)]
        public static void LuaToTxt()
        {
            foreach (string dirPath in EZLuaSettings.Instance.luaFolders)
            {
                string luaFolderPath = Path.Combine(Application.dataPath, dirPath);
                string txtFolderPath = luaFolderPath + "_txt/";
                if (!Directory.Exists(luaFolderPath)) continue;
                Directory.CreateDirectory(txtFolderPath);
                string[] files = Directory.GetFiles(luaFolderPath, "*.lua", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    string newPath = txtFolderPath + file.Substring(luaFolderPath.Length + 1).Replace("/", "__").Replace("\\", "__") + ".txt";
                    File.Copy(file, newPath, true);
                }
                Debug.Log("Copy complete: " + txtFolderPath);
            }
            AssetDatabase.Refresh();
        }
        [MenuItem(ROOT_NAME + "Clear Lua Text", false, 18002)]
        public static void ClearLuaTxt()
        {
            foreach (string dirPath in EZLuaSettings.Instance.luaFolders)
            {
                string luaFolderPath = Path.Combine(Application.dataPath, dirPath);
                string txtDirPath = luaFolderPath + "_txt/";
                try
                {
                    Directory.Delete(txtDirPath, true);
                    Debug.Log("Delete complete: " + txtDirPath);
                    AssetDatabase.Refresh();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }
    }
}
