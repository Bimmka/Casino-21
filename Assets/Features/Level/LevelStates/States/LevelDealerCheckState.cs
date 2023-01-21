using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.Dealer;
using Features.Hands.Scripts.User;
using Features.Level.LevelStates.Machine;
using Features.NPC.Scripts.Base;
using Features.Rules.Data;

namespace Features.Level.LevelStates.States
{
  public class LevelDealerCheckState: IState
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealer;
    private readonly GameRules rules;
    private readonly ILevelStateMachine levelStateMachine;

    public LevelDealerCheckState(UserHands userHands, DealerMachine dealer, GameRules rules, 
      ILevelStateMachine levelStateMachine)
    {
      this.userHands = userHands;
      this.dealer = dealer;
      this.rules = rules;
      this.levelStateMachine = levelStateMachine;
    }
    
    public void Enter()
    {
      int dealerPoints = dealer.Points;
      if (IsDealerTakeMaxPoints(dealerPoints) || IsDealerTakeMorePoints(dealerPoints, userHands.CardPoints()))
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

    private bool IsDealerTakeMorePoints(int dealerPoints, int userPoints) => 
      (dealerPoints > userPoints) || (dealerPoints == userPoints && rules.IsUserWinInDraw == false);

    private bool IsDealerTakeMaxPoints(int dealerPoints) => 
      dealerPoints == rules.MaxPoints;
  }
}