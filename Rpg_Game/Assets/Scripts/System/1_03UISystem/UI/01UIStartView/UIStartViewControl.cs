using LitJson;
using System;
using System.Collections.Generic;

public class UIStartViewControl : ViewBaseControl
{
    public string userName = "";
    public string pwd = "";


    private UIStartView uiStartView;
    public UIStartViewControl(UIStartView viewInterface) : base(viewInterface)
    {
        uiStartView = (UIStartView)m_view;
    }



    public override void Init(params object[] parem)
    {
        AppDebug.Log("UIStartViewControl:Init");
        uiStartView.ButtonAddAction("Register", OnClickRegisterBtn);
        uiStartView.ButtonAddAction("Login", OnClickLoginBtn);
    }
    /// <summary>
    /// ����
    /// </summary>
    private void OnClickLoginBtn()
    {
        //Dictionary<string, object> _dic = new Dictionary<string, object>();
        //_dic["Type"] = 0;//��ȡ������ҳǩ
        //_dic["pageIndex"] = 1;
        //NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/GameServer", OnSeverPageCallBack, isPost: true, dic: _dic);


        if (userName == "" || pwd == "")
        {
            AppDebug.Log("�������˻�������Ϊ�գ�����");
            return;
        }

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 1;//0 ע�� 1����
        dic["UserName"] = userName;
        dic["Pwd"] = pwd;

        NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/Account", OnLoginCallBack, isPost: true, dic: dic);

        AppDebug.Log("����");
    }



    /// <summary>
    /// ע��
    /// </summary>
    private void OnClickRegisterBtn()
    {
        //Dictionary<string, object> _dic = new Dictionary<string, object>();
        //_dic["Type"] = 1;//��ȡ������
        //_dic["pageIndex"] = 1;
        //NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/GameServer", OnSeverCallBack, isPost: true, dic: _dic);


        if (userName == "" || pwd == "") 
        {
            AppDebug.Log("�������˻�������Ϊ�գ�����");
            return;
        }

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 0;//0 ע�� 1����
        dic["UserName"] = userName;
        dic["Pwd"] = pwd;
        dic["ChannelId"] = 0;

        NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/Account", OnRegisterCallBack, isPost: true, dic:dic);

        AppDebug.Log("ע��");
    }



    private void OnSeverCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            AppDebug.Log(obj.ErrorMsg);
        }
        else
        {
            RetValue retValue = JsonMapper.ToObject<RetValue>(obj.Value);
            if (retValue.HasError)
            {
                AppDebug.Log(retValue.ErrorMsg);
            }
            else
            {
                List<RetGameServerEntity> data = JsonMapper.ToObject<List<RetGameServerEntity>>(retValue.Value.ToString());
                AppDebug.Log("��ȡ�ɹ�");
                AppDebug.Log("ID:" + obj.Value);
                for (int i = 0; i < data.Count; i++)
                {
                    AppDebug.Log("Name:" + data[i].Name);
                    AppDebug.Log("ID:" + data[i].Id);
                    AppDebug.Log("IP:" + data[i].Ip);
                    AppDebug.Log("Port:" + data[i].Port);
                    AppDebug.Log("RunStatus:" + data[i].RunStatus);
                }

            }
        }
    }


    private void OnSeverPageCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            AppDebug.Log(obj.ErrorMsg);
        }
        else
        {
            RetValue retValue = JsonMapper.ToObject<RetValue>(obj.Value);
            if (retValue.HasError)
            {
                AppDebug.Log(retValue.ErrorMsg);
            }
            else
            {
                List<RetGameServerPageEntity> data = JsonMapper.ToObject<List<RetGameServerPageEntity>>(retValue.Value.ToString());
                AppDebug.Log("��ȡ�ɹ�");
                AppDebug.Log("ID:" + obj.Value);
                for (int i=0;i< data.Count;i++) 
                {
                    AppDebug.Log("Name:" + data[i].Name);
                    AppDebug.Log("PageIndex:" + data[i].PageIndex);
                }
                
            }
        }
    }


    /// <summary>
    /// ��������ص�
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoginCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            AppDebug.Log(obj.ErrorMsg);
        }
        else
        {
            RetValue retValue = JsonMapper.ToObject<RetValue>(obj.Value);
            if (retValue.HasError)
            {
                AppDebug.Log(retValue.ErrorMsg);
            }
            else
            {
                AppDebug.Log("����ɹ�");
                RetUserInfoEntity retUserInfo = JsonMapper.ToObject<RetUserInfoEntity>(retValue.Value.ToString());
                AppDebug.Log("retUserInfo.Id:" + retUserInfo.Id);
                AppDebug.Log("retUserInfo.Money:" + retUserInfo.Money);
                AppDebug.Log("retUserInfo.LastLogOnServerId:" + retUserInfo.LastLogOnServerId);
                AppDebug.Log("retUserInfo.LastLogOnServerName:" + retUserInfo.LastLogOnServerName);
                AppDebug.Log("retUserInfo.LastLogOnRoleId:" + retUserInfo.LastLogOnRoleId);
                AppDebug.Log("retUserInfo.LastLogOnRoleJobId:" + retUserInfo.LastLogOnRoleJobId);
                AppDebug.Log("retUserInfo.LastLogOnRoleNickName:" + retUserInfo.LastLogOnRoleNickName);
                if (retUserInfo.LastLogOnServerId == 0)
                {
                    ((UISystem)GameManager.Instance.GameSystemDic[SystemType.UI]).OpenView(4, retUserInfo);
                    ((UISystem)GameManager.Instance.GameSystemDic[SystemType.UI]).CloseView(3);
                }
                else 
                {
                    ((UISystem)GameManager.Instance.GameSystemDic[SystemType.UI]).OpenView(4, retUserInfo);
                    ((UISystem)GameManager.Instance.GameSystemDic[SystemType.UI]).CloseView(3);
                }

            }
        }
    }
    /// <summary>
    /// ע������ص�
    /// </summary>
    /// <param name="obj"></param>
    private void OnRegisterCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            AppDebug.Log(obj.ErrorMsg);
        }
        else 
        {
            RetValue retValue = JsonMapper.ToObject<RetValue>(obj.Value);
            if (retValue.HasError)
            {
                AppDebug.Log(retValue.ErrorMsg);
            }
            else 
            {
                AppDebug.Log("ע��ɹ�");
                RetUserInfoEntity retUserInfo = JsonMapper.ToObject<RetUserInfoEntity>(retValue.Value.ToString());
                AppDebug.Log("retUserInfo.Id:" + retUserInfo.Id);
                AppDebug.Log("retUserInfo.Money:" + retUserInfo.Money);
                AppDebug.Log("retUserInfo.LastLogOnServerId:" + retUserInfo.LastLogOnServerId);
                AppDebug.Log("retUserInfo.LastLogOnServerName:" + retUserInfo.LastLogOnServerName);
                AppDebug.Log("retUserInfo.LastLogOnRoleId:" + retUserInfo.LastLogOnRoleId);
                AppDebug.Log("retUserInfo.LastLogOnRoleJobId:" + retUserInfo.LastLogOnRoleJobId);
                AppDebug.Log("retUserInfo.LastLogOnRoleNickName:" + retUserInfo.LastLogOnRoleNickName);


                //����ѡ�ǽ���
            }
        }
    }
}
