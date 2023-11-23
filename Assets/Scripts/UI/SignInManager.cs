using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SignInManager : MonoBehaviour
{
    public GameObject usernameInput;
    public GameObject emailInput;
    public GameObject usernameButton;
    public GameObject emailButton;

    public GameObject hideButton;
    public GameObject showButton;

    public TMP_InputField passwordInput;


    public void ChangeToEmailInput()
    {
        emailButton.SetActive(true);
        emailInput.SetActive(true);
        usernameButton.SetActive(false);
        usernameInput.SetActive(false);

    }

    public void ChangeToUsernameInput()
    {
        usernameButton.SetActive(true);
        usernameInput.SetActive(true);
        emailButton.SetActive(false);
        emailInput.SetActive(false);
        
    }

    public void HidePassword()
    {
        hideButton.SetActive(false);
        showButton.SetActive(true);

        passwordInput.contentType = TMP_InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();
    }

    public void ShowPassword()
    {
        hideButton.SetActive(true);
        showButton.SetActive(false);

        passwordInput.contentType = TMP_InputField.ContentType.Standard;
        passwordInput.ForceLabelUpdate();

    }

}
