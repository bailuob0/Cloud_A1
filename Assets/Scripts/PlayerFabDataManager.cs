using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Unity.VisualScripting;

public class PlayerFabDataManager : MonoBehaviour
{
    [SerializeField] ShipStats shipStats;


    // call when player buys stuff
    public void SendData()
    {
        string shipStatsAsJson = JsonUtility.ToJson(shipStats);

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Stats", shipStatsAsJson}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnSendDataSuccess, OnError);
    }

    void OnSendDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Data sent succesfully");
    }

    //call on login
    public void GetData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetDataSuccess, OnError);
    }

    void OnGetDataSuccess(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Stats"))
        {
            ShipStats savedStats = JsonUtility.FromJson<ShipStats>(result.Data["Stats"].Value);
        }
    }

    private void OnError(PlayFabError e)
    {
        Debug.Log("Error" + e.GenerateErrorReport());
    }
}
