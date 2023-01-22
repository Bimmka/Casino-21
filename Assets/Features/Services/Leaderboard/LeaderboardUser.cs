namespace Features.Services.Leaderboard
{
  public struct LeaderboardUser
  {
    public string Name;
    public int Points;

    public LeaderboardUser(string name, int points)
    {
      Name = name;
      Points = points;
    }
  }
}