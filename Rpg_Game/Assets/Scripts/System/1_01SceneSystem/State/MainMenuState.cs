using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主界面场景状态类
/// </summary>
public class MainMenuState : ISceneState
{
    /// <summary>
    /// 构造函数  继承父类控制类
    /// </summary>
    /// <param name="controllor"></param>
    public MainMenuState(SceneStateControllor controllor) : base(controllor)
    {
        this.StateName = "MainMenuScene";
    }

    public override void StateBegin()
    {
        base.StateBegin();
        ((UISystem)GameManager.Instance.GameSystemDic[SystemType.UI]).OpenView(1);
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
