namespace Features.User.Data
{
  public class UserCommonData
  {
    public string Nickname { get; private set; }

    public void Initialize(string nickname)
    {
      Nickname = nickname;
    }
  }
}