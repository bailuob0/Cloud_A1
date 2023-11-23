using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using Unity.VisualScripting;

/*
    script displays username, email in game ui

*/
public class UserUIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text usernameText;

    void Start()
    {
        var req = new GetAccountInfoRequest();

        usernameText.text = "User:" + req.TitleDisplayName;
    }

    private void UpdateCurrentPlayerUI()
    {

    }
}
