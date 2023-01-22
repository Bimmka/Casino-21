using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.UI.Windows.Actions.Scripts;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelPrepareState : IState
  {
    private readonly ILevelStateMachine levelStateMachine;
    private readonly CardDeck deck;
    private readonly IWindowsService windowsService;

    public LevelPrepareState(ILevelStateMachine levelStateMachine, CardDeck deck, IWindowsService windowsService)
    {
      this.levelStateMachine = levelStateMachine;
      this.deck = deck;
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.HUD);
      UIActionsWindow window = (UIActionsWindow) windowsService.Open(WindowId.Action);
      window.Hide();
      deck.Create();
      levelStateMachine.Enter<LevelBetState>();
    }

    public void Exit()
    {
      
    }
  }
}