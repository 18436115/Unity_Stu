using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingsWindow : EditorWindow
{
    /// <summary>
    /// 宏定义列表
    /// </summary>
    private List<MacorItem> m_List = new List<MacorItem>();
    /// <summary>
    /// 宏定义状态字典
    /// </summary>
    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();
    /// <summary>
    /// Unity宏定义字段
    /// </summary>
    private string m_Macor = null;

    private void OnEnable()
    {
        //获取当前宏定义字段
#if UNITY_STANDALONE_WIN
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
#elif UNITY_ANDROID
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
#elif UNITY_IPHONE
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
#endif

        //初始化当前宏定义列表
        m_List.Clear();
        m_List.Add(new MacorItem() { Name = "DEBUG_MODEL", DisplayName = "调试模式", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DEBUG_REAL", DisplayName = "发布模式", IsDebug = false, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "DEBUG_LOG", DisplayName = "打印日志", IsDebug = true, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "HOTFIX_ENABLE", DisplayName = "开启热补丁", IsDebug = false, IsRelease = true });
        //设置当前宏定义状态
        for (int i = 0; i < m_List.Count; i++)
        {
            if (!string.IsNullOrEmpty(m_Macor) && m_Macor.IndexOf(m_List[i].Name) != -1)
            {
                m_Dic[m_List[i].Name] = true;
            }
            else
            {
                m_Dic[m_List[i].Name] = false;
            }
        }
    }


    void OnGUI()
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
            EditorGUILayout.EndHorizontal();
        }

        //开启一行
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveMacor();
        }

        if (GUILayout.Button("调试模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            //SaveMacor();
        }

        if (GUILayout.Button("发布模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsRelease;
            }
            //SaveMacor();
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 保存宏定义
    /// </summary>
    private void SaveMacor()
    {
        //重置宏定义字段
        m_Macor = string.Empty;
        foreach (var item in m_Dic)
        {
            if (item.Value)
            {
                m_Macor += string.Format("{0};", item.Key);
            }
        }
        //将当前字段填入Unity
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macor);
    }
}
