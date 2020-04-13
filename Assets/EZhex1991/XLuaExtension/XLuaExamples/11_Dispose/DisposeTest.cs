/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-11-25 13:36:11
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XLua;

namespace EZhex1991.XLuaExtension.Example
{
    /// <summary>
    /// -----Dispose并不是必要的-----
    /// -----Dispose并不是必要的-----
    /// -----Dispose并不是必要的-----
    /// 通常来说，整个应用只需要一个LuaEnv，并且这个LuaEnv的生命周期与该应用一致，换句话说，Dispose大部分时候意味着应用要退出了
    /// 你根本不用在乎这个LuaEnv在退出前是否被合理释放 - 它是应用的一部分，自然会随着应用的结束而被系统处理
    /// 
    /// 如果你真的要手动Dispose，你只需要释放掉被引用的lua方法即可，具体说明请查看官方FAQ
    /// require('xlua.util').print_func_ref_by_csharp()可以用来查看未释放的引用
    /// </summary>
    [LuaCallCSharp]
    public class DisposeTest : MonoBehaviour
    {
        private LuaEnv luaEnv;
        public Button button_Test;
        public Button button_Unregister;
        public UnityAction luaFunction;

        private void Start()
        {
            luaEnv = new LuaEnv();
            luaEnv.DoString(@"
                function LuaFunction()
                    print('Lua Function')
                end
            ");

            print("----------Register----------");
            luaFunction = luaEnv.Global.Get<UnityAction>("LuaFunction");
            button_Test.onClick.AddListener(luaFunction);

            button_Unregister.onClick.AddListener(Unregister);
        }
        private void Unregister()
        {
            print("----------Unregistered----------");
            button_Test.onClick.RemoveAllListeners();
            // UnityAction会有缓存，清除后需要调用一下
            button_Test.onClick.Invoke();
            // 变量置空
            luaFunction = null;
        }
        private void OnDestroy()
        {
            // 未进行Unregister就退出会报错
            luaEnv.Dispose();
            print("LuaEnv Disposed");
        }
    }

    public static class DisposeTestConfig
    {
        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>()
        {
            typeof(Action),
            typeof(Action<int, int>),
            typeof(UnityAction),
        };
    }
}
