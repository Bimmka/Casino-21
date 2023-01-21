using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Zenject;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;

    [Inject]
    public GameLoadState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      gameStateMachine.Register(this);
    }

    public void Enter() => 
      sceneLoader.Load(GameConstants.GameSceneName, OnLoad);

    public void Exit() { }

    private void OnLoad() => 
      gameStateMachine.Enter<GameLoopState>();
  }
}