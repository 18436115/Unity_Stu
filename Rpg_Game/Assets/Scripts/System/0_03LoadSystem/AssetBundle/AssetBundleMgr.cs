using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// AB资源加载管理器
/// </summary>
public class AssetBundleMgr : Singleton<AssetBundleMgr>
{
    /// <summary>
    /// 同步加载克隆
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadClone(string path, string name)
    {
        //是否非AB模式加载
        bool isNotABLoad = false;
        using (AssetBundleLoader loader = new AssetBundleLoader(path, isNotABLoad))
        {
            GameObject obj = loader.LoadAsset<GameObject>(name);
            return Object.Instantiate(obj);
        }
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public AssetBundleLoaderAsync LoadAsync(string path, string name)
    {
        GameObject obj = new GameObject("AssetBundleLoadAsync");
        AssetBundleLoaderAsync async = obj.GetOrCreatComponent<AssetBundleLoaderAsync>();
        async.Init(path, name);
        return async;
    }
}