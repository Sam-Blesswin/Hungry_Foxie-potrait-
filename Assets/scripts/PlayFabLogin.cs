using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    private string usermailid;
    private string userpassword;
    private string username;
    public GameObject loginpanel;

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
        if(PlayerPrefs.HasKey("EMAIL"))
        {
            usermailid = PlayerPrefs.GetString("EMAIL");
            userpassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest { Email = usermailid, Password = userpassword };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
       
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", usermailid);        //stores the mailid
        PlayerPrefs.SetString("PASSWORD", userpassword);    //stores the password
        loginpanel.SetActive(false);
    }

    private void onregistersuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", usermailid);
        PlayerPrefs.SetString("PASSWORD", userpassword);
        loginpanel.SetActive(false);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var register = new RegisterPlayFabUserRequest {Email = usermailid, Password = userpassword, Username = username};
        PlayFabClientAPI.RegisterPlayFabUser(register,onregistersuccess,onregisterfailure);
    }

    private void onregisterfailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void Getusermailid(string emailIn)
    {
        usermailid = emailIn;
    }

    public void Getuserpassword(string passwordIn)
    {
        userpassword = passwordIn;
    }

    public void Getusername(string usernameIn)
    {
        username = usernameIn;
    }

    public void Onclicklogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = usermailid, Password = userpassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
}