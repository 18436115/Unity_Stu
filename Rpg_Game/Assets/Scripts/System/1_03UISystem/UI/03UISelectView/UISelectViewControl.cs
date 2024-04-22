using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectViewControl : ViewBaseControl
{
    private UISelectView uiSelectVIew;
    private RetUserInfoEntity retUserInfo;
    public UISelectViewControl(UISelectView viewInterface) : base(viewInterface) 
    {
        uiSelectVIew = (UISelectView)m_view;
    }

    public override void Init(params object[] param)
    {
        if (param != null && param.Length > 0)
        {
            retUserInfo = (RetUserInfoEntity)param[0];
        }

        Dictionary<string, object> _dic = new Dictionary<string, object>();
        _dic["Type"] = 0;//��ȡ������ҳǩ
        NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/GameServer", OnSeverPageCallBack, isPost: true, dic: _dic);

    }


    /// <summary>
    /// ѡ��ҳǩ
    /// </summary>
    public void OnClickPageBtn(int page) 
    {
        Dictionary<string, object> _dic = new Dictionary<string, object>();
        _dic["Type"] = 1;//��ȡ������
        _dic["pageIndex"] = page;
        NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/GameServer", OnSeverCallBack, isPost: true, dic: _dic);
    }
    /// <summary>
    /// ѡ�������
    /// </summary>
    public void OnClickSelectBtn(RetGameServerEntity serverInfo)
    {
        Dictionary<string, object> _dic = new Dictionary<string, object>();
        _dic["Type"] = 2;//��ȡ������
        _dic["userId"] = retUserInfo.Id;
        _dic["lastServerId"] = serverInfo.Id;
        _dic["lastServerName"] = serverInfo.Name;
        //_dic["LastLogOnServerTime"] = DateTime.Now;
        NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/GameServer", OnLoginCallBack, isPost: true, dic: _dic);
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
            }
        }
    }

    /// <summary>
    /// ��ȡ�������ص�
    /// </summary>
    /// <param name="obj"></param>
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
                uiSelectVIew.SetUIServer(data);
                //AppDebug.Log("��ȡ�ɹ�");
                //AppDebug.Log("ID:" + obj.Value);
                //for (int i = 0; i < data.Count; i++)
                //{
                //    AppDebug.Log("Name:" + data[i].Name);
                //    AppDebug.Log("ID:" + data[i].Id);
                //    AppDebug.Log("IP:" + data[i].Ip);
                //    AppDebug.Log("Port:" + data[i].Port);
                //    AppDebug.Log("RunStatus:" + data[i].RunStatus);
                //}

            }
        }
    }


    /// <summary>
    /// ��ȡ������ҳǩ�ص�
    /// </summary>
    /// <param name="obj"></param>
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
                uiSelectVIew.SetUIPage(data);
                //AppDebug.Log("��ȡ�ɹ�");
                //AppDebug.Log("ID:" + obj.Value);
                //for (int i = 0; i < data.Count; i++)
                //{
                //    AppDebug.Log("Name:" + data[i].Name);
                //    AppDebug.Log("PageIndex:" + data[i].PageIndex);
                //}

                Dictionary<string, object> _dic = new Dictionary<string, object>();
                _dic["Type"] = 1;//��ȡ������
                _dic["pageIndex"] = 1;
                NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/GameServer", OnSeverCallBack, isPost: true, dic: _dic);
            }
        }
    }
}
