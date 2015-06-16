using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour {

    private Button loginBtn;
    private Button registBtn;
    private InputField accountFiled;

    private enum ButtonType
    {
        LOGIN,
        REGIST
    }

	// Use this for initialization
	void Start () {
        accountFiled = GameObject.Find("AccountField").GetComponent<InputField>();
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
        if(type == ButtonType.LOGIN)
        {
            LoginController.Instance.SendLoginRequest(account);
        }
        else if(type == ButtonType.REGIST)
        {

        }
    }
}
