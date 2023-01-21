using Features.User.Data;

namespace Features.Services.UserProvider
{
  public class UserProviderService : IUserProvider
  {
    public UserData User { get; private set; }

    public void Initialize(UserData userData)
    {
      User = userData;
    }
  }
}