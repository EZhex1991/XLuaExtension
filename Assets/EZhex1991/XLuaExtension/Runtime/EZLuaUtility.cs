/* Author:          ezhex1991@outlook.com
 * CreateTime:      2017-09-26 13:02:58
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System.Collections;
using UnityEngine;
using XLua;

namespace EZhex1991.XLuaExtension
{
    public static class EZLuaUtility
    {
        public static EZLuaBehaviour GetLuaBehaviour(this GameObject go, string moduleName)
        {
            EZLuaBehaviour[] behaviours = go.GetComponents<EZLuaBehaviour>();
            if (moduleName.Contains("."))
            {
                for (int i = 0; i < behaviours.Length; i++)
                {
                    if (behaviours[i].moduleName == moduleName)
                    {
                        return behaviours[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < behaviours.Length; i++)
                {
                    string shortName = behaviours[i].moduleName;
                    if (shortName.Contains(".")) shortName = shortName.Substring(shortName.LastIndexOf(".") + 1);
                    if (shortName == moduleName)
                    {
                        return behaviours[i];
                    }
                }
            }
            return null;
        }
        public static LuaTable GetLuaTable(this GameObject go, string moduleName)
        {
            EZLuaBehaviour behaviour = go.GetLuaBehaviour(moduleName);
            return behaviour ? behaviour.luaTable : null;
        }

        // lua时间精确度不太可靠
        public static long CurrentTime
        {
            get
            {
                return System.DateTime.UtcNow.Ticks;
            }
        }
        public static long DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond = 0)
        {
            return new System.DateTime(year, month, day, hour, minute, second, millisecond).Ticks;
        }
        public static double TimeSpanInMilliseconds(long ticks1, long ticks2)
        {
            return new System.TimeSpan(ticks2 - ticks1).TotalMilliseconds;
        }
        public static double RelativeTime()
        {
            return RelativeTime(CurrentTime);
        }
        public static double RelativeTime(long ticks)
        {
            return TimeSpanInMilliseconds(DateTime(1970, 1, 1, 0, 0, 0), ticks);
        }

        // 小数位的保留
        public static double Round(double number, int digits = 2)
        {
            return System.Math.Round(number, digits);
        }

        // System.String只导出部分方法
        public static string FormatString(string format, params object[] args)
        {
            return System.String.Format(format, args);
        }

        // 枚举到int的转换
        public static int ToInt32(object value)
        {
            return System.Convert.ToInt32(value);
        }

        // 变量是否为空的判断由C#来做比较保险
        public static bool IsNull(Object o)
        {
            return o == null;
        }

        // 词典不能使用string和object索引，getter可使用TryGetValue，setter用该方法代替
        public static void SetItem(IDictionary dict, object key, object value)
        {
            dict[key] = value;
        }

        // float和int参数造成UnityEngine.Random.Range和UnityEngine.Mathf.Clamp重载调用不明确
        public static int RandomInt(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        public static float RandomFloat(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        public static int ClampInt(int value, int min, int max)
        {
            return UnityEngine.Mathf.Clamp(value, min, max);
        }
        public static float ClampFloat(float value, float min, float max)
        {
            return UnityEngine.Mathf.Clamp(value, min, max);
        }

        // out和ref参数造成UnityEngine.Physics.Raycast重载调用不明确
        public static bool Raycast(Ray ray, out RaycastHit hitInfo)
        {
            return UnityEngine.Physics.Raycast(ray, out hitInfo);
        }
        public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
        {
            return UnityEngine.Physics.Raycast(ray, out hitInfo, maxDistance);
        }
        public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
        {
            return UnityEngine.Physics.Raycast(ray, out hitInfo, maxDistance, layerMask);
        }
    }
}
