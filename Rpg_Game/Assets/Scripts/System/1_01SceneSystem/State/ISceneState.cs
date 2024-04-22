using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬����
/// </summary>
public class ISceneState
{
    /// <summary>
    /// ��������
    /// </summary>
    private string m_StateName = "ISceneState";
    /// <summary>
    /// ��������
    /// </summary>
    public string StateName
    {
        get { return m_StateName; }
        set { m_StateName = value; }
    }

    /// <summary>
    /// ����������
    /// </summary>
    protected SceneStateControllor m_Controller = null;
    /// <summary>
    /// ���캯��  ��ֵ����������
    /// </summary>
    /// <param name="controllor"></param>
    public ISceneState(SceneStateControllor controllor) 
    {
        m_Controller = controllor;
    }

    /// <summary>
    /// ״̬��ʼ
    /// </summary>
    public virtual void StateBegin() 
    {

    }
    /// <summary>
    /// ״̬����
    /// </summary>
    public virtual void StateEnd()
    {

    }
    /// <summary>
    /// ״̬ˢ��
    /// </summary>
    public virtual void StateUpdate(float delatTim)
    {

    }
    public override string ToString()
    {
        return string.Format("[SceneState:StateName = {0}]", StateName);
    }
}
