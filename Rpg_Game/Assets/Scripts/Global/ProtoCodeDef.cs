//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-02-25 22:40:37
//备    注：
//===================================================
using System.Collections;

/// <summary>
/// 协议编号定义
/// </summary>
public class ProtoCodeDef
{
    /// <summary>
    /// 用户登入--测试
    /// </summary>
    public const ushort User_LoginProto = 10000;


    //-----------------------------------------------------------------------------
    /// <summary>
    /// 客户端发送登录区服消息
    /// </summary>
    public const ushort RoleOperation_LogOnGameServerProto = 10001;

    /// <summary>
    /// 服务器返回登录信息
    /// </summary>
    public const ushort RoleOperation_LogOnGameServerReturnProto = 10002;

    /// <summary>
    /// 客户端发送创建角色消息
    /// </summary>
    public const ushort RoleOperation_CreateRoleProto = 10003;

    /// <summary>
    /// 服务器返回创建角色消息
    /// </summary>
    public const ushort RoleOperation_CreateRoleReturnProto = 10004;

    /// <summary>
    /// 客户端发送删除角色消息
    /// </summary>
    public const ushort RoleOperation_DeleteRoleProto = 10005;

    /// <summary>
    /// 服务器返回删除角色消息
    /// </summary>
    public const ushort RoleOperation_DeleteRoleReturnProto = 10006;

    /// <summary>
    /// 客户端发送进入游戏消息
    /// </summary>
    public const ushort RoleOperation_EnterGameProto = 10007;

    /// <summary>
    /// 服务器返回进入游戏消息
    /// </summary>
    public const ushort RoleOperation_EnterGameReturnProto = 10008;

    /// <summary>
    /// 客户端查询角色信息
    /// </summary>
    public const ushort RoleOperation_SelectRoleInfoProto = 10009;

    /// <summary>
    /// 服务器返回角色信息
    /// </summary>
    public const ushort RoleOperation_SelectRoleInfoReturnProto = 10010;



}
