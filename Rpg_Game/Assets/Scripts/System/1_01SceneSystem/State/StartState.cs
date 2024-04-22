using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʼ����״̬��
/// </summary>
public class StartState : ISceneState
{
    /// <summary>
    /// ��Դ���صȴ�ʱ��
    /// </summary>
    private float waitTiem;

    /// <summary>
    /// ���캯��  �̳и��������
    /// </summary>
    /// <param name="controllor"></param>
    public StartState(SceneStateControllor controllor) :base(controllor) 
    {
        this.StateName = "StartScene";
        waitTiem = 2f;
    }




    public override void StateBegin()
    {
        base.StateBegin();
        ((UISystem)GameManager.Instance.GameSystemDic[SystemType.UI]).OpenView(3);

        //TODO ���ݼ��� ������ʼ��
        //m_Controller.SetState(new MainMenuState(m_Controller));
    }


    public override void StateEnd()
    {
        base.StateEnd();
    }

    public override void StateUpdate(float delatTim)
    {
        base.StateUpdate(delatTim);  
    }
}
