--[==[
- Author:       ezhex1991@outlook.com
- CreateTime:   2017-05-23 19:00:42
- Orgnization:  #ORGNIZATION#
- Description:  
--]==]
local moduleName = ...
local M = {}
M.__index = M
----- begin module -----

-- float和int参数造成UnityEngine.Random.Range重载调用不明确
-- float和int在lua上无法区分，这里会调用Range(float, float)
print("Random.Range(0,10)\n", CS.UnityEngine.Random.Range(0, 10))
-- 自己把这个方法封装一下RandomInt(int, int) -> Range(int, int)
print("OverloadWrap.RandomInt(0,10)\n", CS.EZhex1991.XLuaExtension.Example.OverloadWrap.RandomInt(0, 10))
-- RandomFloat(float, float) -> Range(float, float)
print("OverloadWrap.RandomFoat(0,10)\n", CS.EZhex1991.XLuaExtension.Example.OverloadWrap.RandomFloat(0, 10))

-- out和ref参数造成UnityEngine.Physics.Raycast重载调用不明确
local origin = CS.UnityEngine.Vector3.one * -1
local direction = CS.UnityEngine.Vector3.one
local ray = CS.UnityEngine.Ray(origin, direction)
-- 无法区分Raycast(Ray)和Raycast(Ray, out HitInfo)，这里会调用Raycast(Ray)
local hit1, info1 = CS.UnityEngine.Physics.Raycast(ray)
print("Physics.Raycast\n", hit1, info1 and info1.collider.name)
-- 自己封装Raycast(Ray, out HitInfo)
local hit2, info2 = CS.EZhex1991.XLuaExtension.Example.OverloadWrap.RaycastOut(ray)
print("OverloadWrap.RaycastOut\n", hit2, info2 and info2.collider.name)

----- end -----
return M
