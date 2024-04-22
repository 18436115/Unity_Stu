//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-03-21 23:01:44
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 同步加载资源包
/// </summary>
public class AssetBundleLoader : IDisposable
{
    private AssetBundle bundle;

    public AssetBundleLoader(string assetBundlePath, bool isFullPath)
    {
        string fullPath = isFullPath ? assetBundlePath : LocalFileMgr.Instance.LocalFilePath + assetBundlePath;
        bundle = AssetBundle.LoadFromMemory(LocalFileMgr.Instance.GetBuffer(fullPath));
    }

    public T LoadAsset<T>(string name) where T : UnityEngine.Object
    {
        if (bundle == null) return default(T);
        return bundle.LoadAsset(name) as T;
    }

    public UnityEngine.Object LoadAsset(string name)
    {
        return bundle.LoadAsset(name);
    }

    public UnityEngine.Object[] LoadAllAssets()
    {
        return bundle.LoadAllAssets();
    }

    public void Dispose()
    {
        if (bundle != null) bundle.Unload(false);
    }
}