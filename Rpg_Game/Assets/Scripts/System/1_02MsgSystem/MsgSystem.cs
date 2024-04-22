using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public delegate void OnActionHander(params object[] param);
public delegate void OnActionNetHander(byte[] buffer);
public class MsgSystem : IGameSystem
{
    EventDispatcher eventDispatcher = new EventDispatcher();
    public MsgSystem(GameManager gameManager) : base(gameManager) 
    {
        m_GameMgr.GameSystemDic.Add(SystemType.Msg, this);
    }

    #region  ϵͳ��Ϣ
    /// <summary>
    /// ����¼�
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void AddEvent(ushort eventId, OnActionHander onActionHander) 
    {
        eventDispatcher.AddEventListen(eventId, onActionHander);
    }
    /// <summary>
    /// �Ƴ��¼�
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void RemoveEvent(ushort eventId, OnActionHander onActionHander)
    {
        eventDispatcher.RemoveEventListen(eventId, onActionHander);
    }
    /// <summary>
    /// ��Ӧ�¼�
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="param"></param>
    public void FireEvent(ushort eventId, params Object[] param)
    {
        eventDispatcher.Dispatcher(eventId, param);
    }
    #endregion

    #region  ������Ϣ
    /// <summary>
    /// ����¼�
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void AddNetEvent(ushort protoCode, OnActionNetHander onActionNetHander)
    {
        eventDispatcher.AddEventNetListen(protoCode, onActionNetHander);
    }

    internal void AddNetEvent(object loginEvent)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// �Ƴ��¼�
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void RemoveNetEvent(ushort protoCode, OnActionNetHander onActionNetHander)
    {
        eventDispatcher.RemoveEventNstListen(protoCode, onActionNetHander);
    }
    /// <summary>
    /// ��Ӧ�¼�
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="param"></param>
    public void FireNetEvent(ushort protoCode,byte[] buffer)
    {
        eventDispatcher.NetDispatcher(protoCode, buffer);
    }
    #endregion
}
