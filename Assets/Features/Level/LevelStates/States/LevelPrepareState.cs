using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Level.LevelStates.Machine;

namespace Features.Level.LevelStates.States
{
  public class LevelPrepareState : IState
  {
    private readonly ILevelStateMachine levelStateMachine;
    private readonly CardDeck deck;

    public LevelPrepareState(ILevelStateMachine levelStateMachine, CardDeck deck)
    {
      this.levelStateMachine = levelStateMachine;
      this.deck = deck;
    }
    
    public void Enter()
    {
      deck.Create();
      levelStateMachine.Enter<LevelBetState>();
    }

    public void Exit()
    {
      
    }
  }
}