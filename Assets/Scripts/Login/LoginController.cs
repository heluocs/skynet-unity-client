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
        CMsgAccountLoginRequest request = new CMsgAccountLoginRequest();
        request.account = account;

        MemoryStream stream = new MemoryStream();
        Serializer.Serialize<CMsgAccountLoginRequest>(stream, request);

        NetManager.Instance.Send(Message.MSG_ACCOUNT_LOGIN_REQUEST_C2S, stream);
    }

    public void SendRegistRequest(string account)
    {
        CMsgAccountRegistRequest request = new CMsgAccountRegistRequest();
        request.account = account;

        MemoryStream stream = new MemoryStream();
        Serializer.Serialize<CMsgAccountRegistRequest>(stream, request);

        NetManager.Instance.Send(Message.MSG_ACCOUNT_REGIST_REQUEST_C2S, stream);
    }

    private void processAccountLoginResponse(MemoryStream stream)
    {
        CMsgAccountLoginResponse response = ProtoBuf.Serializer.Deserialize<CMsgAccountLoginResponse>(stream);
        long accountid = response.accountid;
        Debug.Log("-------accountid:" + accountid);
    }

    private void processAccountRegistResponse(MemoryStream stream)
    {
        CMsgAccountRegistResponse response = ProtoBuf.Serializer.Deserialize<CMsgAccountRegistResponse>(stream);
        long accountid = response.accountid;
        Debug.Log("-------accountid:" + accountid);
    }

    public void OnLoginMessageResponse(int opcode, MemoryStream stream)
    {
        Debug.Log("---login controller receive opcode " + opcode);
        switch(opcode)
        {
            case Message.MSG_ACCOUNT_LOGIN_RESPONSE_S2C & 0x0000FFFF:
                processAccountLoginResponse(stream);
                break;
            case Message.MSG_ACCOUNT_REGIST_RESPONSE_S2C & 0x0000FFFF:
                processAccountRegistResponse(stream);
                break;
            default:
                break;
        }
    }
}
