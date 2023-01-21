using Features.User.Data;

namespace Features.Services.UserProvider
{
  public interface IUserProvider
  {
    UserData User { get; }
    void Initialize(UserData userData);
  }
}