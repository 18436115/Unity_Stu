using System.IO;
using UnityEditor;
using UnityEngine;

public class ExcelToDataWindow : EditorWindow
{
    #region 本地数据路径
    //-------------------本地数据选择路径---------------------
    /// <summary>
    /// 选择的文件路径
    /// </summary>
    private string selectFilePath;
    /// <summary>
    /// 选择文件夹路径
    /// </summary>
    private string selectDirectoryPath;


    //-------------------本地数据存储路径---------------------
    /// <summary>
    /// 本地数据 -创建文件路径
    /// </summary>
    private string LocalDataTableCreatePath;
    /// <summary>
    /// 本地数据脚本路径 -创建文件类路径
    /// </summary>
    private string LocalDataScriptCreatePath;
    /// <summary>
    /// 本地数据 -存储文件路径
    /// </summary>
    private string LoaclDataTableDirPath;
    /// <summary>
    /// 本地数据脚本路径 -存储文件类路径
    /// </summary>
    private string LocalDataScriptDirPath;
    #endregion








    #region 用户数据路径
    /// <summary>
    /// 选择的文件路径
    /// </summary>
    private string selectUserFilePath;
    /// <summary>
    /// 选择的文件夹路径
    /// </summary>
    private string selectUserPath;
    //-------------------用户数据路径---------------------
    /// <summary>
    /// 文件路径
    /// </summary>
    private string UserDataExcelDirPath;
    /// <summary>
    /// 自动生成文件夹 路径
    /// </summary>
    private string CreateUserDataDirPath;
    /// <summary>
    /// 本地数据 的文件夹路径
    /// </summary>
    private string UserDataDirPath;
    /// <summary>
    /// 数据 脚本的文件夹路径
    /// </summary>
    private string UserDataScriptDirPath;
    #endregion

    #region 查看data文件内容
    /// <summary>
    /// 选择的data文件路径
    /// </summary>
    private string selectDatePath;
    /// <summary>
    /// 选择的data文件路径
    /// </summary>
    private string selectDataFilePath;
    /// <summary>
    /// 滑动区域
    /// </summary>
    private Vector2 ScrollPos = Vector2.zero;
    /// <summary>
    /// data文件数据内容
    /// </summary>
    private string dataStr = string.Empty;
    #endregion

