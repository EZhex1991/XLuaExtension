--[==[
- Author:       ezhex1991@outlook.com
- CreateTime:   2017-05-23 17:55:45
- Orgnization:  #ORGNIZATION#
- Description:  
--]==]
local moduleName = ...
local M = {}
M.__index = M
----- begin module -----
-- 泛型的表示可以在C#上通过Debug.Log(typeof(Type))输出
--local DictSS = CS.System.Collections.Generic["Dictionary`2[System.String,System.String]"]

-- 新版本的优化写法
local DictSS = CS.System.Collections.Generic.Dictionary(CS.System.String, CS.System.String)
local dictSS = DictSS()

-- 直接访问C#方法
dictSS:Add("0", "zero")
dictSS:Add("1", "one")

-- 索引访问和同名成员访问会有冲突
print("dictSS.Count: ", dictSS.Count)
print("dictSS['Count']: ", dictSS["Count"])

local keys = {"0", "1", "2"}
for i = 1, 3 do
	-- 字符串Key不能通过[]直接索引了，需要通过TryGetValue来获取（数字依然可以直接索引）
	local succeed, value = dictSS:TryGetValue(keys[i])
	print(succeed, keys[i], value)
end

-- dictSS["2"] = "two";	-- setter之前可以用后来也被改掉了，此处会报错
-- 自己封装方法替代setter
CS.EZhex1991.XLuaExtension.Example.Dictionary.SetItem(dictSS, "2", "two")

-- 遍历用Enumerator
local enum = dictSS:GetEnumerator()
while enum:MoveNext() do
	print(enum.Current.Key, enum.Current.Value)
end
----- end -----
return M
