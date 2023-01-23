using System.Collections.Generic;
using Features.Constants;
using Features.Perks.Data;
using Features.User.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Features.Services.Save
{
  public class PrefsSaveService : ISaveService
  {
    public SerializedUser LoadPlayer()
    {
      int points = PlayerPrefs.GetInt(GameConstants.PlayerPointsKey, GameConstants.PlayerDefaultPoints);
      string nickname = PlayerPrefs.GetString(GameConstants.PlayerNickKey, "");
      
      string perks = PlayerPrefs.GetString(GameConstants.PlayerPerksKey, "");
      List<PerkType> openPerks = string.IsNullOrEmpty(perks) ? new List<PerkType>() : JsonConvert.DeserializeObject<List<PerkType>>(perks);

      string statistics = PlayerPrefs.GetString(GameConstants.PlayerStatisticsKey, "");
      List<int> openStatistics = string.IsNullOrEmpty(perks) ? new List<int>() : JsonConvert.DeserializeObject<List<int>>(statistics);
      return new SerializedUser()
      {
        Points = points,
        Nickname = nickname,
        Perks = openPerks,
        Statistics = openStatistics
      };
    }

    public void SavePlayer(UserData userData)
    {
      PlayerPrefs.SetInt(GameConstants.PlayerPointsKey, userData.PointsData.CurrentPoints);
      PlayerPrefs.SetString(GameConstants.PlayerNickKey, userData.CommonData.Nickname);

      List<PerkType> perks = userData.OpenPerksData.OpenPerks();
      PlayerPrefs.SetString(GameConstants.PlayerPerksKey, JsonConvert.SerializeObject(perks));

      List<int> statistics = userData.StatisticsData.TotalStatistics();
      PlayerPrefs.SetString(GameConstants.PlayerStatisticsKey, JsonConvert.SerializeObject(statistics));
    }
  }
}