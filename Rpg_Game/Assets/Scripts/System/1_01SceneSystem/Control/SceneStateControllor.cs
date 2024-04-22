using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景切换控制类
/// </summary>
public class SceneStateControllor 
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SceneStateControllor() 
    {
        m_State = null;
        isChangeState = false;
    }

    /// <summary>
    /// 场景状态类
    /// </summary>
    private ISceneState m_State;
    /// <summary>
    /// 是否切换状态
    /// </summary>
    private bool isChangeState;
    /// <summary>
    /// 加载场景完成事件回调
    /// </summary>
    System.Action LoadCallBack;

    /// <summary>
    /// 设置场景状态
    /// </summary>
    public void SetState(ISceneState state) 
    {       
        if (m_State != null)
        {//结束上一个场景
            AppDebug.Log("StateEnd:" + m_State.ToString());
            m_State.StateEnd();          
        }
        
        m_State = state;
        isChangeState = true;
        if (m_State != null)
        {
            LoadScene.Instance.Load(m_State.StateName,
                ()=>{
                    isChangeState = false;
                    AppDebug.Log("StateBegin:" + m_State.ToString());
                    m_State.StateBegin();         
                });
        }
    }


    public void StateUpdate(float delatTim) 
    {
        if (m_State != null && !isChangeState)  
        {//场景刷新
            m_State.StateUpdate(delatTim);
        }
    }


}
