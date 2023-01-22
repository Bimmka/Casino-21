using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.NPC.Scripts.Base;
using Features.Perks.Data;
using Features.Rules.Data;

namespace Features.Perks.Strategy
{
  public class TakeFullHands : PerkStrategy
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealerMachine;
    private readonly ILevelStateMachine levelStateMachine;
    private readonly GameRules gameRules;

    public TakeFullHands(PerkSettings settings, UserHands userHands, DealerMachine dealerMachine,
      ILevelStateMachine levelStateMachine, GameRules gameRules) : base(settings)
    {
      this.userHands = userHands;
      this.dealerMachine = dealerMachine;
      this.levelStateMachine = levelStateMachine;
      this.gameRules = gameRules;
    }

    public override bool IsCanBeUsed() => 
      userHands.IsNotEmpty && dealerMachine.IsNotEmpty;

    public override void Use()
    {
      
    }
  }
}