    private void OnEnable()
    {
        selectFilePath = "";
        selectDirectoryPath = "";

        LocalDataTableCreatePath = AllPath.LocalDataTableReadPath;
        LocalDataScriptCreatePath = LocalDataTableCreatePath + "Create/";

        LoaclDataTableDirPath = AllPath.LocalDataTableTargetPath;
        LocalDataScriptDirPath = Application.dataPath + "/Scripts/System/LoadSystem/Excel/Data/LocalData/Create/";




        //用户数据路径
        //excel文件路径 默认初始路径
        UserDataExcelDirPath = Application.dataPath + "/../DataExcel/UserData/";
        //自动生成文件路径 默认初始路径
        CreateUserDataDirPath = UserDataExcelDirPath + "/Create/";
        UserDataDirPath = AllPath.GameResUserDataTableFilePath;
        UserDataScriptDirPath = Application.dataPath + "/02.Scripts/Game/Data/UserData/Create/";

        
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        //-------------------------------------ExcelTodata转换区域------------------------------------
        #region ExcelTodata转换区域
        //提示显示
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.LabelField("请先选择Excel文件!");
        EditorGUILayout.EndHorizontal();

        //文件路径显示
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.TextField("Excel文件路径:", selectFilePath, GUILayout.MinWidth(400), GUILayout.Height(20));
        GUILayout.Space(10);
        EditorGUILayout.EndHorizontal();

        //按钮
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        if (GUILayout.Button("选择Excel文件", GUILayout.Width(130), GUILayout.Height(25)))
        {
            if (string.IsNullOrEmpty(selectDirectoryPath))
            {
                selectFilePath = EditorUtility.OpenFilePanel("选择Excel文件", LocalDataTableCreatePath, "xls");
            }
            else
            {
                selectFilePath = EditorUtility.OpenFilePanel("选择Excel文件", selectDirectoryPath, "xls");
            }
            selectDirectoryPath = selectFilePath.Substring(0, selectFilePath.LastIndexOf('/') + 1);
        }
        GUILayout.Space(25);
        if (GUILayout.Button("单个Excel文件转化", GUILayout.Width(200), GUILayout.Height(25)))
        {
            if (string.IsNullOrEmpty(selectFilePath))
            {
                //提示 未选择文件 
                EditorUtility.DisplayDialog("错误", " 未选择文件！！！", "确定");
            }
            else if (selectFilePath.EndsWith(".xls") == false)
            {
                //提示 选择文件格式存在问题'
                EditorUtility.DisplayDialog("错误", " 选择文件格式存在问题！！！", "确定");
            }
            else
            {
                //转换
                if (ExcelToData.DoExcelToData(selectFilePath))
                {
                    //提示 成功
                    EditorUtility.DisplayDialog("结果", " 转换成功！！！", "确定");

                    //成功
                    //拷贝文件
                    string name = Path.GetFileNameWithoutExtension(selectFilePath);
               

                    //1.拷贝data文件到  GameRes/DataTable 路径
                    if (Directory.Exists(LoaclDataTableDirPath) ==false)
                    {
                        Directory.CreateDirectory(LoaclDataTableDirPath);
                    }

                    string fileName = name + ".data";
                    FileHelper.CopyFile(selectDirectoryPath + fileName, LoaclDataTableDirPath + fileName);


                    //2.拷贝script文件到 "/02.Scripts/Game/Data/LocalData/Create/";
                    if (Directory.Exists(LocalDataScriptDirPath) == false)
                    {
                        Directory.CreateDirectory(LocalDataScriptDirPath);
                    }

                    string dbFileName = name + "DBModel.cs";
                    string entityFileName = name + "Entity.cs";

                    FileHelper.CopyFile(selectDirectoryPath + "/Create/" + dbFileName, LocalDataScriptDirPath + dbFileName);
                    FileHelper.CopyFile(selectDirectoryPath + "/Create/" + entityFileName, LocalDataScriptDirPath + entityFileName);

                    AssetDatabase.Refresh();
                }
                else
                {
                    //提示 失败
                    EditorUtility.DisplayDialog("结果", " 转换失败！！！", "确定");
                }
            }
        }
        GUILayout.Space(20);
        if (GUILayout.Button("所有Excel文件转化", GUILayout.Width(200), GUILayout.Height(25)))
        {
            string[] arrExcelFiles = Directory.GetFiles(LocalDataTableCreatePath, "*.xls");
            //转换
            if (arrExcelFiles.Length > 0)
            {
                for (int i = 0; i < arrExcelFiles.Length; i++)
                {
                    if (ExcelToData.DoExcelToData(arrExcelFiles[i]) ==false)
                    {
                        //提示 失败
                        EditorUtility.DisplayDialog("结果", string.Format(" 文件{0}转换失败！！！", arrExcelFiles[i]), "确定");
                        return;
                    }
                }

                //1.所有 data文件 DataExcelDirPath
                //拿到文件夹下所有文件
                string[] arrFiles = Directory.GetFiles(LocalDataTableCreatePath, "*.data");
                if (arrFiles.Length > 0)
                {
                    for (int i = 0; i < arrFiles.Length; i++)
                    {
                        string fileName = Path.GetFileName(arrFiles[i]);

                        FileHelper.CopyFile(LocalDataTableCreatePath + fileName, LoaclDataTableDirPath + fileName);
                    }
                }

                //2.所有的自动化脚本文件 CreateDataDirPath
                FileHelper.CopyAllFile(LocalDataScriptCreatePath, LocalDataScriptDirPath);

                AssetDatabase.Refresh();

                //提示 成功
                EditorUtility.DisplayDialog("结果", " 转换成功！！！", "确定");
            }
            else
            {
                //提示 失败
                EditorUtility.DisplayDialog("结果", " 没有Excel文件，请检查！！！", "确定");
            }
        }
        EditorGUILayout.EndHorizontal();

        //文件路径显示
        GUILayout.Space(15);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.TextField("UserExcel文件路径:", selectUserFilePath, GUILayout.MinWidth(400), GUILayout.Height(20));
        GUILayout.Space(10);
        EditorGUILayout.EndHorizontal();
        //按钮
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        if (GUILayout.Button("选择UserExcel文件", GUILayout.Width(130), GUILayout.Height(25)))
        {
            if (string.IsNullOrEmpty(selectUserPath))
            {
                selectUserFilePath = EditorUtility.OpenFilePanel("选择UserExcel文件", UserDataExcelDirPath, "xls");
            }
            else
            {
                selectUserFilePath = EditorUtility.OpenFilePanel("选择UserExcel文件", selectUserPath, "xls");
            }
            selectUserPath = selectUserFilePath.Substring(0, selectUserFilePath.LastIndexOf('/') + 1);
        }
        GUILayout.Space(25);
        if (GUILayout.Button("单个UserExcel文件转化", GUILayout.Width(200), GUILayout.Height(25)))
        {
            if (string.IsNullOrEmpty(selectUserFilePath))
            {
                //提示 未选择文件 
                EditorUtility.DisplayDialog("错误", " 未选择文件！！！", "确定");
            }
            else if (selectUserFilePath.EndsWith(".xls") == false)
            {
                //提示 选择文件格式存在问题'
                EditorUtility.DisplayDialog("错误", " 选择文件格式存在问题！！！", "确定");
            }
            else
            {
                //转换 用户数据
                if (ExcelToData.DoExcelToData(selectUserFilePath,1))
                {
                    //提示 成功
                    EditorUtility.DisplayDialog("结果", " 转换成功！！！", "确定");

                    //成功
                    //拷贝文件
                    string name = Path.GetFileNameWithoutExtension(selectUserFilePath);

                    //1.拷贝data文件到  GameRes/DataTable 路径
                    if (Directory.Exists(UserDataDirPath) == false)
                    {
                        Directory.CreateDirectory(UserDataDirPath);
                    }

                    string fileName = name + ".data";
                    FileHelper.CopyFile(selectUserPath + fileName, UserDataDirPath + fileName);


                    //2.拷贝script文件到 "/02.Scripts/Game/Data/LocalData/Create/";
                    if (Directory.Exists(UserDataScriptDirPath) == false)
                    {
                        Directory.CreateDirectory(UserDataScriptDirPath);
                    }

                    string dbFileName = name + "DBModel.cs";
                    string entityFileName = name + "Entity.cs";

                    FileHelper.CopyFile(selectUserPath + "/Create/" + dbFileName, UserDataScriptDirPath + dbFileName);
                    FileHelper.CopyFile(selectUserPath + "/Create/" + entityFileName, UserDataScriptDirPath + entityFileName);

                    AssetDatabase.Refresh();
                }
                else
                {
                    //提示 失败
                    EditorUtility.DisplayDialog("结果", " 转换失败！！！", "确定");
                }
            }
        }
        GUILayout.Space(20);
        if (GUILayout.Button("所有UserExcel文件转化", GUILayout.Width(200), GUILayout.Height(25)))
        {
            string[] arrExcelFiles = Directory.GetFiles(UserDataExcelDirPath, "*.xls");
            //转换
            if (arrExcelFiles.Length > 0)
            {
                for (int i = 0; i < arrExcelFiles.Length; i++)
                {
                    //用户数据
                    if (ExcelToData.DoExcelToData(arrExcelFiles[i] ,1) == false)
                    {
                        //提示 失败
                        EditorUtility.DisplayDialog("结果", string.Format(" 文件{0}转换失败！！！", arrExcelFiles[i]), "确定");
                        return;
                    }
                }

                //1.所有 data文件 DataExcelDirPath
                //拿到文件夹下所有文件
                string[] arrFiles = Directory.GetFiles(UserDataExcelDirPath, "*.data");
                if (arrFiles.Length > 0)
                {
                    for (int i = 0; i < arrFiles.Length; i++)
                    {
                        string fileName = Path.GetFileName(arrFiles[i]);

                        FileHelper.CopyFile(UserDataExcelDirPath + fileName, UserDataDirPath + fileName);
                    }
                }

                //2.所有的自动化脚本文件 CreateDataDirPath
                FileHelper.CopyAllFile(CreateUserDataDirPath, UserDataScriptDirPath);

                AssetDatabase.Refresh();

                //提示 成功
                EditorUtility.DisplayDialog("结果", " 转换成功！！！", "确定");
            }
            else
            {
                //提示 失败
                EditorUtility.DisplayDialog("结果", " 没有Excel文件，请检查！！！", "确定");
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        //------------------------------------------------------------------------------------------------

        GUILayout.Space(20);

        //-------------------------------------显示data文件内容区域------------------------------------
        #region 显示data文件内容区域
        //提示信息
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.LabelField("请先选择Data文件!");
        EditorGUILayout.EndHorizontal();

        //显示读取的文件信息
        GUILayout.Space(5);
        ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.MaxHeight(400));
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(25);
        EditorGUILayout.TextArea(dataStr,GUILayout.MinWidth(750));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        //按钮
        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        if (GUILayout.Button("查看Data文件", GUILayout.Width(130), GUILayout.Height(25)))
        {
            dataStr = string.Empty;
            if (string.IsNullOrEmpty(selectDatePath))
            {
                selectDataFilePath = EditorUtility.OpenFilePanel("选择需要查看的Data文件", LocalDataTableCreatePath, "data");
            }
            else
            {
                selectDataFilePath = EditorUtility.OpenFilePanel("选择需要查看的Data文件", selectDatePath, "data");
                
            }
            selectDatePath = selectDataFilePath.Substring(0, selectDataFilePath.LastIndexOf('/') + 1);

            if (string.IsNullOrEmpty(selectDataFilePath))
            {
                //提示 未选择文件 
                EditorUtility.DisplayDialog("错误", " 未选择文件！！！", "确定");
            }
            else if (selectDataFilePath.EndsWith(".data") == false)
            {
                //提示 选择文件格式存在问题
                EditorUtility.DisplayDialog("错误", " 选择文件格式存在问题！！！", "确定");
            }
            else
            {
                dataStr = ExcelToData.DoViewData(selectDataFilePath);
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        //------------------------------------------------------------------------------------------------

        GUILayout.Space(20);

        EditorGUILayout.EndVertical();
    }

}

