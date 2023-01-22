using System;

namespace Features.Services.Leaderboard
{
  public interface ILeaderboard
  {
    void Login(Action<bool> callback);
    void SetNickname(string nickname, Action<bool> callback);
    void LogPoints(int points, Action<bool> callback);
  }
}