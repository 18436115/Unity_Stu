using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏全局参数
/// </summary>
public class GameGlobal : Singleton<GameGlobal>
{
    //------------------------------------------------------------------------------------------
    /// <summary>
    /// 屏幕宽度
    /// </summary>
    public float SceenWidth;
    /// <summary>
    /// 屏幕高度
    /// </summary>
    public float SceenHeight;
    //-------------------------------------------------------------------------------------------
    /// <summary>
    /// 服务器时间
    /// </summary>
    public long ServerTime;

}
