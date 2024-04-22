using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UISystem : IGameSystem
{
    UIManager uiManager;
    Dictionary<int, GameObject> viewDic;

    UIStartView UIStartView;
    UIStartViewControl UIStartViewControl;

    UILoginView UILoginView;
    UILoginViewControl UILoginViewControl;

    UISelectView UISelectView;
    UISelectViewControl UISelectViewControl;





    UIMainVIew UIMainVIew;
    UIMainVIewControl UIMainVIewControl;

    public UISystem(GameManager gameManager) : base(gameManager)
    {
        m_GameMgr.GameSystemDic.Add(SystemType.UI, this);      
    }

    public override void Init()
    {
        base.Init();
        uiManager = new UIManager();
        viewDic = new Dictionary<int, GameObject>();

        UIStartView = new UIStartView();
        UIStartViewControl = new UIStartViewControl(UIStartView);
        UIStartView.SetViewContol(UIStartViewControl);

        UILoginView = new UILoginView();
        UILoginViewControl = new UILoginViewControl(UILoginView);
        UILoginView.SetViewContol(UILoginViewControl);

        UISelectView = new UISelectView();
        UISelectViewControl = new UISelectViewControl(UISelectView);
        UISelectView.SetViewContol(UISelectViewControl);

        UIMainVIew = new UIMainVIew();
        UIMainVIewControl = new UIMainVIewControl(UIMainVIew);
        UIMainVIew.SetViewContol(UIMainVIewControl);
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    public void OpenView(int viewId, params object[] parem)
    {
        ViewBase viewFunc = GetView(viewId);

        if (viewDic.ContainsKey(viewId))
        {//字典中存有界面
            uiManager.ShowView(viewDic[viewId]);
            viewFunc.Show(parem);
        }
        else
        {
            UIViewPathInfoEntity uIViewPathInfoEntity = UIViewPathInfoDBModel.Instance.Get(viewId);
            

            uiManager.GetView(uIViewPathInfoEntity.uiPath.ToLower(), uIViewPathInfoEntity.uiType.ToLower(), (obj) =>
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(obj);
                Transform view = gameObject.transform;

                GameObject m_RootUI = UITool.FindUIGameObject("UIRoot");
                gameObject.transform.parent = m_RootUI.transform;

                RectTransform viewRTF = view.GetComponent<RectTransform>();
                viewRTF.anchoredPosition = Vector2.zero;
                viewRTF.sizeDelta = Vector2.zero;

                view.transform.localPosition = Vector3.zero;
                view.transform.localScale = Vector3.one;
                //view.transform.localRotation = Quaternion.Euler(Vector3.zero);

                viewDic.Add(viewId, gameObject);
                uiManager.ShowView(viewDic[viewId]);

                viewFunc.Show(parem);
            });
        }

        
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public ViewBase GetView(int viewType) 
    {
        ViewBase view = null;
        switch (viewType)
        {
            case 1:
                view = UIMainVIew;
                break;
            case 2:
                //view = UILoadView;
                break;
            case 3:
                view = UIStartView;
                break;
            case 4:
                view = UILoginView;
                break;
            case 5:
                view = UISelectView;
                break;
            default:
                break;
        }
        return view;
    }



    public void CloseView(int viewId)
    {
        ViewBase viewFunc = GetView(viewId);
        if (viewDic.ContainsKey(viewId))
        {//字典中存有界面
            viewFunc.Hide();
            uiManager.CloseView(viewDic[viewId]);
        }
    }



    public override void Release()
    {
        base.Release();
        uiManager.Release();
        uiManager = null;
        viewDic.Clear();
        viewDic = null;
    }

}
