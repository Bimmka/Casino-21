using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.LevelStates.Machine;
using Features.NPC.Scripts.Base;

namespace Features.Level.LevelStates.States
{
  public class LevelFirstCardsState : IState
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealer;
    private readonly ILevelStateMachine levelStateMachine;

    public LevelFirstCardsState(UserHands userHands, DealerMachine dealer, ILevelStateMachine levelStateMachine)
    {
      this.userHands = userHands;
      this.dealer = dealer;
      this.levelStateMachine = levelStateMachine;
    }

    public void Enter()
    {
      userHands.TakeCard();
      dealer.TakeCard();
      levelStateMachine.Enter<LevelUserTurnState>();
    }

    public void Exit()
    {
      
    }
  }
}