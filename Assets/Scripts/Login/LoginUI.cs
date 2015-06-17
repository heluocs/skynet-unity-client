using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour {

    private Button loginBtn;
    private Button registBtn;
    private InputField accountFiled;
    private Text msgText;

    private static LoginUI instance;

    public static LoginUI Instance
    {
        get
        {
            return instance;
        }
    }

    private enum ButtonType
    {
        LOGIN,
        REGIST
    }

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        accountFiled = GameObject.Find("AccountField").GetComponent<InputField>();
        msgText = GameObject.Find("MsgText").GetComponent<Text>();

        loginBtn = GameObject.Find("LoginBtn").GetComponent<Button>();
        loginBtn.onClick.AddListener(() => OnButtonClick(ButtonType.LOGIN));
        registBtn = GameObject.Find("RegistBtn").GetComponent<Button>();
        registBtn.onClick.AddListener(() => OnButtonClick(ButtonType.REGIST));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnButtonClick(ButtonType type)
    {
        string account = accountFiled.text;
        Debug.Log("---"+account+"---");
        if(account.Equals(""))
        {
            ShowErrorMsg("请先输入账号");
            return;
        }
        if(type == ButtonType.LOGIN)
        {
            LoginController.Instance.SendLoginRequest(account);
        }
        else if(type == ButtonType.REGIST)
        {
            LoginController.Instance.SendRegistRequest(account);
        }
    }

    public void ShowErrorMsg(string msg)
    {
        msgText.text = msg;
    }
}
