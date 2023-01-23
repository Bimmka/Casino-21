using System;
using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Services.GameSettings;
using Features.Services.Leaderboard;
using Features.Services.Save;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.User.Data;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelWinState: IState
  {
    private readonly IWindowsService windowsService;
    private readonly IGameSettings gameSettings;
    private readonly IUserProvider userProvider;
    private readonly ISaveService saveService;
    private readonly ILeaderboard leaderboard;
    private readonly UserHands userHands;

    public LevelWinState(IWindowsService windowsService, IGameSettings gameSettings, 
      IUserProvider userProvider, ISaveService saveService, ILeaderboard leaderboard, UserHands userHands)
    {
      this.windowsService = windowsService;
      this.gameSettings = gameSettings;
      this.userProvider = userProvider;
      this.saveService = saveService;
      this.leaderboard = leaderboard;
      this.userHands = userHands;
    }

    public void Enter()
    {
      windowsService.Open(WindowId.Win);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.WinStreak);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.TotalWins);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.TotalGames);
      switch (gameSettings.DifficultType)
      {
        case GameDifficultType.Easy:
          userProvider.User.StatisticsData.IncStatistic(StatisticsType.EasyWin);
          break;
        case GameDifficultType.Medium:
          userProvider.User.StatisticsData.IncStatistic(StatisticsType.MediumWin);
          break;
        case GameDifficultType.Hard:
          userProvider.User.StatisticsData.IncStatistic(StatisticsType.HardWin);
          break;
      }
      userProvider.User.OpenPerksData.RefreshPerksOpen();
      userProvider.User.PointsData.Add(WinPoints());
      saveService.SavePlayer(userProvider.User);
      leaderboard.LogPoints(userProvider.User.PointsData.CurrentPoints);
      userHands.WinAnimation();
    }

    public void Exit()
    {
      
    }

    private int WinPoints() => 
      gameSettings.CurrentBet + (int)(gameSettings.CurrentBet * gameSettings.CurrentCoefficient());
  }
}