/* Author:          ezhex1991@outlook.com
 * CreateTime:      2017-04-05 12:57:17
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;
using XLua;

namespace EZhex1991.XLuaExtension.Example
{
    [LuaCallCSharp]
    public static class OverloadWrap
    {
        // float和int参数造成UnityEngine.Random.Range重载调用不明确
        public static int RandomInt(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        public static float RandomFloat(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        // out参数造成UnityEngine.Physics.Raycast重载调用不明确(ref同理)
        public static bool Raycast(Ray ray)
        {
            return UnityEngine.Physics.Raycast(ray);
        }
        public static bool RaycastOut(Ray ray, out RaycastHit hitInfo)
        {
            return UnityEngine.Physics.Raycast(ray, out hitInfo);
        }
    }
}