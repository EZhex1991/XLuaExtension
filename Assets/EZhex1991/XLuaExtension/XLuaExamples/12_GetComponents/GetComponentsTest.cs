/* Author:          ezhex1991@outlook.com
 * CreateTime:      2020-04-13 16:47:29
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;
using XLua;

namespace EZhex1991.XLuaExtension.Example
{
    [LuaCallCSharp]
    public class GetComponentsTest : MonoBehaviour
    {
        private LuaEnv luaEnv;
        public static Transform[] transforms;

        private void Start()
        {
            luaEnv = new LuaEnv();
            luaEnv.DoString(@"
                local GetComponentsTest = CS.EZhex1991.XLuaExtension.Example.GetComponentsTest
                local parent = CS.UnityEngine.GameObject.Find('GetComponentsTest')

                -- 此处获取的是Component[]，无法赋值给Transform[]
                local transforms = parent:GetComponentsInChildren(typeof(CS.UnityEngine.Transform))
                -- GetComponentsTest.transforms = transforms -- 错误

                -- 创建数组
                GetComponentsTest.transforms =
                    CS.System.Array.CreateInstance(typeof(CS.UnityEngine.Transform), transforms.Length)
                -- 遍历赋值，XLua会帮你处理类型转换
                for i = 0, transforms.Length - 1 do
                    GetComponentsTest.transforms[i] = transforms[i]
                end

                for i = 0, GetComponentsTest.transforms.Length - 1 do
                    print(GetComponentsTest.transforms[i])
                end
            ");
        }
    }
}
