using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.Dealer;
using Features.Hands.Scripts.User;
using Features.Services.GameSettings;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;

namespace Features.Level.LevelStates.States
{
  public class LevelLoseState: IState
  {
    private readonly UserHands userHands;
    private readonly DealerHands dealerHands;
    private readonly IGameSettings gameSettings;
    private readonly IUserProvider userProvider;
    private readonly IWindowsService windowsService;

    public LevelLoseState(UserHands userHands, DealerHands dealerHands, IWindowsService windowsService)
    {
      this.userHands = userHands;
      this.dealerHands = dealerHands;
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      userHands.ReleaseCards();
      dealerHands.ReleaseCards();
      windowsService.Open(WindowId.Lose);
    }

    public void Exit()
    {
      
    }
  }
}