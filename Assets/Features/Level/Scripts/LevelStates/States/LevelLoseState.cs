using Features.GameStates.States.Interfaces;
using Features.Services.Save;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.User.Data;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelLoseState: IState
  {
    private readonly IWindowsService windowsService;
    private readonly IUserProvider userProvider;
    private readonly ISaveService saveService;

    public LevelLoseState(IWindowsService windowsService, IUserProvider userProvider, ISaveService saveService)
    {
      this.windowsService = windowsService;
      this.userProvider = userProvider;
      this.saveService = saveService;
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.Lose);
      userProvider.User.StatisticsData.ResetStatistic(StatisticsType.WinStreak);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.TotalGames);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.TotalLose);
      userProvider.User.OpenPerksData.RefreshPerksOpen();
      saveService.SavePlayer(userProvider.User);
    }

    public void Exit()
    {
      
    }
  }
}