using Features.GameStates.States.Interfaces;
using Features.Services.GameSettings;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelWinState: IState
  {
    private readonly IWindowsService windowsService;
    private readonly IGameSettings gameSettings;
    private readonly IUserProvider userProvider;

    public LevelWinState(IWindowsService windowsService,
      IGameSettings gameSettings, IUserProvider userProvider)
    {
      this.windowsService = windowsService;
      this.gameSettings = gameSettings;
      this.userProvider = userProvider;
    }

    public void Enter()
    {
      windowsService.Open(WindowId.Win);
      userProvider.User.PointsData.Add(gameSettings.CurrentBet * 2);
    }

    public void Exit()
    {
      
    }
  }
}