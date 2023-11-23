using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayFabCurrencyManager : MonoBehaviour
{
    public TMP_Text coinsValueText;

    void Start()
    {
        GetVirtualCurrencies();
    }

    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetuserInventorySuccess, OnError);

    }

    void OnGetuserInventorySuccess(GetUserInventoryResult result)
    {
        int coinsAmt = result.VirtualCurrency["CN"];
        coinsValueText.text = "Coins: " + coinsAmt.ToString();
    }

    private void OnError(PlayFabError e)
    {
        //messageBox.text = "Error" + e.GenerateErrorReport();
        Debug.Log("Error" + e.GenerateErrorReport());
    }
}
