using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.NPC.Scripts.Base;
using Features.Services.GameSettings;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;

namespace Features.Level.LevelStates.States
{
  public class LevelLoseState: IState
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealer;
    private readonly IGameSettings gameSettings;
    private readonly IUserProvider userProvider;
    private readonly IWindowsService windowsService;

    public LevelLoseState(UserHands userHands, DealerMachine dealer, IWindowsService windowsService)
    {
      this.userHands = userHands;
      this.dealer = dealer;
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      userHands.ReleaseCards();
      dealer.ReleaseCards();
      windowsService.Open(WindowId.Lose);
    }

    public void Exit()
    {
      
    }
  }
}