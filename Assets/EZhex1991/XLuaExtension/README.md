# XLuaExtension

该部分是基于腾讯XLua的扩展，需要配合腾讯的XLua使用

几个重要脚本说明如下：

## EZLuaSettings

存放一些重要配置如：lua的开发目录，lua的加载方式等。该配置文件直接存放于"Resources/EZLuaSettings.asset"。

通过菜单[EZhex1991 -> XLuaExtension -> Lua Setting]可快速创建或选择到该配置文件。

"Lua Source Mode"为lua文件的读取方式

- Develop: 根据配置的"LuaFolder"目录列表直接进行文件读取（只能在Editor下使用）
- Local: 根据配置的"LuaBundle"名称列表从"StreamingAssets"目录中加载Bundle
- Remote: 根据配置的"LuaBunle"名称列表从"PersistentDataPath"目录中加载Bundle

菜单[EZhex1991 -> XLuaExtension -> '.lua' to '.txt']会遍历配置中的"LuaFolder"目录列表，拷贝其中的'.lua'文件并重命名为'.lua.txt'文件，方便进行Bundle。

## EZLuaManager

单例，用来初始化`LuaEnv`，会根据`EZLuaSettings`中配置的lua加载方式使用不同的`CustomLoader`，
如果`LuaSourceMode`是`Local`或者`Remote`，`BundleSourceLocal`和`BundleSourceRemote`可以让你以自定义的方式加载Bundle。

`public delegate AssetBundle BundleSource(string bundleName);`

其中bundleName为`EZLuaSettings`中配置的Bundle名称。

## EZLuaBehaviour

继承关系：`EZLuaBehaviour`-`EZLuaInjector`-`EZPropertyList`-`MonoBehaviour`

`EZProperty`的目的是为了实现类似于`UnityEditor.SerializedProperty`的功能，在一个大的可序列化类型中存下多种类型的数据，配套的`EZPropertyDrawer`来限定Inspector中的可编辑类型。

`EZPropertyList`则是`EZProperty`的集合，并且可以嵌套，根据`m_IsList`字段有list和mapping两种表现。

`EZLuaInjector`在`EZPropertyList`的基础上提供了`Inject(LuaTable)`方法，可以将集合内Property当前类型的数据注入到一个LuaTable中（如果其中有Injector嵌套，那么注入也会是嵌套的）。

`EZLuaBehaviour`在`EZLuaInjector`的基础上导出了`MonoBehaviour`的一些生命周期相关的消息，可用于lua脚本的启动。

![NestedInjector](.SamplePicture/NestedInjector.png)
![TypeSelection](.SamplePicture/TypeSelection.png)

## EZLuaMessage

该部分是`MonoBehaviour`消息的一些分类封装

- `_Message<T>`: 基础泛型，可模仿其他实例类型自己对消息进行分类封装，包含`Require(GameObject)`和`Dismiss(GameObject)`方法来获取和移除某个物体上的消息监听器
- `ActivityMessage`: 包含Start, OnEnable, OnDisable, OnDestory一类脚本活动相关消息
- `ApplicationMessage`: 包含OnApplicationFocus，OnApplicationPause一类应用活动相关消息
- `CollisionMessage`: 包含OnCollision*一类碰撞消息
- `TriggerMessage`: 包含OnTrigger*一类触发消息
- `UpdateMessage`: 包含Update, FixedUpdate, LateUpdate一类游戏刷新消息
- `MouseMessage`: 包含OnMouse*一类MouseInput相关消息

这些消息都以`OnMessageEvent`或`OnMessageEvent<T>`的class形式封装，使用方法`AddAction`, `RemoveAction`, `Clear`来添加、移除或清空注册的消息。

用法示例：

``` lua
function luaUpdate()
    print(CS.UnityEngine.Time.deltaTime)
end
local updateMessage = CS.EZhex1991.XLuaExtension.UpdateMessage.Require(gameOject)    -- 获取
updateMessage.update:AddAction(luaUpdate)   -- 添加
updateMessage.update:RemoveAction(luaUpdate)   -- 移除
updateMessage.lateUpdate:Clear()   -- 清空
updateMessage:Dismiss()    -- 销毁，或者CS.EZhex1991.XLuaExtension.UpdateMessage.Dismiss(gameOject)

function luaOnCollisionStay(collision)
    print(collision.collider.name)
end
local collisionMessage = CS.EZhex1991.XLuaExtension.CollisionMessage.Require(gameObject)
collisionMessage.onCollisionStay:AddAction(luaOnCollisionStay)
```
