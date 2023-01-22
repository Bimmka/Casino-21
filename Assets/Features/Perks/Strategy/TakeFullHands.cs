using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.NPC.Scripts.Base;
using Features.Perks.Data;
using Features.Rules.Data;

namespace Features.Perks.Strategy
{
  public class TakeFullHands : PerkStrategy
  {
    public TakeFullHands(PerkSettings settings, UserHands userHands, DealerMachine dealerMachine,
      ILevelStateMachine levelStateMachine, GameRules gameRules) : base(settings)
    {
    }

    public override void Use()
    {
      
    }
  }
}