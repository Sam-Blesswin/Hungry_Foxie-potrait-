using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public class PlayFabLogin: MonoBehaviour
{
    private string usermailid;
    private string userpassword;
    private string username;

    public GameObject loginpanel;
    public GameObject Addloginpanel;
    public GameObject RecoverButton;
    public GameObject oldRecoverButton;



    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "3461F";
        }

        //**playfabcustom login**
        // var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        // PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        //delete previous logins
        //PlayerPrefs.DeleteAll();

        //**automatic mail login if email exists**
        if (PlayerPrefs.HasKey("USERNAME"))
        {
            username = PlayerPrefs.GetString("USERNAME");
            userpassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithPlayFabRequest { Username = username, Password = userpassword };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        }
        else
        {
            //Anonymous mobile login
            var reqAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = GetAndroidId(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(reqAndroid, OnAndroidLoginSuccess, OnAndroidLoginFailure);
        }

    }


    #region Login
    private string GetAndroidId()
    {
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        return deviceId;
    }

    private void OnAndroidLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call -Android!");
        Toast.Instance.Show("Congratulations, you made your first successful API call -Android!",2f);
        loginpanel.SetActive(false);
    }

    private void OnAndroidLoginFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        Toast.Instance.Show(error.GenerateErrorReport(),2f);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        Toast.Instance.Show("Congratulations, you made your first successful API call!", 2f);
        PlayerPrefs.SetString("USERNAME", username);        //stores the mailid
        PlayerPrefs.SetString("PASSWORD", userpassword);    //stores the password
        loginpanel.SetActive(false);
        RecoverButton.SetActive(false);
        oldRecoverButton.SetActive(false);
    }

    private void onregistersuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        Toast.Instance.Show("Congratulations, you made your first successful API call!", 2f);
        PlayerPrefs.SetString("USERNAME", username);
        PlayerPrefs.SetString("PASSWORD", userpassword);
        loginpanel.SetActive(false);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var register = new RegisterPlayFabUserRequest { RequireBothUsernameAndEmail = false, Username = username, Password = userpassword };
        PlayFabClientAPI.RegisterPlayFabUser(register, onregistersuccess, onregisterfailure);
    }

    private void onregisterfailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        Toast.Instance.Show(error.GenerateErrorReport(),2f);
    }

    public void Getusername(string usernameIn)
    {
        username = usernameIn;
    }
    public void GetUsermailid(string usermailidIn)
    {
        usermailid = usermailidIn;
    }
    public void Getuserpassword(string passwordIn)
    {
        userpassword = passwordIn;
    }

    public void Onclicklogin()
    {
        var request = new LoginWithPlayFabRequest { Username = username, Password = userpassword };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    public void OpenAddLogin()
    {
        Addloginpanel.SetActive(true);
    }

    public void OpenLogin()
    {
        loginpanel.SetActive(true);
    }

    public void OnClickAddLogin()
    {
        var addloginrequest = new AddUsernamePasswordRequest { Email = usermailid, Username = username, Password = userpassword };
        PlayFabClientAPI.AddUsernamePassword(addloginrequest, addloginrequestsuccess, addloginreuestfailure);
    }

    private void addloginreuestfailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        Toast.Instance.Show(error.GenerateErrorReport(),2f);
    }

    private void addloginrequestsuccess(AddUsernamePasswordResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("USERNAME", username);
        PlayerPrefs.SetString("PASSWORD", userpassword);
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest { DisplayName = username },
            displaynamesuccess,
            addloginreuestfailure);

        Addloginpanel.SetActive(false);
        RecoverButton.SetActive(false);
    }

    private void displaynamesuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName);
    }

    public void OnclickcloseLogin()
    {
        loginpanel.SetActive(false);
    }

    public void OnclickcloseAddLogin()
    {
        Addloginpanel.SetActive(false);
    }

    #endregion Login

}
