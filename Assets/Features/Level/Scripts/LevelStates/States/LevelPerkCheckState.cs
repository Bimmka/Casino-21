using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.Info;
using Features.Level.Scripts.LevelStates.Machine;
using Features.NPC.Scripts.Base;
using Features.Rules.Data;
using Features.Services.GameSettings;

namespace Features.Level.Scripts.LevelStates.States
{
  public class LevelPerkCheckState : IState
  {
    private readonly ILevelStateMachine levelStateMachine;
    private readonly UserHands userHands;
    private readonly DealerMachine dealerMachine;
    private readonly IGameSettings gameSettings;
    private readonly LevelInfoDisplayer levelInfoDisplayer;
    private readonly GameRules gameRules;

    private bool isDealerOpened;
    private bool isUserOpened;

    public LevelPerkCheckState(UserHands userHands, ILevelStateMachine levelStateMachine, DealerMachine dealerMachine,
      IGameSettings gameSettings, LevelInfoDisplayer levelInfoDisplayer, GameRules gameRules)
    {
      this.levelStateMachine = levelStateMachine;
      this.userHands = userHands;
      this.dealerMachine = dealerMachine;
      this.gameSettings = gameSettings;
      this.levelInfoDisplayer = levelInfoDisplayer;
      this.gameRules = gameRules;
    }

    public void Enter()
    {
      if (gameSettings.DifficultType != GameDifficultType.Easy)
        DisplayCards();
      else
        Check();
    }

    public void Exit()
    {
      
    }

    private void DisplayCards()
    {
      isDealerOpened = false;
      isUserOpened = false;
      userHands.DisplayCardsCost(OnUserOpened);
      dealerMachine.DisplayCardsCost(OnDealerOpened);
    }

    private void Check()
    {
      int userPoints = userHands.CardPoints();
      int dealerPoints = dealerMachine.Points;
      
      levelInfoDisplayer.DisplayUserPoints(userPoints);
      levelInfoDisplayer.DisplayDealerPoints(dealerPoints);
      
      if (IsUserWin(userPoints, dealerPoints))
        levelStateMachine.Enter<LevelWinState>();
      else
        levelStateMachine.Enter<LevelLoseState>();
    }

    private void OnUserOpened()
    {
      isUserOpened = true;
      
      if (isDealerOpened && isUserOpened)
        Check();
    }

    private void OnDealerOpened()
    {
      isDealerOpened = true;
      
      if (isDealerOpened && isUserOpened)
        Check();
    }

    private bool IsUserWin(int userPoints, int dealerPoints) => 
      userPoints < dealerPoints || (userPoints == dealerPoints && gameRules.IsUserWinInDraw);
  }
}