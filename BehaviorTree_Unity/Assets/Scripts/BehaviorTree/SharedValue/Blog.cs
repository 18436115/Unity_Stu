using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Blog
{
    /// <summary>
    /// ����
    /// </summary>
    public string author;

    /// <summary>
    /// ���͵�ַ
    /// </summary>
    public string url;

    public override string ToString()
    {
        return $"author: {author}, url: {url}";
    }
}

