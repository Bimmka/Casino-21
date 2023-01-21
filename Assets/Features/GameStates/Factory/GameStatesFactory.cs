using System;
using Features.GameStates.States;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.UI.Windows;

namespace Features.GameStates.Factory
{
  public class GameStatesFactory
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IWindowsService windowsService;

    public GameStatesFactory(ISceneLoader sceneLoader, IWindowsService windowsService)
    {
      this.sceneLoader = sceneLoader;
      this.windowsService = windowsService;
    }
    
    public IState Create<TState>(IGameStateMachine gameStateMachine) where TState : IExitableState
    {
      switch (typeof(TState).Name)
      {
        case nameof(GameEndState):
          return new GameEndState(windowsService);
        case nameof(GameLoadState):
          return new GameLoadState(gameStateMachine, sceneLoader);
        case nameof(GameLoopState):
          return new GameLoopState();
        case nameof(MainMenuState):
          return new MainMenuState(gameStateMachine, sceneLoader, windowsService);
        case nameof(RegistrationState):
          return new RegistrationState(sceneLoader, windowsService);
        case nameof(ProgressLoadState):
          return new ProgressLoadState(gameStateMachine);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}