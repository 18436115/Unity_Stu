using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UISelectView : ViewBase
{
    private UISelectViewControl uiSelectVIewControl;
    private GameObject m_RootUI;


    private List<AreaInfo> areaInfos;
    private List<ServerInfo> serverInfos;
    protected override void OnInit(params object[] parem)
    {
        uiSelectVIewControl = (UISelectViewControl)m_control;
        areaInfos = new List<AreaInfo>();
        serverInfos = new List<ServerInfo>();
        m_RootUI = UITool.FindUIGameObject("UISelectView(Clone)");
        for (int i = 0; i < 10; i++)
        {
            ServerInfo serverInfo = new ServerInfo();
            serverInfo.trans = UITool.GetUIComponent<Transform>(m_RootUI, "ServerInfo (" + (i + 1) + ")");
            serverInfo.trans.gameObject.SetActive(false);
            serverInfo.OnClickBtn = uiSelectVIewControl.OnClickSelectBtn;
            serverInfo.Init();
            serverInfos.Add(serverInfo);

            AreaInfo areaInfo = new AreaInfo();
            areaInfo.trans = UITool.GetUIComponent<Transform>(m_RootUI, "AreaInfo (" + (i + 1) + ")");
            areaInfo.trans.gameObject.SetActive(false);
            areaInfo.OnClickBtn = uiSelectVIewControl.OnClickPageBtn;
            areaInfo.Init();
            areaInfos.Add(areaInfo);
        }

    }
    protected override void OnStart()
    {




    }





    public override void ButtonAddAction(string name, UnityAction action)
    {

    }

    /// <summary>
    /// 设置页签
    /// </summary>
    public void SetUIPage(List<RetGameServerPageEntity> pageLst) 
    {
        for (int i = 0; i < areaInfos.Count; i++) 
        {
            areaInfos[i].trans.gameObject.SetActive(false);
        }

        if (pageLst != null && pageLst.Count > 0) 
        {
            for (int i = 0; i < pageLst.Count; i++) 
            {
                areaInfos[i].trans.gameObject.SetActive(true);
                areaInfos[i].SetUI(pageLst[i]);
            }
        }
    }
    /// <summary>
    /// 设置服务器
    /// </summary>
    public void SetUIServer(List<RetGameServerEntity> serverLst)
    {
        for (int i = 0; i < serverInfos.Count; i++)
        {
            serverInfos[i].trans.gameObject.SetActive(false);
        }

        if (serverLst != null && serverLst.Count > 0)
        {
            for (int i = 0; i < serverLst.Count; i++)
            {
                serverInfos[i].trans.gameObject.SetActive(true);
                serverInfos[i].SetUI(serverLst[i]);
            }
        }
    }

}
