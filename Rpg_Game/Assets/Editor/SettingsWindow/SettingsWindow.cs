using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingsWindow : EditorWindow
{
    /// <summary>
    /// �궨���б�
    /// </summary>
    private List<MacorItem> m_List = new List<MacorItem>();
    /// <summary>
    /// �궨��״̬�ֵ�
    /// </summary>
    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();
    /// <summary>
    /// Unity�궨���ֶ�
    /// </summary>
    private string m_Macor = null;

    private void OnEnable()
    {
        //��ȡ��ǰ�궨���ֶ�
#if UNITY_STANDALONE_WIN
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
#elif UNITY_ANDROID
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
#elif UNITY_IPHONE
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
#endif

        //��ʼ����ǰ�궨���б�
        m_List.Clear();
        m_List.Add(new MacorItem() { Name = "DEBUG_MODEL", DisplayName = "����ģʽ", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DEBUG_REAL", DisplayName = "����ģʽ", IsDebug = false, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "DEBUG_LOG", DisplayName = "��ӡ��־", IsDebug = true, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "HOTFIX_ENABLE", DisplayName = "�����Ȳ���", IsDebug = false, IsRelease = true });
        //���õ�ǰ�궨��״̬
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

        //����һ��
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("����", GUILayout.Width(100)))
        {
            SaveMacor();
        }

        if (GUILayout.Button("����ģʽ", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            //SaveMacor();
        }

        if (GUILayout.Button("����ģʽ", GUILayout.Width(100)))
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
    /// ����궨��
    /// </summary>
    private void SaveMacor()
    {
        //���ú궨���ֶ�
        m_Macor = string.Empty;
        foreach (var item in m_Dic)
        {
            if (item.Value)
            {
                m_Macor += string.Format("{0};", item.Key);
            }
        }
        //����ǰ�ֶ�����Unity
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macor);
    }
}
