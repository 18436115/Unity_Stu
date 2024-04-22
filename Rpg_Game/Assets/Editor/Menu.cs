using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu 
{
    [MenuItem("��Ϸ����/ȫ������")]
    public static void SettingsWindowBtn() 
    {
        SettingsWindow settingsWindow = EditorWindow.GetWindow<SettingsWindow>();
        settingsWindow.titleContent = new GUIContent("ȫ������");
        settingsWindow.Show();
    }

    [MenuItem("��Ϸ����/��Դ����")]
    public static void AssetBundleWindowBtn()
    {
        AssetBundleWindow assetBundleWindow = EditorWindow.GetWindow<AssetBundleWindow>();
        assetBundleWindow.titleContent = new GUIContent("��Դ����");
        assetBundleWindow.Show();
    }

    [MenuItem("��Ϸ����/���ݼ���")]
    private static void OpenExcelToDataWidnow()
    {
        ExcelToDataWindow excelToDataWindow = EditorWindow.GetWindow<ExcelToDataWindow>();
        excelToDataWindow.titleContent = new GUIContent("���ݼ���");
        excelToDataWindow.minSize = new Vector2(800, 400);
        excelToDataWindow.Show();
    }
}
