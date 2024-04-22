using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����л�������
/// </summary>
public class SceneStateControllor 
{
    /// <summary>
    /// ���캯��
    /// </summary>
    public SceneStateControllor() 
    {
        m_State = null;
        isChangeState = false;
    }

    /// <summary>
    /// ����״̬��
    /// </summary>
    private ISceneState m_State;
    /// <summary>
    /// �Ƿ��л�״̬
    /// </summary>
    private bool isChangeState;
    /// <summary>
    /// ���س�������¼��ص�
    /// </summary>
    System.Action LoadCallBack;

    /// <summary>
    /// ���ó���״̬
    /// </summary>
    public void SetState(ISceneState state) 
    {       
        if (m_State != null)
        {//������һ������
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
        {//����ˢ��
            m_State.StateUpdate(delatTim);
        }
    }


}
