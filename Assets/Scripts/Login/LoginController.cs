using System.Collections;
using System.IO;
using login_message;
using ProtoBuf;
using UnityEngine;
using System.Collections.Generic;

public class LoginController {

    private static LoginController instance = null;

    public static LoginController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoginController();
            }

            return instance;
        }
    }

    private LoginController()
    {

    }

    public void SendLoginRequest(string account)
    {
        CMsgRoleLoginRequest request = new CMsgRoleLoginRequest();
        request.nickname = account;

        MemoryStream stream = new MemoryStream();
        Serializer.Serialize<CMsgRoleLoginRequest>(stream, request);

        NetManager.Instance.Send(Message.MSG_ROLE_LOGIN_REQUEST_C2S, stream);
    }

    private void processRoleLoginResponse(MemoryStream stream)
    {        
        CMsgRoleLoginResponse response = ProtoBuf.Serializer.Deserialize<CMsgRoleLoginResponse>(stream);
        List<RoleInfo> roles = response.role;
        Debug.Log("---role number " + roles.Count);
        foreach(RoleInfo role in roles)
        {
            Debug.Log("---" + role.nickname);
            Debug.Log("---" + role.level);
        }
    }

    public void OnLoginMessageResponse(int opcode, MemoryStream stream)
    {
        Debug.Log("---login controller receive opcode " + opcode);
        switch(opcode)
        {
            case Message.MSG_ROLE_LOGIN_RESPONSE_S2C & 0x0000FFFF:
                processRoleLoginResponse(stream);
                break;
            default:
                break;
        }
    }
}
