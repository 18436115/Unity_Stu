/// <summary>
/// 界面控制类 基类
/// </summary>
public abstract class ViewBaseControl
{

    protected ViewBase m_view; 


    public ViewBaseControl(ViewBase viewInterface)
    {
        m_view = viewInterface;
    }

    public abstract void Init(params object[] param);

}

