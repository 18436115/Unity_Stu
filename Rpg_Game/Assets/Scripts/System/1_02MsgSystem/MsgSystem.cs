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

    #region  系统消息
    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void AddEvent(ushort eventId, OnActionHander onActionHander) 
    {
        eventDispatcher.AddEventListen(eventId, onActionHander);
    }
    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void RemoveEvent(ushort eventId, OnActionHander onActionHander)
    {
        eventDispatcher.RemoveEventListen(eventId, onActionHander);
    }
    /// <summary>
    /// 响应事件
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="param"></param>
    public void FireEvent(ushort eventId, params Object[] param)
    {
        eventDispatcher.Dispatcher(eventId, param);
    }
    #endregion

    #region  网络消息
    /// <summary>
    /// 添加事件
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
    /// 移除事件
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void RemoveNetEvent(ushort protoCode, OnActionNetHander onActionNetHander)
    {
        eventDispatcher.RemoveEventNstListen(protoCode, onActionNetHander);
    }
    /// <summary>
    /// 响应事件
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="param"></param>
    public void FireNetEvent(ushort protoCode,byte[] buffer)
    {
        eventDispatcher.NetDispatcher(protoCode, buffer);
    }
    #endregion
}
