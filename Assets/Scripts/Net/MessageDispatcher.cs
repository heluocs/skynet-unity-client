using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class MessageDispatcher
{
    private static MessageDispatcher instance = null;

    public delegate void LoginDelegate(int msgno, MemoryStream stream);
    LoginDelegate loginDelegate;

    private MessageDispatcher()
    {
        loginDelegate += LoginController.Instance.OnMessageResponse;
    }

    public static MessageDispatcher Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new MessageDispatcher();
            }
            return instance;
        }
    }

    public void DispatchMessage()
    {
        Protocol protocol = NetManager.Instance.GetRecvMessage();
        if (protocol == null)
        {
            return;
        }

        MemoryStream stream = protocol.stream;
        int msgno = protocol.msgno; //消息号
        int module = msgno >> 16; //模块号
        int opcode = msgno & 0x0000FFFF; //操作码
        Debug.Log("---dispatch message module ---" + module + "--- msgno---" + msgno);

        switch(module)
        {
            case Message.MSG_LOGIN_MODULE_NO >> 16:
                loginDelegate(opcode, stream);
                break;
            case Message.MSG_ROLE_MODULE_NO >> 16:
                RoleController.Instance.OnMessageResponse(opcode, stream);
                break;
            default:
                break;
        }
    }
}

