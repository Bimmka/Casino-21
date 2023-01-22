using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.UI.Windows.Actions.Scripts;

namespace Features.Level.Scripts.LevelStates.States
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
      ((UIActionsWindow)windowsService.Window(WindowId.Action)).Hide();
    }
  }
}