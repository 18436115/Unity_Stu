//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2024-04-22 13:22:43
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回删除角色消息
/// </summary>
public struct RoleOperation_DeleteRoleReturnProto : IProto
{
    public ushort ProtoCode { get { return 10006; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码

    public byte[] ToArray()
    {
        using (MemoryStreamTool ms = new MemoryStreamTool())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static RoleOperation_DeleteRoleReturnProto GetProto(byte[] buffer)
    {
        RoleOperation_DeleteRoleReturnProto proto = new RoleOperation_DeleteRoleReturnProto();
        using (MemoryStreamTool ms = new MemoryStreamTool(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}