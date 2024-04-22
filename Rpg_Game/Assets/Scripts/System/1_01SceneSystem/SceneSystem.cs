using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//场景系统
public class SceneSystem : IGameSystem
{
    /// <summary>
    /// 场景状态控制类
    /// </summary>
    SceneStateControllor m_SceneStateControllor = new SceneStateControllor();

    public SceneSystem(GameManager gameManager) :base(gameManager)
    {
        m_GameMgr.GameSystemDic.Add(SystemType.Scene, this);
    }


    public override void Init()
    {
        base.Init();
        Debug.Log("场景系统初始化...");
        //添加初始场景
        StartState startState = new StartState(m_SceneStateControllor);
        if (startState != null)
        {
            m_SceneStateControllor.SetState(startState);
        }
    }

    public override void Release()
    {
        base.Release();
    }

    public override void Update(float delatTime)
    {
        base.Update(delatTime);
        m_SceneStateControllor.StateUpdate(delatTime);
    }
}