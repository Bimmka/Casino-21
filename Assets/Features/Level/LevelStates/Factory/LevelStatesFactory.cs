using System;
using Features.GameStates.States.Interfaces;
using Features.Level.LevelStates.Machine;
using Features.Level.LevelStates.States;
using Features.Services.UI.Windows;

namespace Features.Level.LevelStates.Factory
{
  public class LevelStatesFactory
  {
    private readonly IWindowsService windowsService;

    public LevelStatesFactory(IWindowsService windowsService)
    {
      this.windowsService = windowsService;
    }
    
    public IExitableState Create<TState>(ILevelStateMachine levelStateMachine) where TState : IExitableState
    {
      switch (typeof(TState).Name)
      {
        case nameof(LevelBetState):
          return new LevelBetState(windowsService);
        case nameof(LevelCardDeckShuffleState):
          return new LevelCardDeckShuffleState();
        case nameof(LevelDealerCheckState):
          return new LevelDealerCheckState();
        case nameof(LevelDealerTurnState):
          return new LevelDealerTurnState();
        case nameof(LevelLoseState):
          return new LevelLoseState();
        case nameof(LevelUserCheckState):
          return new LevelUserCheckState();
        case nameof(LevelUserTurnState):
          return new LevelUserTurnState();
        case nameof(LevelWinState):
          return new LevelWinState();
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}