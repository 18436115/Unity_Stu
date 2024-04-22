using UnityEngine;

/// <summary>
/// 所有的资源文件路径
/// </summary>
public class AllPath
{
    #region 数据表位置
    /// <summary>
    /// 本地表数据读取位置
    /// </summary>    
    public static string LocalDataTableReadPath = Application.dataPath + "/../DataExcel/LocalData/";
    /// <summary>
    /// 本地表数据存储位置
    /// </summary>    
    public static string LocalDataTableTargetPath = Application.dataPath + "/DataTable/LocalData/";
    #endregion















    #region 参考线
    //----------------------------参考--------------------------
    //编辑器下
#if UNITY_EDITOR
    // win平台
#if UNITY_STANDALONE_WIN
    // Andoird平台
#elif UNITY_ANDROID
#elif UNITY_IPHONE
#endif

    //非编辑器下  win平台
#elif UNITY_STANDALONE_WIN
//非编辑器下 Andoird平台
#elif UNITY_ANDROID
//非编辑器下 Iphone平台
#elif UNITY_IPHONE
#endif
    //----------------------------参考--------------------------
    #endregion




    #region GameRes 本地资源路径  GameResFilePath
    //本地资源根路径
    public static string GameResFilePath = Application.dataPath;
    #endregion

    #region 本地  Streaming资源路径 ABStreamingAssetsPath
//#if UNITY_EDITOR
//    public static string ABStreamingAssetsPath = Application.streamingAssetsPath + @"/AssetBundles/Android/";
//    //public static string ABStreamingAssetsPath  = "jar:file://" + Application.dataPath + "!/assets" + @"/AssetBundles/Android/";
//#else
        // win平台
#if UNITY_STANDALONE_WIN
            public static string ABStreamingAssetsPath = "file:///" + Application.streamingAssetsPath  + @"/AssetBundles/Window/";
         // Andoird平台
#elif UNITY_ANDROID
        //public static string ABStreamingAssetsPath = "file:///" + Application.streamingAssetsPath + @"\AssetBundles\Android\";
        public static string ABStreamingAssetsPath = "jar:file://" + Application.dataPath + "!/assets" + @"\AssetBundles\Android\";
#elif UNITY_IPHONE
         public static string ABStreamingAssetsPath = "file:///" + Application.streamingAssetsPath + @"/AssetBundles/IOS/";
    //  public static string ABStreamingAssetsPath = "file://" + Application.dataPath + "/Raw/" + @"/AssetBundles/IOS/";
    //#endif
#endif
    #endregion

    #region Assetbundle本地文件路径 移动平台直接persistentDataPath
    //编辑器下
#if UNITY_EDITOR
    // win平台
#if UNITY_STANDALONE_WIN
         public static string ABLoaclFilePath = Application.dataPath + @"\..\AssetBundles\Window\";
    // Andoird平台
#elif UNITY_ANDROID
    public static string ABLoaclFilePath = Application.dataPath + @"\..\AssetBundles\Android\";
#elif UNITY_IPHONE
    public static string ABLoaclFilePath = Application.persistentDataPath + @"/AssetBundles/IOS/";
#endif
    //非编辑器下  win平台
#elif UNITY_STANDALONE_WIN
     public static string ABLoaclFilePath = Application.persistentDataPath +@"\AssetBundles\Window\";
//非编辑器下 Andoird平台
#elif UNITY_ANDROID
    public static string ABLoaclFilePath = Application.persistentDataPath + @"/AssetBundles/Android/";
//非编辑器下 Iphone平台
#elif UNITY_IPHONE
     public static string ABLoaclFilePath = Application.persistentDataPath + @"/AssetBundles/IOS/";
#endif
    #endregion

    #region Assetbundle  AB文件加载路径，移动平台根据需求用StreamingAssetsPath路径
    //编辑器下
#if UNITY_EDITOR
    // win平台
#if UNITY_STANDALONE_WIN
         public static string ABLoadFilePath = Application.dataPath + @"\..\AssetBundles\Window\";
    // Andoird平台
#elif UNITY_ANDROID
    public static string ABLoadFilePath = Application.dataPath + @"\..\AssetBundles\Android\";
#elif UNITY_IPHONE
    public static string ABLoadFilePath = Application.streamingAssetsPath + @"/AssetBundles/IOS/";
#endif
    //非编辑器下  win平台
#elif UNITY_STANDALONE_WIN
     public static string ABLoadFilePath = Application.persistentDataPath +@"\AssetBundles\Window\";
//非编辑器下 Andoird平台
#elif UNITY_ANDROID
    public static string ABLoadFilePath = Application.persistentDataPath + @"/AssetBundles/Android/";
//非编辑器下 Iphone平台
#elif UNITY_IPHONE
#if IOS_AB_StreamingAssetsPath
            public static string ABLoadFilePath = Application.streamingAssetsPath + @"/AssetBundles/IOS/";
#else
            public static string ABLoadFilePath = Application.persistentDataPath + @"/AssetBundles/IOS/";

#endif
     
#endif
    #endregion

