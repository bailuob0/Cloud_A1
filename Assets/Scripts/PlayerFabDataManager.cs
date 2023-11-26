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

    [SerializeField] 
    private GameObject messageBox;

    private TMP_Text message;

    void Start()
    {
        message = messageBox.GetComponentInChildren<TMP_Text>();
    }

    // call when player buys stuff + logs out
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
       UpdateMessage("Data sent succesfully");
    }

    //call on login
    public void GetData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetDataSuccess, OnError);
        UpdateMessage("Data Retrieved");
    }

    void OnGetDataSuccess(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Stats"))
        {
            JsonUtility.FromJsonOverwrite(result.Data["Stats"].Value, shipStats);
        }

        // if no existing save exists, set default values and send 
        else
        {
            shipStats.Reset();
            SendData();
        }
    }

    private void OnError(PlayFabError e)
    {
        UpdateMessage("Error" + e.GenerateErrorReport());
    }

    public void UpdateMessage(string msg)
    {
        StartCoroutine(UpdateMessageBox(msg));
    }

    private IEnumerator UpdateMessageBox(string msg)
    {
       
        messageBox.SetActive(true);
        
        message.text = msg;

        yield return new WaitForSeconds(2);

        message.text = " ";

        messageBox.gameObject.SetActive(false);
    }
}
