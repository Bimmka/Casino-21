using System;
using System.Collections.Generic;
using Features.Constants;
using Features.StaticData.Leaderboard;
using LootLocker.Requests;
using UnityEngine;

namespace Features.Services.Leaderboard
{
  public class LeaderboardService : ILeaderboard
  {
    private readonly LeaderboardSettings settings;

    private string playerID;

    public LeaderboardService(LeaderboardSettings settings)
    {
      this.settings = settings;
    }
    
    public void Login(Action<bool> callback = null) => 
      LootLockerSDKManager.StartGuestSession((response) => { OnLogin(callback, response); });

    public void SetNickname(string nickname, Action<bool> callback = null) => 
      LootLockerSDKManager.SetPlayerName(nickname, (response) => OnSetName(response, callback));

    public void LogPoints(int points, Action<bool> callback = null) =>
      LootLockerSDKManager.SubmitScore(playerID, points, settings.LeaderboardId, 
        (response) => OnPointsSet(response, callback));
    
    public void FetchTopHighscores(Action<bool,List<LeaderboardUser>> callback) => 
      LootLockerSDKManager.GetScoreList(settings.LeaderboardId, settings.TopCount, (response) => OnLeaderboardList(response, callback));

    private void OnLeaderboardList(LootLockerGetScoreListResponse response,
      Action<bool, List<LeaderboardUser>> callback)
    {
      List<LeaderboardUser> users = new List<LeaderboardUser>();
      if(response.success)
      {
        LootLockerLeaderboardMember[] members = response.items;

        for (int i = 0; i < members.Length; i++)
        {
          users.Add(new LeaderboardUser(members[i].player.name, members[i].score));
        }
      }
      else
      {
#if DEBUG_LOOTLOCKER
        Debug.Log("Failed" + response.Error);
#endif
      }
      
      callback?.Invoke(response.success, users);
    }

    private void OnLogin(Action<bool> callback, LootLockerSessionResponse response)
    {
      if (response.success)
      {
        playerID = response.player_id.ToString();
        
        if (PlayerPrefs.HasKey(GameConstants.PlayerIDKey) == false)
         PlayerPrefs.SetString(GameConstants.PlayerIDKey, playerID);
      }
      else
      {
#if DEBUG_LOOTLOCKER
        Debug.Log("Could not start session");
#endif
      }
      callback?.Invoke(response.success);
    }

    private void OnSetName(PlayerNameResponse response, Action<bool> callback)
    {
#if DEBUG_LOOTLOCKER
      if (response.success)
        Debug.Log("Succesfully set player name");
      else
        Debug.Log("Could not set player name" + response.Error);
#endif
      callback?.Invoke(response.success);
    }

    private void OnPointsSet(LootLockerSubmitScoreResponse response, Action<bool> callback)
    {
      if(response.success)
      {
#if DEBUG_LOOTLOCKER
        Debug.Log("Successfully uploaded score");
#endif
      }
      else
      {
#if DEBUG_LOOTLOCKER
        Debug.Log("Failed" + response.Error);
#endif
      }
      
      callback?.Invoke(response.success);
    }
  }
}