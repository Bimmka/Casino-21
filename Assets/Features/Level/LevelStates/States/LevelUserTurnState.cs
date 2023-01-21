using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.Level.LevelStates.States
{
  public class LevelUserTurnState: IState
  {
    private readonly IWindowsService windowsService;

    public LevelUserTurnState(IWindowsService windowsService)
    {
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.Action);
    }

    public void Exit()
    {
      windowsService.Close(WindowId.Action);
    }
  }
}