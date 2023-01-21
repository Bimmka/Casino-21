using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.Info;
using Features.Level.Scripts.LevelStates.Machine;
using Features.NPC.Scripts.Base;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelResetState : IState
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealer;
    private readonly CardDeck deck;
    private readonly ILevelStateMachine levelStateMachine;
    private readonly LevelInfoDisplayer levelInfoDisplayer;

    public LevelResetState(UserHands userHands, DealerMachine dealer, CardDeck deck, 
      ILevelStateMachine levelStateMachine, LevelInfoDisplayer levelInfoDisplayer)
    {
      this.userHands = userHands;
      this.dealer = dealer;
      this.deck = deck;
      this.levelStateMachine = levelStateMachine;
      this.levelInfoDisplayer = levelInfoDisplayer;
    }
    
    public void Enter()
    {
      userHands.ReleaseCards();
      dealer.ReleaseCards();
      deck.Reset(SetBetState);
      levelInfoDisplayer.Reset();
    }

    public void Exit()
    {
      
    }

    private void SetBetState() => 
      levelStateMachine.Enter<LevelBetState>();
  }
}