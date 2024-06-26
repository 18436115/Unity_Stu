//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-03-21 22:45:43
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// 本地文件管理 -获取AB文件数据
/// </summary>
public class LocalFileMgr : Singleton<LocalFileMgr>
{

#if DEBUG_MODEL
     public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Windows/";
#elif DEBUG_REAL
      public readonly string LocalFilePath = Application.persistentDataPath + "/";
#endif


    /// <summary>
    /// 读取本地文件到byte数组
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] GetBuffer(string path)
    {
        byte[] buffer = null;

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}