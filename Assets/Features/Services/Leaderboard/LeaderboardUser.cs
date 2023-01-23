namespace Features.Services.Leaderboard
{
  public struct LeaderboardUser
  {
    public string Name;
    public int Points;
    public int Position;

    public LeaderboardUser(string name, int points, int position)
    {
      Name = name;
      Points = points;
      Position = position;
    }
  }
}