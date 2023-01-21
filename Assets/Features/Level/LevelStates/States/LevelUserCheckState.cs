using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.Info;
using Features.Level.LevelStates.Machine;
using Features.Rules.Data;

namespace Features.Level.LevelStates.States
{
  public class LevelUserCheckState: IState
  {
    private readonly UserHands userHands;
    private readonly GameRules rules;
    private readonly ILevelStateMachine levelStateMachine;
    private readonly LevelInfoDisplayer levelInfoDisplayer;

    public LevelUserCheckState(UserHands userHands, GameRules rules, ILevelStateMachine levelStateMachine,
      LevelInfoDisplayer levelInfoDisplayer)
    {
      this.userHands = userHands;
      this.rules = rules;
      this.levelStateMachine = levelStateMachine;
      this.levelInfoDisplayer = levelInfoDisplayer;
    }
    
    public void Enter()
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

    public void Exit()
    {
      
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