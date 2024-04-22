using UnityEngine.Events;
/// <summary>
/// 界面基类
/// </summary>
public abstract class ViewBase
{
    public ViewBaseControl m_control = null;

    public void SetViewContol(ViewBaseControl control) 
    {
        m_control = control;
    }


    public void Show(params object[] param)
    {
        AppDebug.Log("界面打开");
        OnInit(param);
        if (m_control != null) 
        {
            m_control.Init(param);
        }

        OnStart();
    }

    public void Hide()
    {
        AppDebug.Log("界面关闭");
    }

    public void Update()
    {
        
    }



    /// <summary>
    /// 界面初始化
    /// </summary>
    protected abstract void OnInit(params object[] param);


    protected abstract void OnStart();

    /// <summary>
    /// 按钮响应事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public abstract void ButtonAddAction(string name, UnityAction action);

}
