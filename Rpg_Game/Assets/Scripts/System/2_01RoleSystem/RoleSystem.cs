using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSystem : IGameSystem
{
    public RoleSystem(GameManager gameManager) : base(gameManager)
    {
        m_GameMgr.GameSystemDic.Add(SystemType.Role, this);
    }
    public override void Init() 
    {
        ((MsgSystem)GameManager.Instance.GameSystemDic[SystemType.Msg]).AddNetEvent(
            ProtoCodeDef.RoleOperation_LogOnGameServerReturnProto, OnLogOnGameServerReturnProto);
    }

    private void OnLogOnGameServerReturnProto(byte[] buffer)
    {
        RoleOperation_LogOnGameServerReturnProto roleOperation_LogOnGameServerReturn = RoleOperation_LogOnGameServerReturnProto.GetProto(buffer);
        AppDebug.Log("RoleCount: " + roleOperation_LogOnGameServerReturn.RoleCount);
        if (roleOperation_LogOnGameServerReturn.RoleCount > 0)
        {
            List<RoleOperation_LogOnGameServerReturnProto.RoleItem> roleItems = roleOperation_LogOnGameServerReturn.RoleList;
            for (int i = 0; i < roleItems.Count; i++)
            {
                AppDebug.Log("username = " + roleItems[i].RoleNickName);
            }
            //选择角色
        }
        else 
        {
            //创建角色
        }

        
    }
}
