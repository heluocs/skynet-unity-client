using UnityEngine;
using System.Collections;
using System.IO;

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
        Debug.Log("---login controller receive opcode " + opcode);
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

    }

    public void SendRoleListRequest(long accountid)
    {
        Debug.Log("---accountid:" + accountid);
    }

    public void SendRoleCreateRequest(string nickname)
    {

    }
}
