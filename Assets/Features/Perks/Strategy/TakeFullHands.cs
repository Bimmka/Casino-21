using System.Collections;
using Features.CustomCoroutine;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.NPC.Scripts.Base;
using Features.Perks.Data;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.UI.Windows.Actions.Scripts;

namespace Features.Perks.Strategy
{
  public class TakeFullHands : PerkStrategy
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealerMachine;
    private readonly ICoroutineRunner coroutineRunner;
    private readonly IWindowsService windowsService;
    private readonly ILevelStateMachine levelStateMachine;

    public TakeFullHands(PerkSettings settings, UserHands userHands, DealerMachine dealerMachine,
      ICoroutineRunner coroutineRunner, IWindowsService windowsService, ILevelStateMachine levelStateMachine) : base(settings)
    {
      this.userHands = userHands;
      this.dealerMachine = dealerMachine;
      this.coroutineRunner = coroutineRunner;
      this.windowsService = windowsService;
      this.levelStateMachine = levelStateMachine;
    }

    public override bool IsCanBeUsed() => 
      userHands.IsNotEmpty && dealerMachine.IsNotEmpty;

    public override void Use()
    {
      coroutineRunner.StartCoroutine(TakeCards());
    }

    private IEnumerator TakeCards()
    {
      ActionsWindow().Hide();
      while (userHands.IsFull == false && dealerMachine.IsFull == false)
      {
        if (userHands.IsFull == false)
          userHands.TakeCard();
        
        if (dealerMachine.IsFull == false)
          dealerMachine.TakeCard();

        while (userHands.IsTakingCard || dealerMachine.IsTakingCard)
        {
          yield return null;
        }
      }
      levelStateMachine.Enter<LevelPerkCheckState>();
    }
    
    private UIActionsWindow ActionsWindow() => 
      ((UIActionsWindow) windowsService.Window(WindowId.Action));
  }
}