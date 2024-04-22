using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    /// <summary>
    /// 游戏总控 单例
    /// </summary>
    private static GameManager instance;
    /// <summary>
    /// 游戏总控 单例
    /// </summary>
    public static GameManager Instance 
    {
        get 
        {
            if (instance == null) 
            {
                instance = new GameManager();
            }
            return instance;
        }
    }


    /// <summary>
    /// 获取当前的服务器时间
    /// </summary>
    /// <returns></returns>
    public long CurrServerTime
    {
        get { return GameGlobal.Instance.ServerTime + (long)Time.time; }
    }


    /// <summary>
    /// 系统字典 
    /// </summary>
    public Dictionary<SystemType, IGameSystem> GameSystemDic 
    {
        get; set;
    }
    /// <summary>
    /// 场景系统
    /// </summary>
    SceneSystem sceneSystem;
    /// <summary>
    /// 消息系统
    /// </summary>
    MsgSystem msgSystem;
    /// <summary>
    /// UI系统
    /// </summary>
    UISystem uiSystem;
    /// <summary>
    /// 角色系统
    /// </summary>
    RoleSystem roleSystem;

    /// <summary>
    /// 管理类 构造函数
    /// </summary>
    private GameManager()
    {
        GameSystemDic = new Dictionary<SystemType, IGameSystem>();
    }

    public void Init() 
    {
        Debug.Log("游戏系统初始化...");
        //初始化的时候 请求服务器时间
        NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/Init", OnInitCallBack);

        sceneSystem = new SceneSystem(this);
        msgSystem = new MsgSystem(this);
        uiSystem = new UISystem(this);
        roleSystem = new RoleSystem(this);

        foreach (IGameSystem system in GameSystemDic.Values)
        {
            system.Init();
        }
    }

    public void Relesae() 
    {
        foreach (IGameSystem system in GameSystemDic.Values)
        {
            system.Release();
        }
    }


    public void Update(float delatTime) 
    {
        foreach (IGameSystem system in GameSystemDic.Values)
        {
            system.Update(delatTime);
        }
    }



    //---------------------

    private void OnInitCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            AppDebug.Log(obj.ErrorMsg);
        }
        else 
        {
            GameGlobal.Instance.ServerTime = long.Parse(obj.Value);
            AppDebug.Log(obj.Value);
        }

    }
}
