using System;
using System.IO;
/// <summary>
/// 主要用到的文件操作  创建 写入 读取 拷贝 删除
/// </summary>
public class FileHelper{

    #region  下面用到的变量说明
    //  dirPath 是文件目录  fileName 是文件名  extension 是文件后缀名
    //  path是文件的路径 包含 目录+文件名
    //  path = dirPath + fileName    
    //  filePath 是文件的全路径 包含 目录+文件名+文件后缀名
    //  filePath = path+ extension   
    //
    //  Path.GetDirectoryName(filePath) 获取文件夹目录
    //  Path.GetFileName(filePath) 获取文件名
    #endregion

    #region 创建
    /// <summary>
    /// 创建目录  有目录不做处理 没有的话创建
    /// </summary>
    /// <param Name="path">路径</param>
    /// <param Name="isfilePath">是否是文件路径</param>
    public static void CreateDirectory(string path,bool isfilePath= true)
    {
        string dirPath = string.Empty;
        if (isfilePath)
        {
            dirPath = Path.GetDirectoryName(path); 
        }
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
    }
    /// <summary>
    /// 创建文件  有则不做处理 没有的话创建
    /// </summary>
    /// <param Name="filePath"></param>
    public static void CreateFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
    }
    #endregion

    #region 写入(覆盖)数据到文件中
    /// <summary>
    /// 写入数据到文件中
    /// </summary>
    /// <param Name="data">数据</param>
    /// <param Name="filePath">文件全路径</param>
    public static void WriteFile(string data, string filePath)
    {
        CreateDirectory(filePath);

        var streamWriter = new StreamWriter(filePath);
        streamWriter.Write(data);
        streamWriter.Close();
    }
    /// <summary>
    /// 写入数据到文件中
    /// </summary>
    /// <param Name="data">数据</param>
    /// <param Name="path">文件路径</param>
    /// <param Name="extension">文件后缀名</param>
    public static void WriteFile(string data , string path, string extension)
    {
        WriteFile(data, path+extension);
    }
    /// <summary>
    /// 写入数据到文件中
    /// </summary>
    /// <param Name="data">数据</param>
    /// <param Name="dirPath">文件夹路径</param>
    /// <param Name="fileName">文件名</param>
    /// <param Name="extension">文件后缀名</param>
    public static void WriteFile(string data, string dirPath, string fileName, string extension)
    {
        WriteFile(data, dirPath+@"/"+fileName+extension);
    }
    #endregion

    #region 从文件中读取数据
    /// <summary>
    /// 从文件中读取数据
    /// </summary>
    /// <param Name="dirPath">文件目录路径</param>
    /// <param Name="fileName">文件名</param>
    /// <param Name="extension">文件后缀名</param>
    /// <returns></returns>
    public static string  ReadFile(string dirPath, string fileName, string extension)
    {
        return ReadFile(dirPath + @"/" + fileName + extension);
    }
    /// <summary>
    /// 从文件中读取数据
    /// </summary>
    /// <param Name="path">文件路径</param>
    /// <param Name="extension">文件后缀名</param>
    /// <returns></returns>
    public static string ReadFile(string path, string extension)
    {
        return ReadFile(path + extension);
    }
    /// <summary>
    /// 从文件中读取数据
    /// </summary>
    /// <param Name="filePath">文件全路径</param>
    /// <returns></returns>
    public static string ReadFile(string filePath)
    {
        string data=null;
        if (File.Exists(filePath))
        {
            var streamReader = new StreamReader(filePath);
            data = streamReader.ReadToEnd();
            streamReader.Close();
        }
        return data;
    }
    #endregion

    #region 从一个文件拷贝到另一个文件
    /// <summary>
    /// 从一个文件拷贝到另一个文件
    /// </summary>
    /// <param Name="sourceDirPath">源文件路径</param>
    /// <param Name="sourceFileName"></param>
    /// <param Name="destDirPath"></param>
    /// <param Name="destFileName"></param>
    /// <param Name="extension"></param>
    public static void CopyFile(string sourceDirPath, string sourceFileName,string destDirPath,string destFileName,string extension)
    {

        string data = ReadFile(sourceDirPath + @"/" + sourceFileName + extension);

        CopyDataToFile(data,destDirPath);
    }
    /// <summary>
    /// 从一个文件拷贝到另一个文件
    /// </summary>
    /// <param Name="sourcePath">源文件路径 不包含后缀</param>
    /// <param Name="destPath">目标文件路径 不包含后缀</param>
    /// <param Name="extension">文件后缀名</param>
    public static void CopyFile(string sourcePath, string destPath,string extension)
    {

        string data = ReadFile(sourcePath + extension);

        CopyDataToFile(data, destPath+extension);
    }
    ///// <summary>
    ///// 从一个文件拷贝到另一个文件
    ///// </summary>
    ///// <param Name="sourceFilePath">源文件全路径 包含后缀</param>
    ///// <param Name="destFilePath">目标文件全路径 包含后缀</param>
    //public static void CopyFile(string sourceFilePath, string destFilePath)
    //{

    //    string data = ReadFile(sourceFilePath);

    //    CopyDataToFile(data, destFilePath);
    //}
    /// <summary>
    /// 拷贝数据到一个目标文件中
    /// </summary>
    /// <param Name="data">数据</param>
    /// <param Name="destDirPath">目标文件目录路径</param>
    /// <param Name="destFileName">目标文件名</param>
    /// <param Name="extension">目标文件后缀名</param>
    public static void CopyDataToFile(string data, string destDirPath, string destFileName, string extension)
    {
        CopyDataToFile(data, destDirPath + @"/" + destFileName + extension);
    }
    /// <summary>
    /// 拷贝数据到一个目标文件中
    /// </summary>
    /// <param Name="data">数据</param>
    /// <param Name="destPath">目标文件路径</param>
    /// <param Name="extension">目标文件后缀名</param>
    public static void CopyDataToFile(string data, string destPath, string extension)
    {
        CopyDataToFile(data, destPath + extension);
    }
    /// <summary>
    ///  拷贝数据到一个目标文件中
    /// </summary>
    /// <param Name="data">数据</param>
    /// <param Name="destFilePath">目标文件全路径</param>
    public static void CopyDataToFile(string data, string destFilePath)
    {
        if(data!=null)
        {
            //创建目录
            CreateDirectory(destFilePath);
            //创建文件
            CreateFile(destFilePath);
            //写入数据
            WriteFile(data, destFilePath);
        }

    }
    #endregion 

    //#region GetFileName 获取文件名
    ///// <summary>
    ///// 获取文件名
    ///// </summary>
    ///// <param name="path"></param>
    ///// <returns></returns>
    //public static string GetFileName(string path)
    //{
    //    string fileName = path;
    //    int lastIndex = path.LastIndexOf('\\');
    //    if (lastIndex > -1)
    //    {
    //        fileName = fileName.Substring(lastIndex + 1);
    //    }

    //    lastIndex = fileName.LastIndexOf('.');
    //    if (lastIndex > -1)
    //    {
    //        fileName = fileName.Substring(0, lastIndex);
    //    }

    //    return fileName;
    //}
    //#endregion

    /// <summary>
    /// 从路径文件中读取byte数组
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static byte[] GetByte(string path)
    {
        byte[] buffer = null;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }

    /// <summary>
    /// 从一个文件拷贝到另一个文件
    /// </summary>
    /// <param Name="sourcePath">源文件夹路径 </param>
    /// <param Name="destPath">目标文件夹路径 </param>
    public static void CopyAllFile(string sourcePath, string destPath)
    {
        //不存在文件夹
        if (Directory.Exists(sourcePath) ==false)
        {
            return;
        }

        //删除目标文件夹
        if(Directory.Exists(destPath))
        {
            Directory.Delete(destPath, true);
        }

        //创建目标文件夹
        Directory.CreateDirectory(destPath);

        #region //拷贝sourcePath文件夹到destPath下
        try
        {
            string[] soruceFiles = Directory.GetFiles(sourcePath);//文件
            if (soruceFiles.Length > 0)
            {
                for (int i = 0; i < soruceFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(soruceFiles[i]);

                    File.Copy(sourcePath + "\\" + fileName, destPath + "\\" + fileName, true);
                }
            }

            string[] sourceDirs = Directory.GetDirectories(sourcePath);//目录
            if (sourceDirs.Length > 0)
            {
                for (int j = 0; j < sourceDirs.Length; j++)
                {
                    //递归调用
                    CopyAllFile(sourcePath + "\\" + Path.GetFileName(sourceDirs[j]), destPath + "\\" + Path.GetFileName(sourceDirs[j]));
                }
            }
        }
        catch (Exception)
        {
            return;
        }
        #endregion
    }

    /// <summary>
    /// 拷贝文件
    /// </summary>
    /// <param name="soruceFile"></param>
    /// <param name="destFile"></param>
    public static void CopyFile(string soruceFile,string destFile)
    {
        if (File.Exists(soruceFile) == false)
        {
            return;
        }

        //先删除
        if (File.Exists(destFile))
        {
            File.Delete(destFile);
        }

        File.Copy(soruceFile, destFile);
    }
}
