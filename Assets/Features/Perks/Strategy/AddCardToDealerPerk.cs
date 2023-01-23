using System;
using Features.NPC.Scripts.Base;
using Features.Perks.Data;

namespace Features.Perks.Strategy
{
  public class AddCardToDealerPerk : PerkStrategy
  {
    private readonly DealerMachine dealerMachine;

    public AddCardToDealerPerk(PerkSettings settings, DealerMachine dealerMachine) : base(settings)
    {
      this.dealerMachine = dealerMachine;
    }

    public override bool IsCanBeUsed() => 
      dealerMachine.IsFull == false;

    public override void Use(Action callback)
    {
      if (dealerMachine.IsFull == false)
        dealerMachine.TakeCard(() => OnTookCard(callback));
      else
        callback?.Invoke();
    }

    private void OnTookCard(Action callback)
    {
      callback?.Invoke();
    }
  }
}