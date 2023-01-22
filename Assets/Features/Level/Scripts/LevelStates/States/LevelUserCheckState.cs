using System;
using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.Info;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Rules.Data;
using Features.Services.GameSettings;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelUserCheckState: IState
  {
    private readonly UserHands userHands;
    private readonly GameRules rules;
    private readonly ILevelStateMachine levelStateMachine;
    private readonly LevelInfoDisplayer levelInfoDisplayer;
    private readonly IGameSettings gameSettings;

    public LevelUserCheckState(UserHands userHands, GameRules rules, ILevelStateMachine levelStateMachine,
      LevelInfoDisplayer levelInfoDisplayer, IGameSettings gameSettings)
    {
      this.userHands = userHands;
      this.rules = rules;
      this.levelStateMachine = levelStateMachine;
      this.levelInfoDisplayer = levelInfoDisplayer;
      this.gameSettings = gameSettings;
    }
    
    public void Enter()
    {
      if (gameSettings.DifficultType != GameDifficultType.Easy)
        DisplayCards(Check);
      else
        Check();
    }

    public void Exit()
    {
      
    }

    private void DisplayCards(Action callback) => 
      userHands.DisplayCardsCost(callback);

    private void Check()
    {
      int userPoints = userHands.CardPoints();
      levelInfoDisplayer.DisplayUserPoints(userPoints);
      if (IsUserWentOver(userPoints))
        Lose();
      else if (IsUserTakeMaxPoints(userPoints))
        Win();
      else
        DealerTurn();
    }

    private void Lose() => 
      levelStateMachine.Enter<LevelLoseState>();

    private void Win() => 
      levelStateMachine.Enter<LevelWinState>();

    private void DealerTurn() => 
      levelStateMachine.Enter<LevelDealerTurnState>();

    private bool IsUserWentOver(int userPoints) => 
      userPoints > rules.MaxPoints;

    private bool IsUserTakeMaxPoints(int userPoints) => 
      userPoints == rules.MaxPoints;
  }
}