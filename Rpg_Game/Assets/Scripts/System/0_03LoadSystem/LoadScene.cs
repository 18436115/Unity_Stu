using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ≥°æ∞º”‘ÿ¿‡
/// </summary>
public class LoadScene : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }


    private static LoadScene instance;
    public static LoadScene Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject("LoadScene");
                if (gameObject.GetComponent<LoadSystem>() == null)
                {
                    instance = gameObject.AddComponent<LoadScene>();
                }
                else
                {
                    instance = gameObject.GetComponent<LoadScene>();
                }
            }
            return instance;
        }
    }


    private Coroutine LoadCoro;



    public void Load(string sceneName,System.Action LoadCallBack) 
    {
        if (LoadCoro != null)
        {
            StopCoroutine(LoadCoro);
        }
        LoadCoro = StartCoroutine(AsynLoadScene(sceneName, LoadCallBack));
    }

    IEnumerator AsynLoadScene(string sceneName, System.Action LoadCallBack)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress:" + (progress * 100) + "%");
            if (Mathf.Approximately(ao.progress, 0.9f))
            {
                Debug.Log("Almost loaded!");
                ao.allowSceneActivation = true;
            }
            yield return null;
        }

        if (ao.allowSceneActivation) 
        {
            LoadCallBack.Invoke();
        }
    }
}
