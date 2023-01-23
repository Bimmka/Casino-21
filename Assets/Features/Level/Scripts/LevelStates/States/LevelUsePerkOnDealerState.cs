using Features.GameStates.States.Interfaces;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Perks.Data;
using Features.Perks.Observer;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelUsePerkOnDealerState : IState
  {
    private readonly PerksObserver perksObserver;
    private readonly ILevelStateMachine levelStateMachine;

    public LevelUsePerkOnDealerState(PerksObserver perksObserver, ILevelStateMachine levelStateMachine)
    {
      this.perksObserver = perksObserver;
      this.levelStateMachine = levelStateMachine;
    }
    
    public void Enter()
    {
      if (perksObserver.IsUsed && perksObserver.Target == PerkTargetType.Dealer)
        perksObserver.Apply(OnApplied);
      else
        OnApplied();
    }

    public void Exit()
    {
      
    }

    private void OnApplied() => 
      levelStateMachine.Enter<LevelDealerCheckState>();
  }
}