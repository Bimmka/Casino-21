using Features.GameStates.States.Interfaces;
using Features.Perks.Observer;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.UI.Windows.Actions.Scripts;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelUserTurnState: IState
  {
    private readonly IWindowsService windowsService;
    private readonly PerksObserver perksObserver;

    public LevelUserTurnState(IWindowsService windowsService, PerksObserver perksObserver)
    {
      this.windowsService = windowsService;
      this.perksObserver = perksObserver;
    }
    
    public void Enter()
    {
      ActionsWindow().Open();
      perksObserver.Reset();
    }

    public void Exit()
    {
      ActionsWindow().LockPerk();
      ActionsWindow().Hide();
    }

    private UIActionsWindow ActionsWindow() => 
      ((UIActionsWindow) windowsService.Window(WindowId.Action));
  }
}