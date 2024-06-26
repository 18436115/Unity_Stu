//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2024-04-22 13:22:47
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回进入游戏消息
/// </summary>
public struct RoleOperation_EnterGameReturnProto : IProto
{
    public ushort ProtoCode { get { return 10008; } }

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

    public static RoleOperation_EnterGameReturnProto GetProto(byte[] buffer)
    {
        RoleOperation_EnterGameReturnProto proto = new RoleOperation_EnterGameReturnProto();
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