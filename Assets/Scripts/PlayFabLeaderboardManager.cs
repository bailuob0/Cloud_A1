using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;

public class PlayFabLeaderboardManager : MonoBehaviour
{
    [SerializeField]
    public List<LeaderboardItemUI> leaderboardItems;

    [SerializeField]
    public TMP_Text leaderboardName;
    

    public void Start()
    {
        if (leaderboardName)
        {
            OnButtonGetGlobalLeaderboard();
        }
    }
    public void OnButtonGetGlobalLeaderboard()
    {
        var lbreq = new GetLeaderboardRequest
        {
            StatisticName = "highscore",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(lbreq, OnLeaderboardGet, OnError);

        leaderboardName.text = "Global Leaderboard";
    }


    void OnLeaderboardGet(GetLeaderboardResult r)
    {
        int i = 0;

        foreach (var item in r.Leaderboard)
        {
            leaderboardItems[i].gameObject.SetActive(true);
            leaderboardItems[i].positionText.text = (item.Position + 1).ToString();
            leaderboardItems[i].usernameText.text = item.DisplayName;
            leaderboardItems[i].scoreText.text = item.StatValue.ToString();

            i++;
        }
    }
    public void OnButtonGetNearbyLeaderboard()
    {
        var lbreq = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "highscore",
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(lbreq, OnLeaderboardAroundPlayerGet, OnError);

        leaderboardName.text = "Nearby Leaderboard";

    }

     void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult r)
    {
        int i = 0;

        foreach (var item in r.Leaderboard)
        {
            leaderboardItems[i].gameObject.SetActive(true);
            leaderboardItems[i].positionText.text = (item.Position + 1).ToString();
            leaderboardItems[i].usernameText.text = item.DisplayName;
            leaderboardItems[i].scoreText.text = item.StatValue.ToString();

            i++;
        }
    }

    public void SendToLeaderboard(int score)
    {
        var req = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "highscore",
                    Value = score
                }
            }
        };
        UpdateMessage("Submitting score: " + score);
        PlayFabClientAPI.UpdatePlayerStatistics(req, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult r)
    {
        UpdateMessage("Successful leaderboard sent: " + r.ToString());
    }

    private void OnError(PlayFabError e)
    {
        Debug.Log("Error" + e.GenerateErrorReport());
    }

    private void UpdateMessage(string msg)
    {
        Debug.Log(msg);
    }

}
