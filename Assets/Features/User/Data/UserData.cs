using Features.Perks.Data;
using Features.Services.Save;

namespace Features.User.Data
{
  public class UserData
  {
    public readonly UserCommonData CommonData;
    public readonly UserPointsData PointsData;
    public readonly UserStatistics StatisticsData;
    public readonly UserOpenPerks OpenPerksData;

    public UserData(PerksSettingsContainer perksContainer)
    {
      CommonData = new UserCommonData();
      PointsData = new UserPointsData();
      StatisticsData = new UserStatistics();
      OpenPerksData = new UserOpenPerks(perksContainer, StatisticsData);
    }

    public void Initialize(string nickname, int points)
    {
      CommonData.Initialize(nickname);
      PointsData.Initialize(points);
      OpenPerksData.RefreshPerksOpen();
    }

    public void Restore(SerializedUser storeData)
    {
      CommonData.Initialize(storeData.Nickname);
      PointsData.Initialize(storeData.Points);
      OpenPerksData.Restore(storeData.Perks);
      StatisticsData.Restore(storeData.Statistics);
    }
  }
}