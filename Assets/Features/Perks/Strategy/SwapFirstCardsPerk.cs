using Features.Cards.Scripts.Element;
using Features.Hands.Scripts.User;
using Features.NPC.Scripts.Base;
using Features.Perks.Data;

namespace Features.Perks.Strategy
{
  public class SwapFirstCardsPerk : PerkStrategy
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealerMachine;

    public SwapFirstCardsPerk(PerkSettings settings, UserHands userHands, DealerMachine dealerMachine) : base(settings)
    {
      this.userHands = userHands;
      this.dealerMachine = dealerMachine;
    }

    public override bool IsCanBeUsed() => 
      userHands.IsNotEmpty && dealerMachine.IsNotEmpty;

    public override void Use()
    {
      Card userFirstCard = userHands.PopFirstCard();
      Card dealerFirstCard = dealerMachine.PopFirstCard();
      userHands.SetCard(dealerFirstCard);
      dealerMachine.SetCard(userFirstCard);
    }
  }
}