using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
  public class GameEndState : IState
  {
    private readonly IWindowsService windowsService;

    public GameEndState(IWindowsService windowsService, IGameStateMachine gameStateMachine)
    {
      this.windowsService = windowsService;
      gameStateMachine.Register(this);
    }
    
    public void Enter() => 
      windowsService.Open(WindowId.LevelEnd);

    public void Exit() { }
  }
}