public class RetUserInfoEntity 
{
    /// <summary>
    /// ���
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///Ԫ�� 
    /// </summary>
    public int Money { get; set; }


    /// <summary>
    ///����¼������Id 
    /// </summary>
    public int LastLogOnServerId { get; set; }


    /// <summary>
    ///����¼���������� 
    /// </summary>
    public string LastLogOnServerName { get; set; }


    /// <summary>
    ///����¼��ɫId 
    /// </summary>
    public int LastLogOnRoleId { get; set; }

    /// <summary>
    ///����¼��ɫ���� 
    /// </summary>
    public string LastLogOnRoleNickName { get; set; }

    /// <summary>
    ///����¼��ɫְҵId 
    /// </summary>
    public int LastLogOnRoleJobId { get; set; }
}
