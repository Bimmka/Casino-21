using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.Dealer;
using Features.Hands.Scripts.User;
using Features.Services.GameSettings;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;

namespace Features.Level.LevelStates.States
{
  public class LevelWinState: IState
  {
    private readonly UserHands userHands;
    private readonly DealerHands dealerHands;
    private readonly IWindowsService windowsService;
    private readonly IGameSettings gameSettings;
    private readonly IUserProvider userProvider;

    public LevelWinState(UserHands userHands, DealerHands dealerHands, IWindowsService windowsService,
      IGameSettings gameSettings, IUserProvider userProvider)
    {
      this.userHands = userHands;
      this.dealerHands = dealerHands;
      this.windowsService = windowsService;
      this.gameSettings = gameSettings;
      this.userProvider = userProvider;
    }

    public void Enter()
    {
      userHands.ReleaseCards();
      dealerHands.ReleaseCards();
      windowsService.Open(WindowId.Win);
      userProvider.User.PointsData.Add(gameSettings.CurrentBet * 2);
    }

    public void Exit()
    {
      
    }
  }
}