    #region Assetbundle本地Manifest文件路径  ABLoaclFilePathManifest
    //编辑器下
#if UNITY_EDITOR
    // win平台
#if UNITY_STANDALONE_WIN
             public static string ABLoaclFilePathManifest = Application.dataPath + @"\..\AssetBundles\Window\Window";
            // Andoird平台
#elif UNITY_ANDROID
    public static string ABLoaclFilePathManifest = Application.dataPath + @"\..\AssetBundles\Android\Android";
#elif UNITY_IPHONE
    //  public static string ABLoaclFilePathManifest = Application.dataPath + @"\..\AssetBundles\IOS\IOS";
#if IOS_AB_StreamingAssetsPath
    public static string ABLoaclFilePathManifest = Application.streamingAssetsPath + @"/AssetBundles/IOS/IOS";
#else
    public static string ABLoaclFilePathManifest = Application.persistentDataPath + @"/AssetBundles/IOS/IOS";
#endif
#endif
    //非编辑器下  win平台
#elif UNITY_STANDALONE_WIN
        public static string ABLoaclFilePathManifest = Application.persistentDataPath +@"\AssetBundles\Window\Window";
//非编辑器下 Andoird平台
#elif UNITY_ANDROID
        public static string ABLoaclFilePathManifest = Application.persistentDataPath +@"/AssetBundles/Android/Android";
//非编辑器下 Iphone平台
#elif UNITY_IPHONE
#if IOS_AB_StreamingAssetsPath
    public static string ABLoaclFilePathManifest = Application.streamingAssetsPath + @"/AssetBundles/IOS/IOS";
#else
    public static string ABLoaclFilePathManifest = Application.persistentDataPath + @"/AssetBundles/IOS/IOS";
#endif
#endif
    #endregion

    #region  Assetbundle服务器资源路径 ABDownloadUrl
    /// <summary>
    /// 服务器资源路径
    ///      AB包下载路径
    /// </summary>
    //public static string ABDownloadBaseUrl = "http://pkg.veeking.cn/GoogleMagicFishSaga";
    public static string ABDownloadBaseUrl = "http://192.168.81.200:8081";
    //http://127.0.0.1/AssetBundles/Android/VersionFile.txt



    //编辑器下
#if UNITY_EDITOR
    // win平台
#if UNITY_STANDALONE_WIN
             public static string ABDownloadUrl = ABDownloadBaseUrl + @"/AssetBundles/Windows/";
            // Andoird平台
#elif UNITY_ANDROID
   public static string ABDownloadUrl = ABDownloadBaseUrl + @"\AssetBundles\Android\";
#elif UNITY_IPHONE
    //public static string ABDownloadUrl = ABDownloadBaseUrl + @"/AssetBundles/iOS/";
    public static string ABDownloadUrl = ABDownloadBaseUrl + @"/AssetBundles/iOS/";
#endif
    //非编辑器下  win平台
#elif UNITY_STANDALONE_WIN
         public static string ABDownloadUrl = ABDownloadBaseUrl + @"/AssetBundles/Windows/";
//非编辑器下 Andoird平台
#elif UNITY_ANDROID
       public static string ABDownloadUrl = ABDownloadBaseUrl + @"\AssetBundles\Android\";
//非编辑器下 Iphone平台
#elif UNITY_IPHONE
        public static string ABDownloadUrl = ABDownloadBaseUrl + @"/AssetBundles/iOS/";
#endif
#endregion



#region 本地数据表DataTable路径  DTLoaclFilePath
    //编辑器下
#if UNITY_EDITOR
    // win平台
#if UNITY_STANDALONE_WIN
        public static string DTLoaclFilePath = Application.dataPath + @"/../AssetBundles/Window/GameRes/DataTable/LocalData/";
           // Andoird平台
#elif UNITY_ANDROID
    public static string DTLoaclFilePath = Application.dataPath + @"\..\AssetBundles\Android\GameRes\DataTable\LocalData\";
#elif UNITY_IPHONE
    //public static string DTLoaclFilePath = Application.dataPath + @"/../AssetBundles/IOS/GameRes/DataTable/LocalData/";
    public static string DTLoaclFilePath = Application.persistentDataPath + @"/AssetBundles/IOS/gameres/DataTable/LocalData/";
#endif
    //非编辑器下  win平台
#elif UNITY_STANDALONE_WIN
    public static string DTLoaclFilePath = Application.persistentDataPath + @"/AssetBundles/Window/GameRes/DataTable/LocalData/";
//非编辑器下 Andoird平台
#elif UNITY_ANDROID
    public static string DTLoaclFilePath = Application.persistentDataPath + @"/AssetBundles/Android/GameRes/DataTable/LocalData/";
//非编辑器下 Iphone平台
#elif UNITY_IPHONE
     public static string DTLoaclFilePath = Application.persistentDataPath + @"/AssetBundles/IOS/gameres/DataTable/LocalData/";
     //    public static string DTLoaclFilePath = Application.streamingAssetsPath + @"/AssetBundles/IOS/gameres/DataTable/LocalData/";
#endif
#endregion

#region GameRes 用户数据表头  UserData路径  GameResUserDataTableFilePath
    //用户表头
    public static string GameResUserDataTableFilePath = Application.dataPath + @"\GameRes\DataTable\UserData\";
#endregion

#region 用户数据表UserData路径  DTUserDataFilePath
#if UNITY_EDITOR
    //非编辑器下  win平台
#if UNITY_STANDALONE_WIN
         public static string DTUserDataTableFilePath = Application.persistentDataPath +@"/AssetBundles/Window/GameRes/DataTable/UserData/";
    //非编辑器下 Andoird平台
#elif UNITY_ANDROID
        public static string DTUserDataTableFilePath = Application.persistentDataPath + @"\AssetBundles\Android\GameRes\DataTable\UserData\";
    //非编辑器下 Iphone平台
#elif UNITY_IPHONE
    //public static string DTUserDataTableFilePath = Application.streamingAssetsPath +@"/AssetBundles/IOS/gameres/DataTable/UserData/";
    public static string DTUserDataTableFilePath = Application.persistentDataPath + @"/AssetBundles/IOS/gameres/DataTable/UserData/";
#endif

