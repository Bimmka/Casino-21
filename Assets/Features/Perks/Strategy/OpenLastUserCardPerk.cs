using Features.Hands.Scripts.User;
using Features.Perks.Data;

namespace Features.Perks.Strategy
{
  public class OpenLastUserCardPerk : PerkStrategy
  {
    private readonly UserHands userHands;

    public OpenLastUserCardPerk(PerkSettings settings, UserHands userHands) : base(settings)
    {
      this.userHands = userHands;
    }

    public override bool IsCanBeUsed() => 
      userHands.IsNotEmpty;

    public override void Use() => 
      userHands.OpenLastCard();
  }
}