/* Author:          ezhex1991@outlook.com
 * CreateTime:      2017-03-13 11:23:11
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using CSObjectWrapEditor;
using UnityEngine;

namespace EZhex1991.XLuaExtension.Example
{
    public static class GenConfig
    {
        [GenPath]
        public static string GenPath = Application.dataPath + "/XLua/Gen/";

        [GenCodeMenu]
        public static void OnGenCode()
        {

        }
    }
}
