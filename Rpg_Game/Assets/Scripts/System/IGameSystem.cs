using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ϵͳ��
/// </summary>
public abstract class IGameSystem 
{
    protected GameManager m_GameMgr = null;
    public IGameSystem(GameManager gameManager) 
    {
        m_GameMgr = gameManager;
    }

    public virtual void Init() 
    {
    }

    public virtual void Release() 
    {

    }

    public virtual void Update(float delatTime) 
    {

    }
}
