using System;
using UnityEngine;
using UnityEngine.UI;

public class ServerInfo 
{
    public Transform trans;

    /// <summary>
    /// 
    /// </summary>
    public Action<RetGameServerEntity> OnClickBtn;


    private Text nameTxt;
    private Button selectBtn;

    private RetGameServerEntity serverInfo;
    public void Init()
    {
        nameTxt = UITool.GetUIComponent<Text>(trans.gameObject, "Text");
        selectBtn = UITool.GetUIComponent<Button>(trans.gameObject, trans.name);
        selectBtn.onClick.AddListener(() =>
        {
            if (serverInfo != null) 
            {
                OnClickBtn(serverInfo);
            }
        });
    }

    public void SetUI(RetGameServerEntity info)
    {
        serverInfo = info;
        nameTxt.text = info.Name;
    }
}
