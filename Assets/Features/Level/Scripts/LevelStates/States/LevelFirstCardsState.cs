using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.NPC.Scripts.Base;
using Features.Services.GameSettings;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelFirstCardsState : IState
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealer;
    private readonly ILevelStateMachine levelStateMachine;
    private readonly CardDeck deck;
    private readonly IGameSettings gameSettings;

    public LevelFirstCardsState(UserHands userHands, DealerMachine dealer, ILevelStateMachine levelStateMachine, CardDeck deck,
      IGameSettings gameSettings)
    {
      this.userHands = userHands;
      this.dealer = dealer;
      this.levelStateMachine = levelStateMachine;
      this.deck = deck;
      this.gameSettings = gameSettings;
    }

    public void Enter()
    {
      if (gameSettings.DifficultType != GameDifficultType.Easy)
        deck.HideCardsCost();
      userHands.TakeCard();
      dealer.TakeCard();
      levelStateMachine.Enter<LevelUserTurnState>();
    }

    public void Exit()
    {
      
    }
  }
}