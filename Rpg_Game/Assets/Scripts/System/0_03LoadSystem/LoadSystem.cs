using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class LoadSystem : MonoBehaviour
{
    private static LoadSystem instance;
    public static LoadSystem Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject("LoadSystem");
                if (gameObject.GetComponent<LoadSystem>() == null)
                {
                    instance = gameObject.AddComponent<LoadSystem>();
                }
                else
                {
                    instance = gameObject.GetComponent<LoadSystem>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }



    public GameObject LoadView(string viewName) 
    {
        return LoadInResources(viewName);
    }



    //1,编辑器环境下加载GameRes目录中的资源
    //Assets/GameRes/Cube.prefab
    public void LoadInEditor() 
    {
        //using UnityEditor;
#if UNITY_EDITOR
        string resPath = "Assets/GameRes/Cube.prefab";
        //加载预设
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(resPath);
        //实例化预设
        GameObject go = Instantiate<GameObject>(prefab);
#endif
    }



    //2,加载Resources目录中的资源
    //Assets/Resources/Cube.prefab
    public GameObject LoadInResources(string name) 
    {
        //加载预设，注意不用带.prefab后缀
        GameObject prefab = Resources.Load<GameObject>(name);
        //实例化预设
        return Instantiate<GameObject>(prefab);

        ////加载预设，注意不用带.prefab后缀
        //GameObject prefab = Resources.Load<GameObject>("Cube");
        ////实例化预设
        //GameObject go = Instantiate<GameObject>(prefab);
    }


    //3,AssetBundle加载  移动平台加载StreamingAssets目录中的资源
    //(1)AssetBundle.LoadFromFile同步加载（推荐）
    public void LoadAB()
    {
        //加载AssetBundle
        string abResPath = Path.Combine(Application.streamingAssetsPath, "obj");
        AssetBundle ab = AssetBundle.LoadFromFile(abResPath);
        //加载Asset
        GameObject prefab = ab.LoadAsset<GameObject>("Cube");
        GameObject prefab2 = ab.LoadAsset<GameObject>("Sphere");
        //实例化
        GameObject cube = Instantiate<GameObject>(prefab);
        GameObject sphere = Instantiate<GameObject>(prefab2);

    }
    //(2)UnityWebRequest异步加载（支持服务器上在资源加载）
    public void LoadABasyc() 
    {
        StartCoroutine(LoadAsset());
    }
    IEnumerator LoadAsset()
    {
        //注意：本地目录需要加上"file://"
        //string uri = "file://" + Application.streamingAssetsPath + "/obj";
        string uri = "file://" + "D:/Unity_Stu/Rpg_Game/Hot/obj";

        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        //获取到ab包
        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
        //加载Asset
        GameObject prefab = ab.LoadAsset<GameObject>("Sphere");
        //实例化
        GameObject go = Instantiate<GameObject>(prefab);
    }
    //(3)WWW异步加载（支持服务器上在资源加载，已过时）
    public void LoadABwww() 
    {
        StartCoroutine(LoadAssetOld());
    }
    IEnumerator LoadAssetOld()
    {
        //注意：本地目录需要加上"file://"
        string uri = "file://" + Application.streamingAssetsPath + "/3dprefabs";
        WWW www = new WWW(uri);
        yield return www;
        //获取到ab包
        AssetBundle ab = www.assetBundle;
        //加载Asset
        GameObject prefab = ab.LoadAsset<GameObject>("Cube");
        //实例化
        GameObject go = Instantiate<GameObject>(prefab);
    }

}
