using Features.Constants;
using Features.GameStates.States.Interfaces;
using UnityEngine;

namespace Features.GameStates.States
{
  public class ProgressLoadState : IState
  {
    private readonly IGameStateMachine gameStateMachine;

    public ProgressLoadState(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }
    
    public void Enter()
    {
      if (IsHaveSavedPlayer())
      {
        LoadPlayerPoints();
        LoadPlayerNickname();
        SetMainMenuState();
      }
      else
        SetRegistrationState();
    }

    public void Exit()
    {
      
    }

    private void LoadPlayerPoints()
    {
      int points = PlayerPrefs.GetInt(GameConstants.PlayerPointsKey);
    }

    private void LoadPlayerNickname()
    {
      string nickname = PlayerPrefs.GetString(GameConstants.PlayerNickKey);
    }

    private void SetMainMenuState() => 
      gameStateMachine.Enter<MainMenuState>();

    private void SetRegistrationState() => 
      gameStateMachine.Enter<RegistrationState>();

    private bool IsHaveSavedPlayer() => 
      PlayerPrefs.HasKey(GameConstants.PlayerNickKey) && PlayerPrefs.HasKey(GameConstants.PlayerPointsKey);
  }
}