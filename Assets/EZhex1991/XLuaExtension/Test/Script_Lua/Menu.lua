--[==[
- Author:       ezhex1991@outlook.com
- CreateTime:   2018-02-27 17:18:49
- Orgnization:  #ORGNIZATION#
- Description:  
--]==]
local M = {}
----- CODE -----
local SceneManager = CS.UnityEngine.SceneManagement.SceneManager
local LoadSceneMode = CS.UnityEngine.SceneManagement.LoadSceneMode

function M.LuaAwake(injector)
    M.btns = {}
    injector:Inject(M)
    M.btns[1].onClick:AddListener(M.LuaBehaviour)
    M.btns[2].onClick:AddListener(M.LuckyBall)
    M.btns[3].onClick:AddListener(M.SpaceShooter)
    return M
end

function M.LoadScene(sceneName)
    SceneManager.LoadScene(sceneName, LoadSceneMode.Single)
end

function M.LuaBehaviour()
    M.LoadScene("LuaBehaviour")
end

function M.LuckyBall()
    M.LoadScene("LuckyBall")
end

function M.SpaceShooter()
    M.LoadScene("SpaceShooter")
end
----- CODE -----
return M
