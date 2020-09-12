using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabStats : MonoBehaviour
{
    private int newscore;
    //private int highscore;
    public GameObject maincanvas;
    public GameObject Leaderboard;
    public GameObject LeaderboardListing;
    public Transform ListingContainer;


    #region SetScore
    public void SetScore(int score)
    {
        newscore = score;
    }
    #endregion SetScore

    #region PlayerStats

    //client direct update stats to cloud
    /*public void SetStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "HighScore", Value = newscore },
        }},
    result => { Debug.Log("User statistics updated"); },
    error => { Debug.LogError(error.GenerateErrorReport()); });
    }

      public  void GetStats()
        {
            PlayFabClientAPI.GetPlayerStatistics(
                new GetPlayerStatisticsRequest(),
                OnGetStats,
                error => Debug.LogError(error.GenerateErrorReport())
            );
        }

        public void OnGetStats(GetPlayerStatisticsResult result)
        {
            Debug.Log("Received the following Statistics:");
            foreach (var eachStat in result.Statistics)
            {
                switch(eachStat.StatisticName)
                {
                 case "HighScore":
                    highscore = eachStat.Value;
                    break;
                }
            }
    }*/

    // Build the request object and access the API
    public void StartCloudUpdatePlayerStats()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { NewHighScore= newscore }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudUpdatePlayerStats, OnErrorShared);
    }
    // OnCloudHelloWorld defined in the next code block
    private static void OnCloudUpdatePlayerStats(ExecuteCloudScriptResult result)
    {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    #endregion PlayerStats

    #region leaderboard
    public void GetLeaderboard()
    {
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0,StatisticName="HighScore",MaxResultsCount=10};
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, leaderboardresult, leaderboarderror);
    }

    private void leaderboardresult(GetLeaderboardResult result)
    { 
        Leaderboard.SetActive(true);
        maincanvas.SetActive(false);
        foreach(PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject templisting = Instantiate(LeaderboardListing, ListingContainer);
            LeaderboardList LL = templisting.GetComponent<LeaderboardList>();
            LL.playername.text = player.DisplayName;
            LL.score.text = player.StatValue.ToString();
            
        }
    }

    public void CloseLeaderboard()
    {
        Leaderboard.SetActive(false);
        maincanvas.SetActive(true);
        for(int i=ListingContainer.childCount -1;i>=0;i--)
        {
            Destroy(ListingContainer.GetChild(i).gameObject);
        }
    }
    
    private void leaderboarderror(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
        Toast.Instance.Show(error.GenerateErrorReport(),2f);
    }
    #endregion leaderboard
}
