using System;
using Features.Hands.Scripts.User;
using Features.NPC.Scripts.Base;
using Features.Perks.Data;
using Features.Perks.Strategy;
using Features.Services.StaticData;

namespace Features.Perks.Factory
{
  public class PerkStrategyFactory
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealerMachine;
    private readonly IStaticDataService staticDataService;

    public PerkStrategyFactory(UserHands userHands, DealerMachine dealerMachine,
      IStaticDataService staticDataService)
    {
      this.userHands = userHands;
      this.dealerMachine = dealerMachine;
      this.staticDataService = staticDataService;
    }
    
    public PerkStrategy Create(PerkType type)
    {
      if (type == PerkType.None)
        return null;

      PerkSettings settings = staticDataService.ForPerks().Perk(type);
      
      switch (type)
      {
        case PerkType.OpenFirstUserCard:
          return new OpenFirstUserCardPerk(settings, userHands);
        case PerkType.OpenLastUserCard:
          return new OpenLastUserCardPerk(settings, userHands);
        case PerkType.RemoveUserFirstCard:
          return new RemoveUserFirstCardPerk(settings, userHands);
        case PerkType.RemoveUserLastCard:
          return new RemoveUserLastCardPerk(settings, userHands);
        case PerkType.AddCardToDealer:
          return new AddCardToDealerPerk(settings, dealerMachine);
        case PerkType.RemoveLastDealerCard:
          return new RemoveLastDealerCardPerk(settings, dealerMachine);
        case PerkType.SwapFirstCards:
          return new SwapFirstCardsPerk(settings, userHands, dealerMachine);
        case PerkType.TakeFullHands:
          return new TakeFullHands(settings, userHands, dealerMachine);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}