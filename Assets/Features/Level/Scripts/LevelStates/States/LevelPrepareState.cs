using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Perks.Observer;
using Features.Services.Audio;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.StaticData.Audio;
using Features.UI.Windows.Actions.Scripts;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelPrepareState : IState
  {
    private readonly ILevelStateMachine levelStateMachine;
    private readonly CardDeck deck;
    private readonly IWindowsService windowsService;
    private readonly IAudioService audioService;
    private readonly PerksObserver perksObserver;

    public LevelPrepareState(ILevelStateMachine levelStateMachine, CardDeck deck, IWindowsService windowsService, IAudioService audioService, 
      PerksObserver perksObserver)
    {
      this.levelStateMachine = levelStateMachine;
      this.deck = deck;
      this.windowsService = windowsService;
      this.audioService = audioService;
      this.perksObserver = perksObserver;
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.HUD);
      deck.Create();
      perksObserver.Initialize(levelStateMachine);
      UIActionsWindow window = (UIActionsWindow) windowsService.Open(WindowId.Action);
      window.Hide();
      levelStateMachine.Enter<LevelBetState>();
      audioService.Play(AudioEventType.GameAmbient);
      audioService.Play(AudioEventType.GameMusic);
    }

    public void Exit()
    {
      
    }
  }
}