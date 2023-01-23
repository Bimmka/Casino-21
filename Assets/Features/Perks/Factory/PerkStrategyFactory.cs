using System;
using Features.CustomCoroutine;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.Info;
using Features.Level.Scripts.LevelStates.Machine;
using Features.NPC.Scripts.Base;
using Features.Perks.Data;
using Features.Perks.Strategy;
using Features.Services.GameSettings;
using Features.Services.UI.Windows;

namespace Features.Perks.Factory
{
  public class PerkStrategyFactory
  {
    private readonly UserHands userHands;
    private readonly DealerMachine dealerMachine;
    private readonly ICoroutineRunner coroutineRunner;
    private readonly IWindowsService windowsService;
    private readonly IGameSettings gameSettings;
    private readonly LevelInfoDisplayer infoDisplayer;

    public PerkStrategyFactory(UserHands userHands, DealerMachine dealerMachine, ICoroutineRunner coroutineRunner,
      IWindowsService windowsService, IGameSettings gameSettings, LevelInfoDisplayer infoDisplayer)
    {
      this.userHands = userHands;
      this.dealerMachine = dealerMachine;
      this.coroutineRunner = coroutineRunner;
      this.windowsService = windowsService;
      this.gameSettings = gameSettings;
      this.infoDisplayer = infoDisplayer;
    }
    
    public PerkStrategy Create(PerkSettings settings, ILevelStateMachine levelStateMachine)
    {
      switch (settings.Type)
      {
        case PerkType.OpenFirstUserCard:
          return new OpenFirstUserCardPerk(settings, userHands);
        case PerkType.OpenLastUserCard:
          return new OpenLastUserCardPerk(settings, userHands);
        case PerkType.RemoveUserFirstCard:
          return new RemoveUserFirstCardPerk(settings, userHands, windowsService);
        case PerkType.RemoveUserLastCard:
          return new RemoveUserLastCardPerk(settings, userHands);
        case PerkType.AddCardToDealer:
          return new AddCardToDealerPerk(settings, dealerMachine);
        case PerkType.RemoveLastDealerCard:
          return new RemoveLastDealerCardPerk(settings, dealerMachine);
        case PerkType.SwapFirstCards:
          return new SwapFirstCardsPerk(settings, userHands, dealerMachine);
        case PerkType.TakeFullHands:
          return new TakeFullHands(settings, userHands, dealerMachine, coroutineRunner, windowsService, levelStateMachine);
        case PerkType.AddBet:
          return new MultiplyBetPerk(settings, gameSettings, infoDisplayer); 
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}