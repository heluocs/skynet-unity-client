using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Message
{
    //消息号由模块号+操作号组成

    //Login Module
    public const int MSG_LOGIN_MODULE_NO                    = 0x00010000;
    public const int MSG_ROLE_LOGIN_REQUEST_C2S             = 0x00010001;
    public const int MSG_ROLE_LOGIN_RESPONSE_S2C            = 0x00010002;
    public const int MSG_ROLE_REGIST_REQUEST_C2S            = 0x00010003;
    public const int MSG_ROLE_REGIST_RESPONSE_S2C           = 0x00010004;

}