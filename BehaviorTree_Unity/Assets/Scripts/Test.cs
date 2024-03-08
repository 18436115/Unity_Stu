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
        // 获取行为树对象
        bt = GetComponent<BehaviorTree>();

        //bt.RegisterEvent<Transform>("Happy_Event", HappyEvent);
        //bt.SendEvent("EAT_EVENT");

        // 访问成员变量-----------------------------------------
        var sharedBlog = (SharedBlog)bt.GetVariable("blog");
        Debug.Log(sharedBlog.Value.ToString());

        // 修改变量值-----------------------------------------
        sharedBlog.Value.author = "Unity3D技术博主：林新发";
        bt.SetVariable("blog", sharedBlog);


        // 构建新的成员对象-----------------------------------------
        SharedBlog newSharedBlog = new SharedBlog();
        Blog newBlog = new Blog();
        newBlog.author = "博客技术专家：林新发";
        newBlog.url = "https://blog.csdn.net/linxinfa";
        newSharedBlog.SetValue(newBlog);
        // 赋值成员变量
        bt.SetVariable("blog", newSharedBlog);
        Debug.Log(sharedBlog.Value.ToString());



    }

    //private void HappyEvent(Transform obj)
    //{
    //    Debug.Log($"{obj.name} 打了我");
    //    SharedTransform fromPlayerId = (SharedTransform)bt.GetVariable("SendObj");
    //    Debug.Log($"{fromPlayerId.Name} 打了我");
    //}
}
