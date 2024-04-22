using System;
using UnityEngine;
using UnityEngine.UI;

public class AreaInfo 
{
    public Transform trans;

    /// <summary>
    /// 
    /// </summary>
    public Action<int> OnClickBtn;


    private Text nameTxt;
    private Button selectBtn;

    /// <summary>
    /// ҳǩ
    /// </summary>
    private int page;
    public void Init() 
    {
        nameTxt = UITool.GetUIComponent<Text>(trans.gameObject, "Text");
        selectBtn = UITool.GetUIComponent<Button>(trans.gameObject, trans.name);
        selectBtn.onClick.AddListener(()=> 
        {
            OnClickBtn(page);
        });
    }

    public void SetUI(RetGameServerPageEntity info)
    {
        nameTxt.text = info.Name;
        page = info.PageIndex;
    }
}
