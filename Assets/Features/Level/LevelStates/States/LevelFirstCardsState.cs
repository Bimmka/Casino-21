using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.Dealer;
using Features.Hands.Scripts.User;
using Features.Level.LevelStates.Machine;

namespace Features.Level.LevelStates.States
{
  public class LevelFirstCardsState : IState
  {
    private readonly UserHands userHands;
    private readonly DealerHands dealerHands;
    private readonly ILevelStateMachine levelStateMachine;

    public LevelFirstCardsState(UserHands userHands, DealerHands dealerHands, ILevelStateMachine levelStateMachine)
    {
      this.userHands = userHands;
      this.dealerHands = dealerHands;
      this.levelStateMachine = levelStateMachine;
    }

    public void Enter()
    {
      userHands.TakeCard();
      dealerHands.TakeCard();
      levelStateMachine.Enter<LevelUserTurnState>();
    }

    public void Exit()
    {
      
    }
  }
}