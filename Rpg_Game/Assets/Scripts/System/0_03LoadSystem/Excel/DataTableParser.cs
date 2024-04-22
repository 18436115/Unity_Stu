using System;
using System.Collections.Generic;

/// <summary>
/// 解析data数据
/// </summary>
public class DataTableParser : IDisposable
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path"></param>
    public DataTableParser(string path)
    {
        //-------------------------------
        // 第一步 读取文件
        //------------------------------
        byte [] buffer = FileHelper.GetByte(path);

        //-------------------------------
        // 第二步 解压缩
        //------------------------------
        buffer = ZlibHelper.DeCompressBytes(buffer);

        //-------------------------------
        // 第三步 xor 解密
        //------------------------------
        buffer = SecurityUtil.Xor(buffer);

        //-------------------------------
        // 第四步 读取byte数据
        //------------------------------
        using (MemoryStreamTool ms = new MemoryStreamTool(buffer))
        {
            //前面两个数是 行 列
            m_Row = ms.ReadInt();
            m_Column = ms.ReadInt();

            m_FieldName = new string[m_Column];
            m_FieldNameDic = new Dictionary<string, int>();
            m_GameData = new string[m_Row, m_Column];


            for (int i = 0; i < m_Row; i++)
            {
                for (int j = 0; j < m_Column; j++)
                {
                    string str = ms.ReadUTF8String();

                    if (i == 0)
                    {   //第一行 字段名
                        m_FieldName[j] = str;
                        m_FieldNameDic[str] = j;
                    }

                    else if (i > 2)
                    {   //实际内容
                        m_GameData[i, j] = str;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 文件行数
    /// </summary>
    private int m_Row;
    /// <summary>
    /// 文件列数
    /// </summary>
    private int m_Column;

    /// <summary>
    /// 字段名
    /// </summary>
    private string[] m_FieldName;

    /// <summary>
    /// key 字段名 value 数据列号
    /// </summary>
    private Dictionary<string, int> m_FieldNameDic;

    /// <summary>
    /// 游戏数据
    /// </summary>
    private string[,] m_GameData;

    #region  读取数据部分
    /// <summary>
    /// 当前读取的行号 第三行开始表示数据
    /// </summary>
    private int m_CurRowNo = 3;

    /// <summary>
    /// 字段名称
    /// </summary>
    public string[] FieldName
    {
        get { return m_FieldName; }
    }

    /// <summary>
    /// 判断数据是不是读取完了
    /// </summary>
    public bool Eof
    {
        get
        {
            return m_CurRowNo == m_Row;
        }
    }

    /// <summary>
    /// 进入下一条数据
    /// </summary>
    public void Next()
    {
        if (Eof) return;
        m_CurRowNo++;
    }

    /// <summary>
    /// 根据字段名获取数据内容
    /// </summary>
    /// <param name="filedName"></param>
    /// <returns></returns>
    public string GetFileValue(string filedName)
    {
        try
        {
            if (m_CurRowNo < 3 || m_CurRowNo >= m_Row) return null;
            return m_GameData[m_CurRowNo, m_FieldNameDic[filedName]];
        }catch
        {
            return null;
        }
    }
    #endregion

    #region  Dispose 释放
    public void Dispose()
    {
        m_FieldNameDic.Clear();
        m_FieldNameDic = null;

        m_FieldName = null;
        m_GameData = null;
    }
    #endregion
}