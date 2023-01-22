using System;
using Features.GameStates.States;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.Save;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Services.Audio;

namespace Features.GameStates.Factory
{
  public class GameStatesFactory
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IWindowsService windowsService;
    private readonly ISaveService saveService;
    private readonly IUserProvider userProvider;
    private readonly IAudioService audioService;

    public GameStatesFactory(ISceneLoader sceneLoader, IWindowsService windowsService, ISaveService saveService, IUserProvider userProvider,
      IAudioService audioService)
    {
      this.sceneLoader = sceneLoader;
      this.windowsService = windowsService;
      this.saveService = saveService;
      this.userProvider = userProvider;
      this.audioService = audioService;
    }
    
    public IExitableState Create<TState>(IGameStateMachine gameStateMachine) where TState : IExitableState
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
          return new MainMenuState(sceneLoader, windowsService);
        case nameof(RegistrationState):
          return new RegistrationState(sceneLoader, windowsService);
        case nameof(LoadProgressState):
          return new LoadProgressState(gameStateMachine, saveService, userProvider, audioService);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}