using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILoginView : ViewBase
{
    private GameObject m_RootUI;
    private Transform m_Contain;

    private Button selectBtn;
    private Button LoginBtn;

    private Text serverNameTxt;


    private UILoginViewControl uiLoginViewControl;
    private RetUserInfoEntity retUserInfo;

    protected override void OnInit(params object[] param)
    {
        uiLoginViewControl = (UILoginViewControl)m_control;

        m_RootUI = UITool.FindUIGameObject("UILoginView(Clone)");
        m_Contain = UITool.GetUIComponent<Transform>(m_RootUI, "Contain");

        LoginBtn = UITool.GetUIComponent<Button>(m_RootUI, "LoginBtn");
        selectBtn = UITool.GetUIComponent<Button>(m_Contain.gameObject, "selectBtn");

        serverNameTxt = UITool.GetUIComponent<Text>(m_Contain.gameObject, "ServerName");

        if (param != null && param.Length > 0) 
        {
            retUserInfo = (RetUserInfoEntity)param[0];
        }
    }

    protected override void OnStart()
    {
        if (retUserInfo.LastLogOnServerId != 0)
        {
            serverNameTxt.text = retUserInfo.LastLogOnServerName;
            AppDebug.Log("retUserInfo.LastLogOnServerName:" + retUserInfo.LastLogOnServerName);
        }
        else 
        {
            serverNameTxt.text = "推荐服";
        }

    }



    public override void ButtonAddAction(string name, UnityAction action)
    {
        switch (name)
        {
            case "Select":
                selectBtn.onClick.AddListener(action);
                break;
            case "Login":
                LoginBtn.onClick.AddListener(action);
                break;
            default:
                Debug.LogErrorFormat("非法添加开关事件{0}->{1}", name, action.ToString());
                break;
        }
    }


}
