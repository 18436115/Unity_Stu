using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景状态基类
/// </summary>
public class ISceneState
{
    /// <summary>
    /// 场景名称
    /// </summary>
    private string m_StateName = "ISceneState";
    /// <summary>
    /// 场景名称
    /// </summary>
    public string StateName
    {
        get { return m_StateName; }
        set { m_StateName = value; }
    }

    /// <summary>
    /// 场景控制类
    /// </summary>
    protected SceneStateControllor m_Controller = null;
    /// <summary>
    /// 构造函数  赋值场景控制类
    /// </summary>
    /// <param name="controllor"></param>
    public ISceneState(SceneStateControllor controllor) 
    {
        m_Controller = controllor;
    }

    /// <summary>
    /// 状态开始
    /// </summary>
    public virtual void StateBegin() 
    {

    }
    /// <summary>
    /// 状态结束
    /// </summary>
    public virtual void StateEnd()
    {

    }
    /// <summary>
    /// 状态刷新
    /// </summary>
    public virtual void StateUpdate(float delatTim)
    {

    }
    public override string ToString()
    {
        return string.Format("[SceneState:StateName = {0}]", StateName);
    }
}
