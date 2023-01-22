using UnityEngine;

namespace Features.StaticData.Leaderboard
{
  [CreateAssetMenu(fileName = "LeaderboardSettings", menuName = "StaticData/Leaderboard/Create Leaderboard Settings", order = 52)]
  public class LeaderboardSettings : ScriptableObject
  {
    public string LeaderboardId = "10783";
    public string APIKEY = "dev_18c8c2155f4944a98b154fd386223f39";
  }
}