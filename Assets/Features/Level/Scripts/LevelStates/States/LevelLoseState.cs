using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
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
    private readonly UserHands userHands;

    public LevelLoseState(IWindowsService windowsService, IUserProvider userProvider, ISaveService saveService,
      UserHands userHands)
    {
      this.windowsService = windowsService;
      this.userProvider = userProvider;
      this.saveService = saveService;
      this.userHands = userHands;
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.Lose);
      userProvider.User.StatisticsData.ResetStatistic(StatisticsType.WinStreak);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.TotalGames);
      userProvider.User.StatisticsData.IncStatistic(StatisticsType.TotalLose);
      userProvider.User.OpenPerksData.RefreshPerksOpen();
      saveService.SavePlayer(userProvider.User);
      userHands.LoseAnimation();
    }

    public void Exit()
    {
      
    }
  }
}