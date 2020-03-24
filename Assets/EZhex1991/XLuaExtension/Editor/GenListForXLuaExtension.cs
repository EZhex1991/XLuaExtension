/* Author:          ezhex1991@outlook.com
 * CreateTime:      2017-03-31 18:11:46
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace EZhex1991.XLuaExtension
{
    public static class GenListForXLuaExtension
    {
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp = new List<Type>()
        {
            typeof(EZhex1991.XLuaExtension.EZProperty),
            typeof(EZhex1991.XLuaExtension.EZPropertyList),

            typeof(EZhex1991.XLuaExtension.LuaSourceMode),
            typeof(EZhex1991.XLuaExtension.EZLuaSettings),
            typeof(EZhex1991.XLuaExtension.EZLuaManager),
            typeof(EZhex1991.XLuaExtension.EZLuaBehaviour),
            typeof(EZhex1991.XLuaExtension.EZLuaInjector),

            typeof(EZhex1991.XLuaExtension.EZLuaUtility),

            typeof(EZhex1991.XLuaExtension.ActivityMessage),
            typeof(EZhex1991.XLuaExtension.ActivityMessage.ActivityEvent),
            typeof(EZhex1991.XLuaExtension.ApplicationMessage),
            typeof(EZhex1991.XLuaExtension.ApplicationMessage.ApplicationEvent),
            typeof(EZhex1991.XLuaExtension.CollisionMessage),
            typeof(EZhex1991.XLuaExtension.CollisionMessage.CollisionEvent),
            typeof(EZhex1991.XLuaExtension.MouseMessage),
            typeof(EZhex1991.XLuaExtension.MouseMessage.MouseEvent),
            typeof(EZhex1991.XLuaExtension.TriggerMessage),
            typeof(EZhex1991.XLuaExtension.TriggerMessage.TriggerEvent),
            typeof(EZhex1991.XLuaExtension.UpdateMessage),
            typeof(EZhex1991.XLuaExtension.UpdateMessage.UpdateEvent),
        };

        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>()
        {
            typeof(EZhex1991.XLuaExtension.LuaRequire),
            typeof(EZhex1991.XLuaExtension.LuaAction),
            typeof(EZhex1991.XLuaExtension.LuaAction<LuaTable>),

            typeof(EZhex1991.XLuaExtension.EZLuaBehaviour.LuaAwake),
            typeof(EZhex1991.XLuaExtension.BundleSource),

            typeof(EZhex1991.XLuaExtension.OnMessageAction),
            typeof(EZhex1991.XLuaExtension.OnMessageAction<bool>),
            typeof(EZhex1991.XLuaExtension.OnMessageAction<Collider>),
            typeof(EZhex1991.XLuaExtension.OnMessageAction<Collision>),
        };

        [BlackList]
        public static List<List<string>> BlackList = new List<List<string>>()
        {

        };
    }
}
