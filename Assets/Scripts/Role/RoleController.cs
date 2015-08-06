using UnityEngine;
using System.Collections;
using System.IO;
using role_message;
using System.Collections.Generic;
using ProtoBuf;

public class RoleController : MessageController {

    private static RoleController instance;

    public static RoleController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new RoleController();
            }
            return instance;
        }
    }

    private RoleController()
    {

    }

    public void OnMessageResponse(int opcode, MemoryStream stream)
    {
        Debug.Log("---role controller receive opcode " + opcode);
        switch (opcode)
        {
            case Message.MSG_ROLE_LIST_RESPONSE_S2C & 0x0000FFFF:
                OnRoleListResponse(stream);
                break;
            case Message.MSG_ROLE_CREATE_RESPONSE_S2C & 0x0000FFFF:
                OnRoleCreateResponse(stream);
                break;
            default:
                break;
        }
    }

    private void OnRoleCreateResponse(MemoryStream stream)
    {

    }

    private void OnRoleListResponse(MemoryStream stream)
    {
        CMsgRoleListResponse response = ProtoBuf.Serializer.Deserialize<CMsgRoleListResponse>(stream);
        List<Role> list = response.roles;
        Debug.Log("---role list---" + list.Capacity);
    }

    public void SendRoleListRequest(long accountid)
    {
        Debug.Log("---accountid:" + accountid);
        CMsgRoleListRequest request = new CMsgRoleListRequest();
        request.account = accountid.ToString();

        MemoryStream stream = new MemoryStream();
        Serializer.Serialize<CMsgRoleListRequest>(stream, request);

        NetManager.Instance.Send(Message.MSG_ROLE_LIST_REQUEST_C2S, stream);
    }

    public void SendRoleCreateRequest(string nickname)
    {

    }
}
