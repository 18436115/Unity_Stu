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



    //1,�༭�������¼���GameResĿ¼�е���Դ
    //Assets/GameRes/Cube.prefab
    public void LoadInEditor() 
    {
        //using UnityEditor;
#if UNITY_EDITOR
        string resPath = "Assets/GameRes/Cube.prefab";
        //����Ԥ��
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(resPath);
        //ʵ����Ԥ��
        GameObject go = Instantiate<GameObject>(prefab);
#endif
    }



    //2,����ResourcesĿ¼�е���Դ
    //Assets/Resources/Cube.prefab
    public GameObject LoadInResources(string name) 
    {
        //����Ԥ�裬ע�ⲻ�ô�.prefab��׺
        GameObject prefab = Resources.Load<GameObject>(name);
        //ʵ����Ԥ��
        return Instantiate<GameObject>(prefab);

        ////����Ԥ�裬ע�ⲻ�ô�.prefab��׺
        //GameObject prefab = Resources.Load<GameObject>("Cube");
        ////ʵ����Ԥ��
        //GameObject go = Instantiate<GameObject>(prefab);
    }


    //3,AssetBundle����  �ƶ�ƽ̨����StreamingAssetsĿ¼�е���Դ
    //(1)AssetBundle.LoadFromFileͬ�����أ��Ƽ���
    public void LoadAB()
    {
        //����AssetBundle
        string abResPath = Path.Combine(Application.streamingAssetsPath, "obj");
        AssetBundle ab = AssetBundle.LoadFromFile(abResPath);
        //����Asset
        GameObject prefab = ab.LoadAsset<GameObject>("Cube");
        GameObject prefab2 = ab.LoadAsset<GameObject>("Sphere");
        //ʵ����
        GameObject cube = Instantiate<GameObject>(prefab);
        GameObject sphere = Instantiate<GameObject>(prefab2);

    }
    //(2)UnityWebRequest�첽���أ�֧�ַ�����������Դ���أ�
    public void LoadABasyc() 
    {
        StartCoroutine(LoadAsset());
    }
    IEnumerator LoadAsset()
    {
        //ע�⣺����Ŀ¼��Ҫ����"file://"
        //string uri = "file://" + Application.streamingAssetsPath + "/obj";
        string uri = "file://" + "D:/Unity_Stu/Rpg_Game/Hot/obj";

        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.LogError(request.error);
        }
        //��ȡ��ab��
        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
        //����Asset
        GameObject prefab = ab.LoadAsset<GameObject>("Sphere");
        //ʵ����
        GameObject go = Instantiate<GameObject>(prefab);
    }
    //(3)WWW�첽���أ�֧�ַ�����������Դ���أ��ѹ�ʱ��
    public void LoadABwww() 
    {
        StartCoroutine(LoadAssetOld());
    }
    IEnumerator LoadAssetOld()
    {
        //ע�⣺����Ŀ¼��Ҫ����"file://"
        string uri = "file://" + Application.streamingAssetsPath + "/3dprefabs";
        WWW www = new WWW(uri);
        yield return www;
        //��ȡ��ab��
        AssetBundle ab = www.assetBundle;
        //����Asset
        GameObject prefab = ab.LoadAsset<GameObject>("Cube");
        //ʵ����
        GameObject go = Instantiate<GameObject>(prefab);
    }

}
