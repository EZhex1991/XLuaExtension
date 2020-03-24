# 相对完整的demo

包括lua间的交互，如何设计Injector来减少Find和GetComponent并方便策划配置，如何使用工具来简化bundle的构建，lua代码的管理等。

## 00_LuaBehaviour

1. LuaBehaviour的基本使用；
1. 知识点：如何在lua和C#之前传递数据，LuaBehaviour之间如何相互调用；
1. 与xLua的官方例子区别很大，先了解如何用，然后再去看如何实现；
1. LuaBehaviour脚本位于[../Runtime/LuaBehaviour](../Runtime/LuaBehaviour)目录下，LuaBehaviour继承于LuaInjector，LuaInjector中实现了变量的注入，LuaBehaviour中实现了Lua的脚本的启动；
1. lua脚本位于[Script_Lua/LuaBehaviour](Script_Lua/LuaBehaviour)目录下；

## 01_LuckyBall

1. 鼠标点击屏幕投球；
1. 知识点：如何为LuaBehaviour绑定Update、OnTrigger之类的MonoBehaviour消息；
1. MonoBehaviour消息已经进行了分类封装，详见[../Runtime/LuaMessage](../Runtime/LuaMessage)；
1. lua脚本位于[Script_Lua/LuckBall](Script_Lua/LuckyBall)目录下；
1. 脚本外其它资源在[01_LuckyBall](01_LuckyBall)目录下；

## 02_SpaceShooter

该示例以*Unity官方Tutorial - Space Shooter*为模板，使用lua重写游戏逻辑，运行该示例前请注意以下几点。

1. 本人尽可能保证lua代码的结构与C#代码结构相似，方便初学者对比学习，甚至还保留了原示例的部分bug（比如物体没销毁啥的）；
1. **注意，Tags和Layers只能在Editor下添加，所以在热更中使用并不方便(个人在5.x下没找到其他方式)。如果报错显示找不到Tag，请自行添加，Tag0:"Enemy", Tag1:"Bonudary"**；
1. lua脚本位于[Script_Lua/SpaceShooter](Script_Lua/SpaceShooter)目录下；
1. prefab和场景均使用EZLuaBehaviour代替了原本的C#脚本，放在[02_SpaceShooter/_Complete-Lua](02_SpaceShooter/_Complete-Lua)目录下；
