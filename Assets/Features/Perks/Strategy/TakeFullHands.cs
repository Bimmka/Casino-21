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

    public TakeFullHands(PerkSettings settings, UserHands userHands, DealerMachine dealerMachine) : base(settings)
    {
      this.userHands = userHands;
      this.dealerMachine = dealerMachine;
    }

    public override bool IsCanBeUsed() => 
      userHands.IsNotEmpty && dealerMachine.IsNotEmpty;

    public override void Use()
    {
      
    }
  }
}