    //非编辑器下  win平台
#elif UNITY_STANDALONE_WIN
         public static string DTUserDataTableFilePath = Application.persistentDataPath +@"/AssetBundles/Window/GameRes/DataTable/UserData/";
    //非编辑器下 Andoird平台
#elif UNITY_ANDROID
        public static string DTUserDataTableFilePath = Application.persistentDataPath + @"/AssetBundles/Android/GameRes/DataTable/UserData/";
        //非编辑器下 Iphone平台
#elif UNITY_IPHONE
         public static string DTUserDataTableFilePath = Application.persistentDataPath +@"/AssetBundles/IOS/gameres/DataTable/UserData/";
      //  public static string DTUserDataTableFilePath = Application.streamingAssetsPath +@"/AssetBundles/IOS/gameres/DataTable/UserData/";
#endif


#endregion

#region GameRes 用户数据表UserData路径  GameResUserDataFilePath
    //编辑器下   使用本地资源根路径
    public static string GameResUserDataFilePath = Application.dataPath + @"\GameRes\UserData\";
#endregion

#region 用户数据表UserData路径  DTUserDataFilePath

#if UNITY_EDITOR
    //非编辑器下  win平台
#if UNITY_STANDALONE_WIN
     public static string DTUserDataFilePath = Application.persistentDataPath +@"\AssetBundles\Window\GameRes\UserData\";
    //非编辑器下 Andoird平台
#elif UNITY_ANDROID
    public static string DTUserDataFilePath = Application.persistentDataPath + @"\AssetBundles\Android\GameRes\UserData\";
    //非编辑器下 Iphone平台
#elif UNITY_IPHONE
    public static string DTUserDataFilePath = Application.persistentDataPath +@"/AssetBundles/IOS/gameres/UserData/";
    //public static string DTUserDataFilePath = Application.streamingAssetsPath + @"/AssetBundles/IOS/gameres/UserData/";
#endif
#elif UNITY_STANDALONE_WIN
     public static string DTUserDataFilePath = Application.persistentDataPath +@"/AssetBundles/Window/GameRes/UserData/";
//非编辑器下 Andoird平台
#elif UNITY_ANDROID
    public static string DTUserDataFilePath = Application.persistentDataPath + @"/AssetBundles/Android/GameRes/UserData/";
    //非编辑器下 Iphone平台
#elif UNITY_IPHONE
     public static string DTUserDataFilePath = Application.persistentDataPath +@"/AssetBundles/IOS/gameres/UserData/";
    // public static string DTUserDataFilePath = Application.streamingAssetsPath +@"/AssetBundles/IOS/gameres/UserData/";
#endif
#endregion

}





