using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理 用于管理具体界面
/// </summary>
public class UIManager
{
    /// <summary>
    /// 缓存池 key = 对象name(路径) ,value = 对象
    /// </summary>
    private Dictionary<string, GameObject> m_viewCache = null;
    /// <summary>
    /// 是否正在加载UI
    /// </summary>
    private bool isLoadingView;

    public UIManager()
    {
        m_viewCache = new Dictionary<string, GameObject>();
        isLoadingView = false;
    }






    /// <summary>
    /// 异步获取 UI预制体
    /// </summary>
    /// <param Name="viewPath">UI路径</param>
    /// <param Name="viewType">UI名称</param>
    /// <param Name="view">返回UI对象</param>
    /// <returns></returns>
    public bool GetView(string viewPath,string viewType, System.Action<GameObject> callBack)
    {
        bool result = false;

        if (isLoadingView)
        {//判断是否正在打开界面
            AppDebug.Log(string.Format("正在打开界面:{0}", viewPath));
            return result;
        }



        GameObject viewobj = null;
        if (m_viewCache.ContainsKey(viewPath))
        {//判断界面是否缓存
            viewobj = m_viewCache[viewPath];
            if (viewobj != null)
            {
                AppDebug.Log(string.Format("从缓存中读取的:{0}", viewPath));
                result = true;
                callBack(viewobj);
            }
            return result;

        }

        isLoadingView = true;
        AssetBundleLoaderAsync assetBundleLoaderAsync = AssetBundleMgr.Instance.LoadAsync(viewPath, viewType);
        assetBundleLoaderAsync.OnLoadComplete = (obj) =>
        {
            isLoadingView = false;
            if (obj != null)
            {
                m_viewCache.Add(viewPath, obj as GameObject);
                callBack(obj as GameObject);

            }
        };
        return result;
    }


    /// <summary>
    /// 显示一个View
    /// </summary>
    /// <param Name="view">UI对象</param>
    /// <returns></returns>
    public bool ShowView(GameObject viewObj)
    {
        bool result = false;
        if (viewObj != null)
        {
            viewObj.SetActive(true);
            result = true;
        }
        return result;
    }

    /// <summary>
    /// 关闭 View
    /// </summary>
    /// <param Name="view">关闭的UI对象</param>
    /// <returns></returns>
    public bool CloseView(GameObject viewObj)
    {
        bool result = false;
        if (viewObj != null) 
        {
            viewObj.SetActive(false);
            result = true;
        }
        return result;
    }



    public void Release()
    {
        m_viewCache.Clear();
        m_viewCache = null;
    }

}

