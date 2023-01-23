using System;
using Features.Cards.Scripts.Deck;
using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.Info;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.NPC.Scripts.Base;
using Features.Perks.Observer;
using Features.Rules.Data;
using Features.Services.GameSettings;
using Features.Services.Leaderboard;
using Features.Services.Save;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Services.Audio;

namespace Features.Level.Scripts.LevelStates.Factory
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
    private readonly LevelInfoDisplayer infoDisplayer;
    private readonly ISaveService saveService;
    private readonly ILeaderboard leaderboard;
    private readonly IAudioService audioService;
    private readonly PerksObserver perksObserver;

    public LevelStatesFactory(IWindowsService windowsService, CardDeck deck, UserHands userHands,
      GameRules gameRules, IGameSettings gameSettings, IUserProvider userProvider, DealerMachine dealer,
      LevelInfoDisplayer infoDisplayer, ISaveService saveService, ILeaderboard leaderboard, IAudioService audioService,
      PerksObserver perksObserver)
    {
      this.windowsService = windowsService;
      this.deck = deck;
      this.userHands = userHands;
      this.gameRules = gameRules;
      this.gameSettings = gameSettings;
      this.userProvider = userProvider;
      this.dealer = dealer;
      this.infoDisplayer = infoDisplayer;
      this.saveService = saveService;
      this.leaderboard = leaderboard;
      this.audioService = audioService;
      this.perksObserver = perksObserver;
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
          return new LevelDealerCheckState(userHands, dealer, gameRules, levelStateMachine, infoDisplayer, gameSettings);
        case nameof(LevelDealerTurnState):
          return new LevelDealerTurnState(dealer, levelStateMachine);
        case nameof(LevelFirstCardsState):
          return new LevelFirstCardsState(userHands, dealer, levelStateMachine, deck, gameSettings);
        case nameof(LevelLoseState):
          return new LevelLoseState(windowsService, userProvider, saveService, userHands);
        case nameof(LevelPerkCheckState):
          return new LevelPerkCheckState(userHands, levelStateMachine, dealer, gameSettings, infoDisplayer, gameRules);
        case nameof(LevelPrepareState):
          return new LevelPrepareState(levelStateMachine,deck, windowsService, audioService, perksObserver);
        case nameof(LevelResetState):
          return new LevelResetState(userHands, dealer, deck, levelStateMachine, infoDisplayer);
        case nameof(LevelUsePerkOnDealerState):
          return new LevelUsePerkOnDealerState(perksObserver, levelStateMachine);
        case nameof(LevelUserCheckState):
          return new LevelUserCheckState(userHands, gameRules, levelStateMachine, infoDisplayer, gameSettings);
        case nameof(LevelUserTurnState):
          return new LevelUserTurnState(windowsService, perksObserver);
        case nameof(LevelWinState):
          return new LevelWinState(windowsService, gameSettings, userProvider, saveService, leaderboard, userHands);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}