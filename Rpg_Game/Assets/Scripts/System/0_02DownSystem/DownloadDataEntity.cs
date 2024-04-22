//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-03-09 21:02:29
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 下载数据的实体
/// </summary>
public class DownloadDataEntity 
{
    /// <summary>
    /// 资源的名称
    /// </summary>
    public string FullName;

    /// <summary>
    /// MD5
    /// </summary>
    public string MD5;

    /// <summary>
    /// 文件大小（K）
    /// </summary>
    public int Size;

    /// <summary>
    /// 是否初始数据
    /// </summary>
    public bool IsFirstData;
}