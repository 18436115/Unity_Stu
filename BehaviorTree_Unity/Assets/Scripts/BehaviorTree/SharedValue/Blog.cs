using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 博客
/// </summary>
public class Blog
{
    /// <summary>
    /// 作者
    /// </summary>
    public string author;

    /// <summary>
    /// 博客地址
    /// </summary>
    public string url;

    public override string ToString()
    {
        return $"author: {author}, url: {url}";
    }
}

