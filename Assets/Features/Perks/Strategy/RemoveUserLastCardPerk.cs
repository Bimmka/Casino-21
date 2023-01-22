using Features.Hands.Scripts.User;
using Features.Perks.Data;

namespace Features.Perks.Strategy
{
  public class RemoveUserLastCardPerk : PerkStrategy
  {
    private readonly UserHands userHands;

    public RemoveUserLastCardPerk(PerkSettings settings, UserHands userHands) : base(settings)
    {
      this.userHands = userHands;
    }

    public override bool IsCanBeUsed() => 
      userHands.IsNotEmpty;

    public override void Use() => 
      userHands.RemoveLastCard();
  }
}