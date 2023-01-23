using Features.GameStates.States.Interfaces;
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

    public LevelWinState(IWindowsService windowsService, IGameSettings gameSettings, 
      IUserProvider userProvider, ISaveService saveService, ILeaderboard leaderboard)
    {
      this.windowsService = windowsService;
      this.gameSettings = gameSettings;
      this.userProvider = userProvider;
      this.saveService = saveService;
      this.leaderboard = leaderboard;
    }

    public void Enter()
    {
      windowsService.Open(WindowId.Win);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.WinStreak);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.TotalWins);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.TotalGames);
      userProvider.User.OpenPerksData.RefreshPerksOpen();
      userProvider.User.PointsData.Add(WinPoints());
      saveService.SavePlayer(userProvider.User);
      leaderboard.LogPoints(userProvider.User.PointsData.CurrentPoints);
    }

    public void Exit()
    {
      
    }

    private int WinPoints() => 
      gameSettings.CurrentBet + (int)(gameSettings.CurrentBet * gameSettings.CurrentCoefficient());
  }
}