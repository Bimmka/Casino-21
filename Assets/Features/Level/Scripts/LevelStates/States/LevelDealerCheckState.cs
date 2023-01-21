using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.Info;
using Features.Level.Scripts.LevelStates.Machine;
using Features.NPC.Scripts.Base;
using Features.Rules.Data;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelDealerCheckState: IState
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealer;
    private readonly GameRules rules;
    private readonly ILevelStateMachine levelStateMachine;
    private readonly LevelInfoDisplayer levelInfoDisplayer;

    public LevelDealerCheckState(UserHands userHands, DealerMachine dealer, GameRules rules,
      ILevelStateMachine levelStateMachine, LevelInfoDisplayer levelInfoDisplayer)
    {
      this.userHands = userHands;
      this.dealer = dealer;
      this.rules = rules;
      this.levelStateMachine = levelStateMachine;
      this.levelInfoDisplayer = levelInfoDisplayer;
    }
    
    public void Enter()
    {
      int dealerPoints = dealer.Points;
      levelInfoDisplayer.DisplayDealerPoints(dealerPoints);
      if (IsDealerWin(dealerPoints))
        UserLose();
      else
        UserWin();
    }

    public void Exit()
    {
      
    }

    private void UserLose() => 
      levelStateMachine.Enter<LevelLoseState>();

    private void UserWin() => 
      levelStateMachine.Enter<LevelWinState>();

    private bool IsDealerWin(int dealerPoints) =>
      IsDealerTakeMaxPoints(dealerPoints) || 
      (IsDealerWentOver(dealerPoints) == false && IsDealerTakeMorePoints(dealerPoints, userHands.CardPoints()));

    private bool IsDealerTakeMorePoints(int dealerPoints, int userPoints) => 
      (dealerPoints > userPoints) || (dealerPoints == userPoints && rules.IsUserWinInDraw == false);

    private bool IsDealerWentOver(in int dealerPoints) => 
      dealerPoints > rules.MaxPoints;

    private bool IsDealerTakeMaxPoints(int dealerPoints) => 
      dealerPoints == rules.MaxPoints;
  }
}