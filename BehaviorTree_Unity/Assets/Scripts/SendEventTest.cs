using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEventTest : MonoBehaviour
{
    /// <summary>
    /// �¼�����
    /// </summary>
    public GameObject go;


    void Start()
    {
        //// go��Ŀ������GameObject����bt��������Ϊ������
        //SharedGameObject targetObj = new SharedGameObject();
        //targetObj.Value = go;

        //// ��ȡ��Ϊ������
        //var bt = GetComponent<BehaviorTree>();
        //bt.SetVariable("targetObj", targetObj);

        // bt��BehaviorTree����
        //bt.SendEvent("EAT_EVENT");

    }
}
