using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class AssetBundleWindow : EditorWindow
{
    private string[] arrTag = { "All", "Scene", "Role", "Effect", "Audio", "UI", "None" };
    private int selectTagIndex = -1; //ѡ��ı�ǵ�����
    private int tagIndex = 0; //��ǵ����� -��ʼ����

    private string[] arrBuildTarget = { "Windows", "Android", "iOS" };
    private int selectBuildTargetIndex = -1; //ѡ��Ĵ��ƽ̨����
#if UNITY_STANDALONE_WIN
    private BuildTarget target = BuildTarget.StandaloneWindows;
    private int buildTargetIndex = 0; //�����ƽ̨���� -��ʼ����
#elif UNITY_ANDROID
    private BuildTarget target = BuildTarget.Android;
    private int buildTargetIndex = 1; //�����ƽ̨���� -��ʼ����
#elif UNITY_IPHONE
    private BuildTarget target = BuildTarget.iOS;
    private int buildTargetIndex = 2; //�����ƽ̨���� -��ʼ����
#endif

    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;

    private Vector2 pos;

    void OnEnable()
    {
        string xmlPath = Application.dataPath + @"\XML\AssetBundleConfig.xml";
        dal = new AssetBundleDAL(xmlPath);
        m_List = dal.GetList();

        m_Dic = new Dictionary<string, bool>();
        for (int i = 0; i < m_List.Count; i++)
        {
            m_Dic[m_List[i].Key] = true;
        }
    }

    void OnGUI()
    {
        if (m_List == null) return;

        #region ��ť��
        GUILayout.BeginHorizontal("box");

        selectTagIndex = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));
        if (selectTagIndex != tagIndex)
        {
            tagIndex = selectTagIndex;
            EditorApplication.delayCall = OnSelectTagCallBack;
        }
        selectBuildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuildTarget, GUILayout.Width(100));
        if (selectBuildTargetIndex != buildTargetIndex)
        {
            buildTargetIndex = selectBuildTargetIndex;
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }





        if (GUILayout.Button("��������", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnSaveAssetBundleCallBack;
        }

        if (GUILayout.Button("���AssetBundle��", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }

        if (GUILayout.Button("��AssetBundle��", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnAssetBundleCallBack;
        }

        if (GUILayout.Button("�������ݱ�", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCopyDataTableCallBack;
        }

        if (GUILayout.Button("���ɰ汾�ļ�", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCreateVersionFileCallBack;
        }

        EditorGUILayout.Space();

        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("����");
        GUILayout.Label("���", GUILayout.Width(100));
        GUILayout.Label("�ļ���", GUILayout.Width(200));
        GUILayout.Label("��ʼ��Դ", GUILayout.Width(200));
        GUILayout.EndHorizontal();



        GUILayout.BeginVertical();
        pos = EditorGUILayout.BeginScrollView(pos);
        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];

            GUILayout.BeginHorizontal("box");

            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.IsFolder.ToString(), GUILayout.Width(200));
            GUILayout.Label(entity.IsFirstData.ToString(), GUILayout.Width(200));
            GUILayout.EndHorizontal();

            foreach (string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();
    }


    /// <summary>
    /// ѡ��Tag�ص�
    /// </summary>
    private void OnSelectTagCallBack()
    {
        switch (tagIndex)
        {
            case 0: //ȫѡ
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = true;
                }
                break;
            case 1: //Scene
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 2: //Role
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 3: //Effect
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Effect", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 4: //Audio
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 5: //UI
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("UI", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 6: //None
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = false;
                }
                break;
        }
        Debug.LogFormat("��ǰѡ���Tag��{0}", arrTag[tagIndex]);
    }

    /// <summary>
    /// ѡ��Target�ص�
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        switch (buildTargetIndex)
        {
            case 0: //Windows
                target = BuildTarget.StandaloneWindows;
                break;
            case 1: //Android
                target = BuildTarget.Android;
                break;
            case 2: //iOS
                target = BuildTarget.iOS;
                break;
        }
        Debug.LogFormat("��ǰѡ���BuildTarget��{0}", arrBuildTarget[buildTargetIndex]);
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void OnSaveAssetBundleCallBack()
    {
        //��Ҫ����Ķ���
        List<AssetBundleEntity> lst = new List<AssetBundleEntity>();

        foreach (AssetBundleEntity entity in m_List)
        {
            if (m_Dic[entity.Key])
            {
                entity.IsChecked = true;
                lst.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                lst.Add(entity);
            }
        }

        //ѭ�������ļ��а������ļ���ߵ���
        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundleEntity entity = lst[i];//ȡ��һ���ڵ�
            if (entity.IsFolder)
            {
                //�������ڵ����õ���һ���ļ��У���ô��Ҫ�����ļ���
                //��Ҫ��·��ɾ���·��
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                }
                SaveFolderSettings(folderArr, !entity.IsChecked);
            }
            else
            {
                //��������ļ��� ֻ��Ҫ������ߵ���
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                    SaveFileSetting(folderArr[j], !entity.IsChecked);
                }
            }
        }

        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
        Debug.Log("�����������");
    }

    private void SaveFolderSettings(string[] folderArr, bool isSetNull)
    {
        foreach (string folderPath in folderArr)
        {
            //1.�ȿ�����ļ����µ��ļ�
            string[] arrFile = Directory.GetFiles(folderPath); //�ļ����µ��ļ�

            //2.���ļ���������
            foreach (string filePath in arrFile)
            {
                Debug.Log("filePath=" + filePath);
                //��������
                SaveFileSetting(filePath, isSetNull);
            }

            //3.������ļ����µ����ļ���
            string[] arrFolder = Directory.GetDirectories(folderPath);
            SaveFolderSettings(arrFolder, isSetNull);
        }
    }

    private void SaveFileSetting(string filePath, bool isSetNull)
    {
        FileInfo file = new FileInfo(filePath);
        if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
        {
            //Debug.Log("filePath=" + filePath);
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);

            //·��
            string newPath = filePath.Substring(index);
            Debug.Log("newPath=" + newPath);

            //�ļ���
            string fileName = newPath.Replace("Assets/", "").Replace(file.Extension, "");

            //��׺
            string variant = file.Extension.Equals(".unity", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle";

            AssetImporter import = AssetImporter.GetAtPath(newPath);
            import.SetAssetBundleNameAndVariant(fileName, variant);

            if (isSetNull)
            {
                import.SetAssetBundleNameAndVariant(null, null);
            }
            import.SaveAndReimport();
        }
    }

    /// <summary>
    /// ����ص�
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }

        //�����������һ�仰
        BuildPipeline.BuildAssetBundles(toPath, BuildAssetBundleOptions.None, target);
        Debug.Log("������");
    }

    /// <summary>
    /// ���AssetBundle���ص�
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Debug.Log("������");
    }




    /// <summary>
    /// �������ݱ�
    /// </summary>
    private void OnCopyDataTableCallBack()
    {
        string fromPath = Application.dataPath + "/DataTable";
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex] + "/DataTable";
        IOUtil.CopyDirectory(fromPath, toPath);
        Debug.Log("�������ݱ����");
    }

    /// <summary>
    /// ���ɰ汾�ļ�
    /// </summary>
    private void OnCreateVersionFileCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string strVersionFilePath = path + "/VersionFile.txt"; //�汾�ļ�·��

        //����汾�ļ����� ��ɾ��
        IOUtil.DeleteFile(strVersionFilePath);

        StringBuilder sbContent = new StringBuilder();

        DirectoryInfo directory = new DirectoryInfo(path);

        //�õ��ļ����������ļ�
        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];
            string fullName = file.FullName; //ȫ�� ����·����չ��

            //���·��
            string name = fullName.Substring(fullName.IndexOf(arrBuildTarget[buildTargetIndex]) + arrBuildTarget[buildTargetIndex].Length + 1);

            string md5 = EncryptUtil.GetFileMD5(fullName); //�ļ���MD5
            if (md5 == null) continue;

            string size = Math.Ceiling(file.Length / 1024f).ToString(); //�ļ���С

            bool isFirstData = true; //�Ƿ��ʼ����
            bool isBreak = false;

            for (int j = 0; j < m_List.Count; j++)
            {
                foreach (string xmlPath in m_List[j].PathList)
                {
                    string tempPath = xmlPath;
                    if (xmlPath.IndexOf(".") != -1)
                    {
                        tempPath = xmlPath.Substring(0, xmlPath.IndexOf("."));
                    }
                    if (name.IndexOf(tempPath, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        isFirstData = m_List[j].IsFirstData;
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak) break;
            }

            if (name.IndexOf("DataTable") != -1)
            {
                isFirstData = true;
            }

            string strLine = string.Format("{0} {1} {2} {3}", name, md5, size, isFirstData ? 1 : 0);
            sbContent.AppendLine(strLine);
        }

        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());
        Debug.Log("�����汾�ļ��ɹ�");
    }
}
