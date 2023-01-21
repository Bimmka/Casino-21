using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    
    public GameLoadState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
    }

    public void Enter() => 
      sceneLoader.Load(GameConstants.GameSceneName, OnLoad);

    public void Exit() { }

    private void OnLoad() => 
      gameStateMachine.Enter<GameLoopState>();
  }
}