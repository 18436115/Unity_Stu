using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI���� ���ڹ���������
/// </summary>
public class UIManager
{
    /// <summary>
    /// ����� key = ����name(·��) ,value = ����
    /// </summary>
    private Dictionary<string, GameObject> m_viewCache = null;
    /// <summary>
    /// �Ƿ����ڼ���UI
    /// </summary>
    private bool isLoadingView;

    public UIManager()
    {
        m_viewCache = new Dictionary<string, GameObject>();
        isLoadingView = false;
    }






    /// <summary>
    /// �첽��ȡ UIԤ����
    /// </summary>
    /// <param Name="viewPath">UI·��</param>
    /// <param Name="viewType">UI����</param>
    /// <param Name="view">����UI����</param>
    /// <returns></returns>
    public bool GetView(string viewPath,string viewType, System.Action<GameObject> callBack)
    {
        bool result = false;

        if (isLoadingView)
        {//�ж��Ƿ����ڴ򿪽���
            AppDebug.Log(string.Format("���ڴ򿪽���:{0}", viewPath));
            return result;
        }



        GameObject viewobj = null;
        if (m_viewCache.ContainsKey(viewPath))
        {//�жϽ����Ƿ񻺴�
            viewobj = m_viewCache[viewPath];
            if (viewobj != null)
            {
                AppDebug.Log(string.Format("�ӻ����ж�ȡ��:{0}", viewPath));
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
    /// ��ʾһ��View
    /// </summary>
    /// <param Name="view">UI����</param>
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
    /// �ر� View
    /// </summary>
    /// <param Name="view">�رյ�UI����</param>
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

