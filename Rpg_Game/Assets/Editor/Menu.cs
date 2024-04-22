using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu 
{
    [MenuItem("游戏设置/全局设置")]
    public static void SettingsWindowBtn() 
    {
        SettingsWindow settingsWindow = EditorWindow.GetWindow<SettingsWindow>();
        settingsWindow.titleContent = new GUIContent("全局设置");
        settingsWindow.Show();
    }

    [MenuItem("游戏设置/资源管理")]
    public static void AssetBundleWindowBtn()
    {
        AssetBundleWindow assetBundleWindow = EditorWindow.GetWindow<AssetBundleWindow>();
        assetBundleWindow.titleContent = new GUIContent("资源管理");
        assetBundleWindow.Show();
    }

    [MenuItem("游戏设置/数据加载")]
    private static void OpenExcelToDataWidnow()
    {
        ExcelToDataWindow excelToDataWindow = EditorWindow.GetWindow<ExcelToDataWindow>();
        excelToDataWindow.titleContent = new GUIContent("数据加载");
        excelToDataWindow.minSize = new Vector2(800, 400);
        excelToDataWindow.Show();
    }
}
