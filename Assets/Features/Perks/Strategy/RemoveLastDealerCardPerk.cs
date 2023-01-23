using System;
using Features.NPC.Scripts.Base;
using Features.Perks.Data;

namespace Features.Perks.Strategy
{
  public class RemoveLastDealerCardPerk : PerkStrategy
  {
    private readonly DealerMachine dealerMachine;

    public RemoveLastDealerCardPerk(PerkSettings settings, DealerMachine dealerMachine) : base(settings)
    {
      this.dealerMachine = dealerMachine;
    }

    public override bool IsCanBeUsed() => 
      dealerMachine.IsNotEmpty;

    public override void Use(Action callback)
    {
      dealerMachine.RemoveLastCard();
      callback?.Invoke();
    }
  }
}