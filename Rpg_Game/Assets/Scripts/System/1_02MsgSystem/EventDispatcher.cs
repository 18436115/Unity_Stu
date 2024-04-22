using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher
{   
    /// <summary>
    /// 事件字典
    /// </summary>
    public Dictionary<ushort, List<OnActionHander>> dic = new Dictionary<ushort, List<OnActionHander>>();
    /// <summary>
    /// 网络事件字典
    /// </summary>
    public Dictionary<ushort, List<OnActionNetHander>> dicNet = new Dictionary<ushort, List<OnActionNetHander>>();

    #region 系统响应事件
    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void AddEventListen(ushort eventId,OnActionHander onActionHander) 
    {
        if (dic.ContainsKey(eventId))
        {
            dic[eventId].Add(onActionHander);
        }
        else 
        {
            List<OnActionHander> eventList = new List<OnActionHander>();
            dic[eventId] = eventList;
            dic[eventId].Add(onActionHander);
        }
    }
    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onActionHander"></param>
    public void RemoveEventListen(ushort eventId, OnActionHander onActionHander) 
    {
        if (dic.ContainsKey(eventId))
        {
            //dic[eventId].Remove(onActionHander);
            List<OnActionHander> eventList = dic[eventId];
            eventList.Remove(onActionHander);
            if (eventList.Count == 0) 
            {
                dic.Remove(eventId);
            }
        }
        else
        {
            Debug.Log("Error Haven't Event");
        }
    }
    /// <summary>
    /// 响应事件
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="param"></param>
    public void Dispatcher(ushort eventId, params Object[] param) 
    {
        if (dic.ContainsKey(eventId))
        {
            List<OnActionHander> eventList = dic[eventId];
            if (eventList != null && eventList.Count > 0) 
            {
                for (int i=0;i<eventList.Count; i++) 
                {
                    if (eventList[i] != null) 
                    {
                        eventList[i](param);
                    }
                }
            }
        }
        else 
        {
            Debug.Log("Error Haven't EventHander");
        }
    }
    #endregion

    #region 网络响应事件
    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="onActionNetHander"></param>
    public void AddEventNetListen(ushort protoCode, OnActionNetHander onActionNetHander)
    {
        if (dicNet.ContainsKey(protoCode))
        {
            dicNet[protoCode].Add(onActionNetHander);
        }
        else
        {
            List<OnActionNetHander> eventList = new List<OnActionNetHander>();
            dicNet[protoCode] = eventList;
            dicNet[protoCode].Add(onActionNetHander);
        }
    }
    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="onActionNetHander"></param>
    public void RemoveEventNstListen(ushort protoCode, OnActionNetHander onActionNetHander)
    {
        if (dicNet.ContainsKey(protoCode))
        {
            //dic[eventId].Remove(onActionHander);
            List<OnActionNetHander> eventList = dicNet[protoCode];
            eventList.Remove(onActionNetHander);
            if (eventList.Count == 0)
            {
                dicNet.Remove(protoCode);
            }
        }
        else
        {
            Debug.Log("Error Haven't Net Event");
        }
    }
    /// <summary>
    /// 响应事件
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="buffer"></param>
    public void NetDispatcher(ushort protoCode, byte[] buffer)
    {
        if (dicNet.ContainsKey(protoCode))
        {
            List<OnActionNetHander> eventList = dicNet[protoCode];
            if (eventList != null && eventList.Count > 0)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    if (eventList[i] != null)
                    {
                        eventList[i](buffer);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Error Haven't EventHander");
        }
    }
    #endregion
}
