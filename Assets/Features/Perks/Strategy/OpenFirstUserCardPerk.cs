using System;
using Features.Hands.Scripts.User;
using Features.Perks.Data;

namespace Features.Perks.Strategy
{
  public class OpenFirstUserCardPerk : PerkStrategy
  {
    private readonly UserHands userHands;

    public OpenFirstUserCardPerk(PerkSettings settings, UserHands userHands) : base(settings)
    {
      this.userHands = userHands;
    }

    public override bool IsCanBeUsed() => 
      userHands.IsNotEmpty;

    public override void Use(Action callback)
    {
      userHands.OpenFirstCard();
      callback?.Invoke();
    }
  }
}