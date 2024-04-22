using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    /// <summary>
    /// ��Ϸ�ܿ� ����
    /// </summary>
    private static GameManager instance;
    /// <summary>
    /// ��Ϸ�ܿ� ����
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
    /// ��ȡ��ǰ�ķ�����ʱ��
    /// </summary>
    /// <returns></returns>
    public long CurrServerTime
    {
        get { return GameGlobal.Instance.ServerTime + (long)Time.time; }
    }


    /// <summary>
    /// ϵͳ�ֵ� 
    /// </summary>
    public Dictionary<SystemType, IGameSystem> GameSystemDic 
    {
        get; set;
    }
    /// <summary>
    /// ����ϵͳ
    /// </summary>
    SceneSystem sceneSystem;
    /// <summary>
    /// ��Ϣϵͳ
    /// </summary>
    MsgSystem msgSystem;
    /// <summary>
    /// UIϵͳ
    /// </summary>
    UISystem uiSystem;
    /// <summary>
    /// ��ɫϵͳ
    /// </summary>
    RoleSystem roleSystem;

    /// <summary>
    /// ������ ���캯��
    /// </summary>
    private GameManager()
    {
        GameSystemDic = new Dictionary<SystemType, IGameSystem>();
    }

    public void Init() 
    {
        Debug.Log("��Ϸϵͳ��ʼ��...");
        //��ʼ����ʱ�� ���������ʱ��
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
