using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStartView : ViewBase
{
    private  GameObject m_RootUI;
    private Transform m_Contain;

    private Button registerBtn;
    private Button LoginBtn;
   
    private InputField userNameInput;
    private InputField pwdInput;

    private UIStartViewControl uiStartViewControl;
    protected override void OnInit(params object[] parem)
    {
        uiStartViewControl = (UIStartViewControl)m_control;

        m_RootUI = UITool.FindUIGameObject("UIStartView(Clone)");
        m_Contain = UITool.GetUIComponent<Transform>(m_RootUI, "Contain");

        registerBtn = UITool.GetUIComponent<Button>(m_RootUI, "RegisterBtn");
        LoginBtn = UITool.GetUIComponent<Button>(m_RootUI, "LoginBtn");

       
        userNameInput = UITool.GetUIComponent<InputField>(m_Contain.gameObject, "NameInput");
        userNameInput.onValueChanged.AddListener((value) =>
        {
            uiStartViewControl.userName = value;
        });
        pwdInput = UITool.GetUIComponent<InputField>(m_Contain.gameObject, "PwdInput");
        pwdInput.onValueChanged.AddListener((value) =>
        {
            uiStartViewControl.pwd = value;
        });
    }


    protected override void OnStart()
    {

    }


    public override void ButtonAddAction(string name, UnityAction action)
    {
        switch (name)
        {
            case "Register":
                registerBtn.onClick.AddListener(action);
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
