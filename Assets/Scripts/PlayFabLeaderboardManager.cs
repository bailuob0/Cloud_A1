using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;

public class PlayFabLeaderboardManager : MonoBehaviour
{
    [SerializeField] 
    private TMP_InputField if_score, if_displayName;

    [SerializeField]
    public List<LeaderboardItemUI> leaderboardItems;
    
    public void OnButtonGetLeaderboard()
    {
        var lbreq = new GetLeaderboardRequest
        {
            StatisticName = "highscore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(lbreq, OnLeaderboardGet, OnError);
    }

    // from practical
    /*
    void OnLeaderboardGet(GetLeaderboardResult r)
    {
        string LeaderboardStr = "Leaderboard\n";

        foreach (var item in r.Leaderboard)
        {
            string onerow = item.Position + "/" + item.DisplayName + "/" + item.StatValue + "\n";
            LeaderboardStr += onerow;
        }

        UpdateMessage(LeaderboardStr);
    }

    
     public void OnButtonSendLeaderboard()
    {
        var req = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "highscore",
                    Value = int.Parse(if_score.text)
                }
            }
        };
        UpdateMessage("Submitting score: " + if_score.text);
        PlayFabClientAPI.UpdatePlayerStatistics(req, OnLeaderboardUpdate, OnError);
    }
    */

    void OnLeaderboardGet(GetLeaderboardResult r)
    {
        int i = 0;

        foreach (var item in r.Leaderboard)
        {
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
        //messageBox.text = "Error" + e.GenerateErrorReport();
        Debug.Log("Error" + e.GenerateErrorReport());
    }

    private void UpdateMessage(string msg)
    {
        Debug.Log(msg);
    }

}
