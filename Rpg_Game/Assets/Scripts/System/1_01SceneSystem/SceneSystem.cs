using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����ϵͳ
public class SceneSystem : IGameSystem
{
    /// <summary>
    /// ����״̬������
    /// </summary>
    SceneStateControllor m_SceneStateControllor = new SceneStateControllor();

    public SceneSystem(GameManager gameManager) :base(gameManager)
    {
        m_GameMgr.GameSystemDic.Add(SystemType.Scene, this);
    }


    public override void Init()
    {
        base.Init();
        Debug.Log("����ϵͳ��ʼ��...");
        //��ӳ�ʼ����
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