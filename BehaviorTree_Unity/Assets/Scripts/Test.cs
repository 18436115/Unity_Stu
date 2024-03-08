using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    BehaviorTree bt;
    private void Start()
    {
        // ��ȡ��Ϊ������
        bt = GetComponent<BehaviorTree>();

        //bt.RegisterEvent<Transform>("Happy_Event", HappyEvent);
        //bt.SendEvent("EAT_EVENT");

        // ���ʳ�Ա����-----------------------------------------
        var sharedBlog = (SharedBlog)bt.GetVariable("blog");
        Debug.Log(sharedBlog.Value.ToString());

        // �޸ı���ֵ-----------------------------------------
        sharedBlog.Value.author = "Unity3D�������������·�";
        bt.SetVariable("blog", sharedBlog);


        // �����µĳ�Ա����-----------------------------------------
        SharedBlog newSharedBlog = new SharedBlog();
        Blog newBlog = new Blog();
        newBlog.author = "���ͼ���ר�ң����·�";
        newBlog.url = "https://blog.csdn.net/linxinfa";
        newSharedBlog.SetValue(newBlog);
        // ��ֵ��Ա����
        bt.SetVariable("blog", newSharedBlog);
        Debug.Log(sharedBlog.Value.ToString());



    }

    //private void HappyEvent(Transform obj)
    //{
    //    Debug.Log($"{obj.name} ������");
    //    SharedTransform fromPlayerId = (SharedTransform)bt.GetVariable("SendObj");
    //    Debug.Log($"{fromPlayerId.Name} ������");
    //}
}
