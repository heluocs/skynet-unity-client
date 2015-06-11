using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour {

    private Button loginBtn;
    private InputField accountFiled;

	// Use this for initialization
	void Start () {
        accountFiled = GameObject.Find("AccountField").GetComponent<InputField>();
        loginBtn = GameObject.Find("LoginBtn").GetComponent<Button>();
        loginBtn.onClick.AddListener(() => OnButtonClick());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnButtonClick()
    {
        string account = accountFiled.text;
        Debug.Log("---"+account+"---");
        LoginController.Instance.SendLoginRequest(account);
    }
}
