using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Level.Scripts.LevelStates.Machine;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelCardDeckShuffleState: IState
  {
    private readonly CardDeck deck;
    private readonly ILevelStateMachine levelStateMachine;

    public LevelCardDeckShuffleState(CardDeck deck, ILevelStateMachine levelStateMachine)
    {
      this.deck = deck;
      this.levelStateMachine = levelStateMachine;
    }
    
    public void Enter()
    {
      deck.Shuffle(StartGame);
    }

    public void Exit()
    {
      
    }

    private void StartGame() => 
      levelStateMachine.Enter<LevelFirstCardsState>();
  }
}