using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.Level.LevelStates.States
{
  public class LevelBetState : IState
  {
    private readonly IWindowsService windowsService;

    public LevelBetState(IWindowsService windowsService)
    {
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.Bet);
    }

    public void Exit()
    {
      
    }
  }
}