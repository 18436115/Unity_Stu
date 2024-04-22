//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2024-04-10 18:00:32
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 用户登入
/// </summary>
public struct User_LoginProto : IProto
{
    public ushort ProtoCode { get { return 10000; } }

    public int id; //
    public string username; //

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(id);
            ms.WriteUTF8String(username);
            return ms.ToArray();
        }
    }

    public static User_LoginProto GetProto(byte[] buffer)
    {
        User_LoginProto proto = new User_LoginProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.id = ms.ReadInt();
            proto.username = ms.ReadUTF8String();
        }
        return proto;
    }
}