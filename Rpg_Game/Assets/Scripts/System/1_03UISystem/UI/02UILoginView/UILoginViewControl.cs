using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginViewControl : ViewBaseControl
{
    private UILoginView uiLoginView;
    private RetUserInfoEntity retUserInfo;
    public UILoginViewControl(UILoginView viewInterface) : base(viewInterface) 
    {
        uiLoginView = (UILoginView)m_view;
    }

    public override void Init(params object[] param)
    {
        if (param != null && param.Length >0 ) 
        {
            retUserInfo = (RetUserInfoEntity)param[0];
        }

        uiLoginView.ButtonAddAction("Select",OnSelectOnClick);
        uiLoginView.ButtonAddAction("Login", OnLoginOnClick);
    }

    /// <summary>
    /// ���밴ť��Ӧ�¼�
    /// </summary>
    private void OnLoginOnClick()
    {
        //����ѡ�ǽ���
        NetWorkSocket.Instance.ConnectSuccessEvent = OnConnect;
        NetWorkSocket.Instance.Connect("127.0.0.1", 9999);
        
    }

    private void OnConnect()
    {
        AppDebug.Log("ConnectSuccessEvent:");
        RoleOperation_LogOnGameServerProto roleOperation_LogOnGameServerProto = new RoleOperation_LogOnGameServerProto();
        roleOperation_LogOnGameServerProto.AccountId = retUserInfo.Id;
        NetWorkSocket.Instance.SendMsg(roleOperation_LogOnGameServerProto.ToArray());
    }


    /// <summary>
    /// ѡ����ť��Ӧ�¼�
    /// </summary>
    private void OnSelectOnClick()
    {
        ((UISystem)GameManager.Instance.GameSystemDic[SystemType.UI]).OpenView(5, retUserInfo);
    }






}
