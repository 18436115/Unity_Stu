using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using LitJson;

/// <summary>
/// Http通讯管理
/// </summary>
public class NetWorkHttp : MonoBehaviour
{
    private static NetWorkHttp instance;
    public static NetWorkHttp Instance 
    {
        get 
        {
            if (instance == null) 
            {
                GameObject gameObject = new GameObject("NetWorkHttp");
                if (gameObject.GetComponent<NetWorkHttp>() == null)
                {
                    instance = gameObject.AddComponent<NetWorkHttp>();
                    GameObject.DontDestroyOnLoad(gameObject);
                }
                else 
                {
                    instance = gameObject.GetComponent<NetWorkHttp>();
                }
            }
            return instance;
        }
    }





    #region 属性
    /// <summary>
    /// Web请求回调
    /// </summary>
    private Action<CallBackArgs>  m_CallBack;

    /// <summary>
    /// Web请求回调数据
    /// </summary>
    private CallBackArgs m_CallBackArgs;

    #endregion



    private bool m_isBusy;
    public bool IsBusy 
    {
        get { return m_isBusy; }
    }



    protected void Start()
    {
        m_CallBackArgs = new CallBackArgs();
        m_isBusy = false;
    }

    #region SendData 发送web数据
    /// <summary>
    /// 发送web数据
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack"></param>
    /// <param name="isPost"></param>
    /// <param name="json"></param>
    public void SendData(string url, Action<CallBackArgs> callBack, bool isPost = false, Dictionary<string, object> dic = null)
    {
        m_CallBack = callBack;
        m_isBusy = true;

        if (!isPost)
        {
            GetUrl(url);
        }
        else
        {
            //web加密
            if (dic != null)
            {
                //客户端标识符
                dic["deviceIdentifier"] = DeviceUtil.DeviceIdentifier;

                //设备型号
                dic["deviceModel"] = DeviceUtil.DeviceModel;

                long t = GameManager.Instance.CurrServerTime;
                AppDebug.Log(t.ToString());
                //签名
                dic["sign"] = EncryptUtil.Md5(string.Format("{0}:{1}", t, DeviceUtil.DeviceIdentifier));

                //时间戳
                dic["t"] = t;
            }

            PostUrl(url, dic == null ? "" : JsonMapper.ToJson(dic));
        }
    }
    #endregion

    #region GetUrl Get请求
    /// <summary>
    /// Get请求
    /// </summary>
    /// <param name="url"></param>
    private void GetUrl(string url)
    {
        WWW data = new WWW(url);
        StartCoroutine(Recive(data));
    }
    #endregion

    #region PostUrl Post请求
    /// <summary>
    /// Post请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    private void PostUrl(string url, string json) 
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("",json);

        WWW data = new WWW(url, wwwForm);
        StartCoroutine(Recive(data));
    }
    #endregion




    IEnumerator Recive(WWW data)
    {
        yield return data;

        m_isBusy = false;
        if (string.IsNullOrEmpty(data.error))
        {
            if (data.text == null)
            {
                if (m_CallBack != null)
                {
                    if (m_CallBack != null)
                    {
                        m_CallBackArgs.HasError = true;
                        m_CallBackArgs.ErrorMsg = "未请求到数据";
                        m_CallBack(m_CallBackArgs);
                    }
                }
            }
            else 
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = false;
                    m_CallBackArgs.Value = data.text;
                    m_CallBack(m_CallBackArgs);
                }
            }


        }
        else
        {
            if (m_CallBack != null)
            {
                m_CallBackArgs.HasError = true;
                m_CallBackArgs.ErrorMsg = data.error;
                m_CallBackArgs.Value = "";
                m_CallBack(m_CallBackArgs);
            }
        }
    }


    #region CallBackArgs Web请求回调数据
    /// <summary>
    /// Web请求回调数据
    /// </summary>
    public class CallBackArgs : EventArgs
    {
        /// <summary>
        /// 是否有错
        /// </summary>
        public bool HasError;

        /// <summary>
        /// 错误原因
        /// </summary>
        public string ErrorMsg;

        /// <summary>
        /// 返回值
        /// </summary>
        public string Value;
    }
    #endregion
}