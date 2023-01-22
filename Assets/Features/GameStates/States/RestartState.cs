using Features.Constants;
using Features.GameStates.States.Interfaces;
using UnityEngine;

namespace Features.GameStates.States
{
  public class RestartState : IState
  {
    private readonly IGameStateMachine gameStateMachine;

    public RestartState(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
      PlayerPrefs.DeleteKey(GameConstants.PlayerNickKey);
      PlayerPrefs.DeleteKey(GameConstants.PlayerPointsKey);
      PlayerPrefs.DeleteKey(GameConstants.PlayerIDKey);
    }

    public void Exit()
    {
      
    }
  }
}