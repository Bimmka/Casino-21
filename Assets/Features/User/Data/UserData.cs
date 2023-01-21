using Features.Services.Save;

namespace Features.User.Data
{
  public class UserData
  {
    public UserCommonData CommonData;
    public UserPointsData PointsData;

    public UserData()
    {
      CommonData = new UserCommonData();
      PointsData = new UserPointsData();
    }

    public void Initialize(string nickname, int points)
    {
      CommonData.Initialize(nickname);
      PointsData.Initialize(points);
    }

    public void Restore(SerializedUser storeData)
    {
      CommonData.Initialize(storeData.Nickname);
      PointsData.Initialize(storeData.Points);
    }
  }
}