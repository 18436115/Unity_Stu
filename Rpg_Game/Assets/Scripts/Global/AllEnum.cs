using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ��Ϸϵͳ����
/// <summary>
/// ��Ϸϵͳ����
/// </summary>
public enum SystemType
{
    None,
    /// <summary>
    /// ����
    /// </summary>
    Scene,
    /// <summary>
    /// ��Ϣ
    /// </summary>
    Msg,
    /// <summary>
    /// ����
    /// </summary>
    UI,
    /// <summary>
    /// ����
    /// </summary>
    Guide,
    /// <summary>
    /// ����
    /// </summary>
    Task,
    /// <summary>
    /// ��ɫ
    /// </summary>
    Role,
}
#endregion

#region ��Ϸ��������
/// <summary>
/// UI��������
/// </summary>
public enum UIContainerType
{
    UICT_TopLeft         //��������
    ,
    UICT_TopRight        //��������
    ,
    UICT_Center          //�м�����
    ,
    UICT_BottomLeft      //��������
    ,
    UICT_BottomRight     //��������
}

/// <summary>
/// UI�㼶����
/// </summary>
public enum UILayerType
{
    /// <summary>
    /// ��Ϸ������  ��ײ�
    ///     ������UI  ��������� Ҳ�����ǵ���
    /// </summary>
    UILT_Scene = 0,  //������

    /// <summary>
    /// ������� ��
    ///     ��һЩUI��ť eg:����,�ɾ�,�����ȵȹ�����ڲ�
    /// </summary>
    UILT_Function = 10,

    /// <summary>
    /// ���˵���
    ///     һ��Ϊ���������ť ������ʾ������UI֮��
    ///     ���˵���
    /// </summary>
    UILT_MainMenu = 20, //���˵�  

    /// <summary>
    /// չʾ�����
    ///     eg:������ڲ� �����Ӧ���֮���UI����  һ��Ϊȫ����
    /// </summary>
    UILT_OverView = 30,

    /// <summary>
    ///  ��ͨ�Ի���
    /// Ϊ չʾ������  �����ĶԻ���
    /// </summary>
    UILT_DialogView = 40,

    /// <summary>
    /// ͨ�öԻ����
    ///  һ��Ϊ�����Ի���
    /// </summary>
    UILT_ComDialogView = 50,

    /// <summary>
    /// ��ʾ���
    ///     ��Ҫ���ڸ������ ����һЩ�ɾ�
    ///     ����һЩ ��Ҫ�����㼶��
    /// </summary>
    UILT_TipView = 60,

    /// <summary>
    /// ������
    /// ������ʾ�������ҳ��
    /// </summary>
    UILT_GuideView = 70, //��������� 

    /// <summary>
    /// ��ȡ��Ʒ
    /// </summary>
    UILT_ReceiveView = 80,

    /// <summary>
    /// ��ʾ���� 
    /// eg:������ʾ���� �� �ӽ�Ҽ������ȵ�
    /// </summary>
    UILT_ToastView = 90,

    /// <summary>
    /// loading����
    /// </summary>
    UILT_LodingView = 100
}

/// <summary>
/// UI��������
/// </summary>
public enum UIAnimType
{
    UIAT_None //Ĭ��û�ж���
    ,
    UIAT_Left //����ߵ���Ļ�м�
    ,
    UIAT_Right //���ұߵ���Ļ�м�
    ,
    UIAT_Center //��0�Ŵ�1
    ,
    UIAT_Top //���ϱߵ���Ļ�м�
    ,
    UIAT_Bottom //���±ߵ���Ļ�м�
    ,
    UIAT_BottomNoAni //���±ߵ���Ļ�м�(�]����Ӯ�)
    ,
    UIAT_Custom //�Զ���
    ,
}
#endregion
