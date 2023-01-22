using System;
using System.Collections.Generic;

namespace Features.Services.Leaderboard
{
  public interface ILeaderboard
  {
    void Login(Action<bool> callback = null);
    void SetNickname(string nickname, Action<bool> callback = null);
    void LogPoints(int points, Action<bool> callback = null);
    void FetchTopHighscores(Action<bool,List<LeaderboardUser>> callback);
  }
}