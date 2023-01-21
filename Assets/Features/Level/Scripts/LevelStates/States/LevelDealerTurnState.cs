using Features.GameStates.States.Interfaces;
using Features.Level.Scripts.LevelStates.Machine;
using Features.NPC.Scripts.Base;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelDealerTurnState: IState
  {
    private readonly DealerMachine dealer;
    private readonly ILevelStateMachine levelStateMachine;

    public LevelDealerTurnState(DealerMachine dealer, ILevelStateMachine levelStateMachine)
    {
      this.dealer = dealer;
      this.levelStateMachine = levelStateMachine;
    }
    
    public void Enter()
    {
      dealer.End += OnEndTurn;
      dealer.StartTurn();
    }

    public void Exit()
    {
      dealer.End -= OnEndTurn;
    }

    private void OnEndTurn() => 
      levelStateMachine.Enter<LevelDealerCheckState>();
  }
}