using System.Collections;
using System.IO;
using login_message;
using ProtoBuf;
using UnityEngine;
using System.Collections.Generic;

public class LoginController : MessageController {

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

    public void OnMessageResponse(int opcode, MemoryStream stream)
    {
        Debug.Log("---login controller receive opcode " + opcode);
        switch (opcode)
        {
            case Message.MSG_ACCOUNT_LOGIN_RESPONSE_S2C & 0x0000FFFF:
                OnAccountLoginResponse(stream);
                break;
            case Message.MSG_ACCOUNT_REGIST_RESPONSE_S2C & 0x0000FFFF:
                OnAccountRegistResponse(stream);
                break;
            default:
                break;
        }
    }

    private void OnAccountLoginResponse(MemoryStream stream)
    {
        CMsgAccountLoginResponse response = ProtoBuf.Serializer.Deserialize<CMsgAccountLoginResponse>(stream);
        long accountid = response.accountid;
        Debug.Log("-------accountid:" + accountid);
        if(accountid == 0)
        {
            LoginUI.Instance.ShowErrorMsg("该用户不存在");
        }
        else
        {
            ApplicationData.accountid = accountid;
            Application.LoadLevel("role");
        }
        
    }

    private void OnAccountRegistResponse(MemoryStream stream)
    {
        CMsgAccountRegistResponse response = ProtoBuf.Serializer.Deserialize<CMsgAccountRegistResponse>(stream);
        long accountid = response.accountid;
        Debug.Log("-------accountid:" + accountid);
        if (accountid == 0)
        {
            LoginUI.Instance.ShowErrorMsg("该用户不存在");
        }
        else
        {
            ApplicationData.accountid = accountid;
            Application.LoadLevel("role");
        }
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
}
