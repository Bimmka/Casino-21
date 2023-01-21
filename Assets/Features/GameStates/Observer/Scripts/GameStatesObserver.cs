using Features.GameStates.States;
using UnityEngine;
using Zenject;

namespace Features.GameStates.Observer.Scripts
{
  public class GameStatesObserver : MonoBehaviour
  {
    private IGameStateMachine gameStateMachine;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void StartGame() => 
      gameStateMachine.Enter<MainMenuState>();
  }
}