using Features.User.Data;

namespace Features.Services.Save
{
  public interface ISaveService
  {
    SerializedUser LoadPlayer();
    void SavePlayer(UserData userData);
  }
}