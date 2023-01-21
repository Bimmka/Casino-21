using System;
using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.LevelStates.Machine;
using Features.Level.LevelStates.States;
using Features.NPC.Scripts.Base;
using Features.Rules.Data;
using Features.Services.GameSettings;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;

namespace Features.Level.LevelStates.Factory
{
  public class LevelStatesFactory
  {
    private readonly IWindowsService windowsService;
    private readonly CardDeck deck;
    private readonly UserHands userHands;
    private readonly GameRules gameRules;
    private readonly IGameSettings gameSettings;
    private readonly IUserProvider userProvider;
    private readonly DealerMachine dealer;

    public LevelStatesFactory(IWindowsService windowsService, CardDeck deck, UserHands userHands,
      GameRules gameRules, IGameSettings gameSettings, IUserProvider userProvider, DealerMachine dealer)
    {
      this.windowsService = windowsService;
      this.deck = deck;
      this.userHands = userHands;
      this.gameRules = gameRules;
      this.gameSettings = gameSettings;
      this.userProvider = userProvider;
      this.dealer = dealer;
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
          return new LevelDealerCheckState(userHands, dealer, gameRules, levelStateMachine);
        case nameof(LevelDealerTurnState):
          return new LevelDealerTurnState(dealer, levelStateMachine);
        case nameof(LevelFirstCardsState):
          return new LevelFirstCardsState(userHands, dealer, levelStateMachine);
        case nameof(LevelLoseState):
          return new LevelLoseState(userHands, dealer, windowsService);
        case nameof(LevelPrepareState):
          return new LevelPrepareState(levelStateMachine,deck);
        case nameof(LevelUserCheckState):
          return new LevelUserCheckState(userHands, gameRules, levelStateMachine);
        case nameof(LevelUserTurnState):
          return new LevelUserTurnState(windowsService);
        case nameof(LevelWinState):
          return new LevelWinState(userHands, dealer, windowsService, gameSettings, userProvider);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}