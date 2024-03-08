using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEventTest : MonoBehaviour
{
    /// <summary>
    /// 事件接受
    /// </summary>
    public GameObject go;


    void Start()
    {
        //// go是目标物体GameObject对象，bt是自身行为树对象
        //SharedGameObject targetObj = new SharedGameObject();
        //targetObj.Value = go;

        //// 获取行为树对象
        //var bt = GetComponent<BehaviorTree>();
        //bt.SetVariable("targetObj", targetObj);

        // bt是BehaviorTree对象
        //bt.SendEvent("EAT_EVENT");

    }
}
