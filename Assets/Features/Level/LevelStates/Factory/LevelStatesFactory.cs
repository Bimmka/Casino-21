using System;
using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.Dealer;
using Features.Hands.Scripts.User;
using Features.Level.LevelStates.Machine;
using Features.Level.LevelStates.States;
using Features.Rules.Data;
using Features.Services.UI.Windows;

namespace Features.Level.LevelStates.Factory
{
  public class LevelStatesFactory
  {
    private readonly IWindowsService windowsService;
    private readonly CardDeck deck;
    private readonly UserHands userHands;
    private readonly DealerHands dealerHands;
    private readonly GameRules gameRules;

    public LevelStatesFactory(IWindowsService windowsService, CardDeck deck, UserHands userHands, DealerHands dealerHands,
      GameRules gameRules)
    {
      this.windowsService = windowsService;
      this.deck = deck;
      this.userHands = userHands;
      this.dealerHands = dealerHands;
      this.gameRules = gameRules;
    }
    
    public IExitableState Create<TState>(ILevelStateMachine levelStateMachine) where TState : IExitableState
    {
      switch (typeof(TState).Name)
      {
        case nameof(LevelBetState):
          return new LevelBetState(windowsService);
        case nameof(LevelCardDeckShuffleState):
          return new LevelCardDeckShuffleState(deck, levelStateMachine);
        case nameof(LevelDealerCheckState):
          return new LevelDealerCheckState();
        case nameof(LevelDealerTurnState):
          return new LevelDealerTurnState();
        case nameof(LevelFirstCardsState):
          return new LevelFirstCardsState(userHands, dealerHands, levelStateMachine);
        case nameof(LevelLoseState):
          return new LevelLoseState();
        case nameof(LevelPrepareState):
          return new LevelPrepareState(levelStateMachine,deck);
        case nameof(LevelUserCheckState):
          return new LevelUserCheckState(userHands, gameRules, levelStateMachine);
        case nameof(LevelUserTurnState):
          return new LevelUserTurnState(windowsService);
        case nameof(LevelWinState):
          return new LevelWinState();
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}