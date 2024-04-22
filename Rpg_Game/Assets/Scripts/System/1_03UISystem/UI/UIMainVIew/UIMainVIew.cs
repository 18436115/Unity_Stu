using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIMainVIew : ViewBase
{
    public override void ButtonAddAction(string name, UnityAction action)
    {
        switch (name)
        {
            case "Close":
                
                break;
            default:
                Debug.LogErrorFormat("非法添加开关事件{0}->{1}", name, action.ToString());
                break;
        }
    }

    protected override void OnInit(params object[] parem)
    {
        
    }
    protected override void OnStart()
    {

    }
}
