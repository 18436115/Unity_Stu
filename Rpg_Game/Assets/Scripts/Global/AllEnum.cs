using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 游戏系统类型
/// <summary>
/// 游戏系统类型
/// </summary>
public enum SystemType
{
    None,
    /// <summary>
    /// 场景
    /// </summary>
    Scene,
    /// <summary>
    /// 消息
    /// </summary>
    Msg,
    /// <summary>
    /// 界面
    /// </summary>
    UI,
    /// <summary>
    /// 引导
    /// </summary>
    Guide,
    /// <summary>
    /// 任务
    /// </summary>
    Task,
    /// <summary>
    /// 角色
    /// </summary>
    Role,
}
#endregion

#region 游戏界面类型
/// <summary>
/// UI容器类型
/// </summary>
public enum UIContainerType
{
    UICT_TopLeft         //上左容器
    ,
    UICT_TopRight        //上右容器
    ,
    UICT_Center          //中间容器
    ,
    UICT_BottomLeft      //下左容器
    ,
    UICT_BottomRight     //下右容器
}

/// <summary>
/// UI层级类型
/// </summary>
public enum UILayerType
{
    /// <summary>
    /// 游戏场景层  最底层
    ///     场景的UI  可以是最底 也可以是叠加
    /// </summary>
    UILT_Scene = 0,  //场景层

    /// <summary>
    /// 功能入口 层
    ///     如一些UI按钮 eg:背包,成就,炮塔等等功能入口层
    /// </summary>
    UILT_Function = 10,

    /// <summary>
    /// 主菜单层
    ///     一般为下面五个按钮 用于显示在所有UI之上
    ///     除了弹窗
    /// </summary>
    UILT_MainMenu = 20, //主菜单  

    /// <summary>
    /// 展示界面层
    ///     eg:功能入口层 点击相应入口之后的UI界面  一般为全屏的
    /// </summary>
    UILT_OverView = 30,

    /// <summary>
    ///  普通对话框
    /// 为 展示界面点击  产生的对话框
    /// </summary>
    UILT_DialogView = 40,

    /// <summary>
    /// 通用对话框层
    ///  一般为公共对话框
    /// </summary>
    UILT_ComDialogView = 50,

    /// <summary>
    /// 提示语层
    ///     主要用于浮动广告 或者一些成就
    ///     还有一些 需要调整层级的
    /// </summary>
    UILT_TipView = 60,

    /// <summary>
    /// 引导层
    /// 用于显示引导相关页面
    /// </summary>
    UILT_GuideView = 70, //引导界面层 

    /// <summary>
    /// 获取物品
    /// </summary>
    UILT_ReceiveView = 80,

    /// <summary>
    /// 提示文字 
    /// eg:各种提示文字 如 加金币加体力等等
    /// </summary>
    UILT_ToastView = 90,

    /// <summary>
    /// loading界面
    /// </summary>
    UILT_LodingView = 100
}

/// <summary>
/// UI动画类型
/// </summary>
public enum UIAnimType
{
    UIAT_None //默认没有动画
    ,
    UIAT_Left //从左边到屏幕中间
    ,
    UIAT_Right //从右边到屏幕中间
    ,
    UIAT_Center //从0放大到1
    ,
    UIAT_Top //从上边到屏幕中间
    ,
    UIAT_Bottom //从下边到屏幕中间
    ,
    UIAT_BottomNoAni //从下边到屏幕中间(沒緩冲動畫)
    ,
    UIAT_Custom //自定义
    ,
}
#endregion
