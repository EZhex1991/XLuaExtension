--[==[
- Author:       ezhex1991@outlook.com
- CreateTime:   2017-06-09 15:09:45
- Orgnization:  #ORGNIZATION#
- Description:  
--]==]
local moduleName = ...
local M = {}
M.__index = M
----- begin module -----
local GameObject = CS.UnityEngine.GameObject

local obj = GameObject("TestObj")

-- 物体上是没有Rigidbody的
local rigidbody = obj:GetComponent("Rigidbody")
-- 返回值实际上是一个Object，在C#上因为重载了==null所以为空，此处==nil不行
print("rigidbody == nil", rigidbody == nil)
-- object.Equals方法可以获取到正确值，但“object”在lua里可能是个nil（编辑器和真机出现过不一致的情况），所以一般需要rigidbody == nil or rigidbody:Equlas(nil)
print("rigidbody:Equals(nil)", rigidbody:Equals(nil))
-- xLua作者在faq上推荐的方法，比较方便，也比较保险
print("IsNull(rigidbody)", CS.EZhex1991.XLuaExtension.Example.CheckNull.IsNull(rigidbody))
----- end -----
return M
