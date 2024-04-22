using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 开始场景状态类
/// </summary>
public class StartState : ISceneState
{
    /// <summary>
    /// 资源加载等待时间
    /// </summary>
    private float waitTiem;

    /// <summary>
    /// 构造函数  继承父类控制类
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

        //TODO 数据加载 场景初始话
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
