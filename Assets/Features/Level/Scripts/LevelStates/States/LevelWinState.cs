using Features.GameStates.States.Interfaces;
using Features.Services.GameSettings;
using Features.Services.Save;
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
    private readonly ISaveService saveService;

    public LevelWinState(IWindowsService windowsService, IGameSettings gameSettings, 
      IUserProvider userProvider, ISaveService saveService)
    {
      this.windowsService = windowsService;
      this.gameSettings = gameSettings;
      this.userProvider = userProvider;
      this.saveService = saveService;
    }

    public void Enter()
    {
      windowsService.Open(WindowId.Win);
      userProvider.User.PointsData.Add((int)(gameSettings.CurrentBet * gameSettings.CurrentCoefficient()));
      saveService.SavePlayer(userProvider.User);
    }

    public void Exit()
    {
      
    }
  }
}