--用户登入
User_LoginProto = { ProtoCode = 10000, id = 0, username = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
User_LoginProto.__index = User_LoginProto;

function User_LoginProto.New()
    local self = { }; --初始化self
    setmetatable(self, User_LoginProto); --将self的元表设定为Class
    return self;
end


--发送协议
function User_LoginProto.SendProto(proto)

    local ms = CS.LuaHelper.Instance:CreateMemoryStream();
    ms:WriteUShort(proto.ProtoCode);

    ms:WriteInt(proto.id);
    ms:WriteUTF8String(proto.username);

    CS.LuaHelper.Instance:SendProto(ms:ToArray());
    ms:Dispose();
end


--解析协议
function User_LoginProto.GetProto(buffer)

    local proto = User_LoginProto.New(); --实例化一个协议对象
    local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);

    proto.id = ms:ReadInt();
    proto.username = ms:ReadUTF8String();

    ms:Dispose();
    return proto;
end