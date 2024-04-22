using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildTools
{
    [MenuItem("����/Tools/SetABName")]
    public static void SetABName()
    {
        //�Ƴ�û���õ�assetbundlename
        AssetDatabase.RemoveUnusedAssetBundleNames();
#if UNITY_STANDALONE_WIN
        string dataPath = Application.dataPath.Replace("/", "\\");
        string gameResPath = Path.Combine(dataPath, "GameRes\\");
#else
        string dataPath = Application.dataPath;
        string gameResPath = Path.Combine(dataPath, "GameRes/");
#endif
        string[] files = Directory.GetFiles(gameResPath, "*.*", SearchOption.AllDirectories);
        foreach (string filePath in files)
        {
            if (filePath.EndsWith(".meta")) continue;
            string fullPath = filePath;
#if UNITY_STANDALONE_WIN
            fullPath = filePath.Replace("/", "\\");
#endif

            string fileFullDirName = Path.GetDirectoryName(fullPath);
            string fileAssetDirName = fullPath.Replace(dataPath, "Assets");
            string abName = fileFullDirName.Replace(gameResPath, "");
#if UNITY_STANDALONE_WIN
            if (abName.Contains("\\"))
                abName = abName.Substring(0, abName.IndexOf("\\"));
#else
            if (abName.Contains("/"))
                abName = abName.Substring(0, abName.IndexOf("/"));
#endif
            AssetImporter importer = AssetImporter.GetAtPath(fileAssetDirName);
            importer.assetBundleName = abName;
        }
        Debug.Log("SetABName Done");
    }






    [MenuItem("����/Tools/BuildAB")]
    public static void StartBuildAB()
    {
        // ��3�������������ƽ̨����
        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
                                        BuildAssetBundleOptions.ChunkBasedCompression,
                                        BuildTarget.StandaloneWindows);
        AssetDatabase.Refresh();
    }






}

