using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RoleUI : MonoBehaviour {

    private static RoleUI instance;

    public static RoleUI Instance
    {
        get
        {
            return instance;
        }
    }

    private Button createBtn;
    private Button enterBtn;
    private InputField nicknameInput;
    private Text msgText;

    enum ButtonType
    {
        CREATE_ROLE,
        ENTER_GAME
    }

    void Awake() {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        createBtn = GameObject.Find("CreateBtn").GetComponent<Button>();
        createBtn.onClick.AddListener(() => OnButtonClick(ButtonType.CREATE_ROLE));
        enterBtn = GameObject.Find("EnterBtn").GetComponent<Button>();
        enterBtn.onClick.AddListener(() => OnButtonClick(ButtonType.ENTER_GAME));

        msgText = GameObject.Find("MsgText").GetComponent<Text>();
        nicknameInput = GameObject.Find("NicknameInput").GetComponent<InputField>();

        long accountid = ApplicationData.accountid;
        RoleController.Instance.SendRoleListRequest(accountid);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnButtonClick(ButtonType type)
    {
        string account = nicknameInput.text;
        Debug.Log("---" + account + "---");
        if (account.Equals(""))
        {
            ShowErrorMsg("昵称不能为空");
            return;
        }
        if (type == ButtonType.CREATE_ROLE)
        {
            RoleController.Instance.SendRoleCreateRequest(account);
        }
        else if (type == ButtonType.ENTER_GAME)
        {
            
        }
    }

    public void ShowErrorMsg(string msg)
    {
        this.msgText.text = msg;
    }

    public void ShowRoleList(List<role_message.Role> roles)
    {

    }
}
