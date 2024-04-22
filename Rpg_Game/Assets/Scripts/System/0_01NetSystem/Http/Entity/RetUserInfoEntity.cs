public class RetUserInfoEntity 
{
    /// <summary>
    /// 编号
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///元宝 
    /// </summary>
    public int Money { get; set; }


    /// <summary>
    ///最后登录服务器Id 
    /// </summary>
    public int LastLogOnServerId { get; set; }


    /// <summary>
    ///最后登录服务器名称 
    /// </summary>
    public string LastLogOnServerName { get; set; }


    /// <summary>
    ///最后登录角色Id 
    /// </summary>
    public int LastLogOnRoleId { get; set; }

    /// <summary>
    ///最后登录角色名称 
    /// </summary>
    public string LastLogOnRoleNickName { get; set; }

    /// <summary>
    ///最后登录角色职业Id 
    /// </summary>
    public int LastLogOnRoleJobId { get; set; }
}
