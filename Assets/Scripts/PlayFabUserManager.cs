using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayFabUserManager : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text messageBox;
    [SerializeField] 
    private TMP_InputField if_username, if_email, if_password, if_displayName;

    [SerializeField]
    PlayerFabDataManager dataManager;
   
    public void OnButtonRegisterUser()
    {
         var regReq = new RegisterPlayFabUserRequest {
             Email = if_email.text,
             Password = if_password.text,
             Username = if_username.text
         };
 
         PlayFabClientAPI.RegisterPlayFabUser(regReq, OnRegisterSuccess, OnError);
    }
 
    
    public void OnRegisterSuccess(RegisterPlayFabUserResult r)
    {
         if (messageBox)
         {
            messageBox.text = "Register Success!" + r.PlayFabId;
         }

         else
            Debug.Log("Register Success!" + r.PlayFabId);
 
         var req = new UpdateUserTitleDisplayNameRequest
         {
             DisplayName = if_displayName.text,
         };
 
         PlayFabClientAPI.UpdateUserTitleDisplayName(req, OnDisplayNameUpdate, OnError);
    }

    private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult r)
    {
        UpdateMessage("display name updated!" + r.DisplayName);
    }

    private void OnError(PlayFabError e)
    {
        //messageBox.text = "Error" + e.GenerateErrorReport();
        Debug.Log("Error" + e.GenerateErrorReport());
    }

    public void UpdateMessage(string msg)
    {
        if (messageBox)
        {
            messageBox.text = msg;
            return;
        }
        
        Debug.Log(msg);
    }

    void OnLoginSuccess(LoginResult r)
    {
        UpdateMessage("Login Success! " + r.PlayFabId + " " + r.InfoResultPayload.PlayerProfile.DisplayName);
        dataManager.GetData();
    }

    public void OnButtonLoginEmail()
    {
        var loginRequest = new LoginWithEmailAddressRequest
        {
            Email = if_email.text,
            Password = if_password.text,

            //to get player profile and get displayname
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnLoginSuccess, OnError);
    }

    public void OnButtonLoginUserName()
    {
         var loginRequest = new LoginWithPlayFabRequest
        {
            Username = if_username.text,
            Password = if_password.text,

            //to get player profile and get displayname
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, OnLoginSuccess, OnError);
    }

    public void OnButtonGuestLogin()
    {
        var loginRequest = new LoginWithEmailAddressRequest
        {
            Email = "guest@gmail.com",
            Password = "123456",

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnLoginSuccess, OnError);
    }

    public void OnButtonLogin()
    {
        if (if_email.isActiveAndEnabled && !if_username.isActiveAndEnabled)
        {
            OnButtonLoginEmail();
            SceneManager.LoadScene("Menu");
            return;
        }

        if (!if_email.isActiveAndEnabled && if_username.isActiveAndEnabled)
        {
            OnButtonLoginUserName();
            SceneManager.LoadScene("Menu");
            return;
        }
    }

    public void OnButtonLogout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        SceneManager.LoadScene("StartMenu");

        Debug.Log("Successfully logged out!");
    }
}
