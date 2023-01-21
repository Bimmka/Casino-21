using Features.Constants;
using Features.User.Data;
using UnityEngine;

namespace Features.Services.Save
{
  public class PrefsSaveService : ISaveService
  {
    public SerializedUser LoadPlayer()
    {
      int points = PlayerPrefs.GetInt(GameConstants.PlayerPointsKey, GameConstants.PlayerDefaultPoints);
      string nickname = PlayerPrefs.GetString(GameConstants.PlayerNickKey, "");
      
      return new SerializedUser()
      {
        Points = points,
        Nickname = nickname
      };
    }

    public void SavePlayer(UserData userData)
    {
      PlayerPrefs.SetInt(GameConstants.PlayerPointsKey, userData.PointsData.CurrentPoints);
      PlayerPrefs.SetString(GameConstants.PlayerNickKey, userData.CommonData.Nickname);
    }
  }
}