//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2024-04-22 13:22:41
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送删除角色消息
/// </summary>
public struct RoleOperation_DeleteRoleProto : IProto
{
    public ushort ProtoCode { get { return 10005; } }

    public int RoleId; //角色ID

    public byte[] ToArray()
    {
        using (MemoryStreamTool ms = new MemoryStreamTool())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            return ms.ToArray();
        }
    }

    public static RoleOperation_DeleteRoleProto GetProto(byte[] buffer)
    {
        RoleOperation_DeleteRoleProto proto = new RoleOperation_DeleteRoleProto();
        using (MemoryStreamTool ms = new MemoryStreamTool(buffer))
        {
            proto.RoleId = ms.ReadInt();
        }
        return proto;
    }
}