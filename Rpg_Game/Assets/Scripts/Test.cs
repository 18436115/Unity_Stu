using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        ////���������������ת��
        //Item item = new Item { id = 1, name = "��ɰ�" };
        //byte[] buffer = null;
        //using(MemoryStreamTool ms = new MemoryStreamTool()) 
        //{
        //    ms.WriteInt(item.id);
        //    ms.WriteUTF8String(item.name);

        //    buffer = ms.ToArray();
        //}
        //Item item1 = new Item();
        //using (MemoryStreamTool ms = new MemoryStreamTool(buffer))
        //{
        //    item1.id = ms.ReadInt();
        //    item1.name = ms.ReadUTF8String();
        //}
        //Debug.Log("item1.id :"+ item1.id);
        //Debug.Log("item1.name :" + item1.name);




        ////����Http����ͨ��
        //if (!NetWorkHttp.Instance.IsBusy)
        //{
        //    NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/account" + "?id=1", GetCallBack);
        //}
        //if (!NetWorkHttp.Instance.IsBusy)
        //{
        //    JsonData jsonData = new JsonData();
        //    jsonData["type"] = 0;//0 ע�� 1����
        //    jsonData["id"] = 3;
        //    jsonData["name"] = "li";
        //    NetWorkHttp.Instance.SendData("http://127.0.0.1:8080/api/account", PostCallBack, isPost:true,json:jsonData.ToJson());
        //}


        ////����Socket����ͨ��
        //GameManager.Instance.Init();
        //((MsgSystem)GameManager.Instance.GameSystemDic[SystemType.Msg]).AddNetEvent(ProtoCodeDef.User_LoginProto, LoginEvent);

        //NetWorkSocket.Instance.Connect("127.0.0.1", 9999);

        ////����Excel���ݼ���
        //UIViewPathInfoEntity uIViewPathInfoEntity = UIViewPathInfoDBModel.Instance.Get(1);
        //Debug.Log(uIViewPathInfoEntity.uiPath);


        ////����ab������
        ////ͨ��·����ӡ
        //Debug.Log("Application.persistentDataPath:" + Application.persistentDataPath.ToString());
        //Debug.Log("Application.streamingAssetsPath:" + Application.streamingAssetsPath.ToString());
        //Debug.Log("Application.dataPath:" + Application.dataPath.ToString());
        //Debug.Log("Application.temporaryCachePath:" + Application.temporaryCachePath.ToString());
        ////ͬ������
        //AssetBundleMgr.Instance.LoadClone(@"gameres/role/player.assetbundle", "Player");
        ////�첽����
        //AssetBundleLoaderAsync assetBundleLoaderAsync = AssetBundleMgr.Instance.LoadAsync(@"gameres/role/player.assetbundle", "Player");
        //assetBundleLoaderAsync.OnLoadComplete = OnLoadComplete;



        //�����ļ�
        //DownloadMgr.Instance.InitCheckVersion();
    }

    #region �����������ת��
    /// <summary>
    /// ���ݲ�����
    /// </summary>
    public class Item
    {
        public int id;
        public string name;
    }
    #endregion

    #region  Http
    private void GetCallBack(NetWorkHttp.CallBackArgs obj)
    {
        Debug.Log(obj.Value);
    }

    private void PostCallBack(NetWorkHttp.CallBackArgs obj)
    {
        Debug.Log(obj.Value);
    }
    #endregion

    #region Socket
    /// <summary>
    /// ������Ӧ�¼�
    /// </summary>
    /// <param name="Buffer"></param>
    private void LoginEvent(byte[] Buffer) 
    {
        User_LoginProto user_LoginProto = User_LoginProto.GetProto(Buffer);
        Debug.Log("Id = " + user_LoginProto.id);
        Debug.Log("username = " + user_LoginProto.username);
    }
    /// <summary>
    /// �����ַ������ݲ���
    /// </summary>
    /// <param name="msg"></param>
    private void Send(string msg)
    {
        using (MemoryStreamTool ms = new MemoryStreamTool())
        {
            ms.WriteUTF8String(msg);
            NetWorkSocket.Instance.SendMsg(ms.ToArray());
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {// ����Э�����
            User_LoginProto user_LoginProto = new User_LoginProto();
            user_LoginProto.id = 1000;
            user_LoginProto.username = "��ɰ�";
            NetWorkSocket.Instance.SendMsg(user_LoginProto.ToArray());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Send("Hello World");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < 10; i++)
            {
                Send("Hello World :: " + i);
            }
        }
    }
    #endregion

    #region ABLoad
    /// <summary>
    /// AB���ػص��¼�����
    /// </summary>
    /// <param name="gameObject"></param>
    private void OnLoadComplete(UnityEngine.Object gameObject)
    {
        UnityEngine.Object.Instantiate(gameObject);
    }
    #endregion


}
