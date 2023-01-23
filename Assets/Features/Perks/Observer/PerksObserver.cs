using System;
using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Perks.Data;
using Features.Perks.Factory;
using Features.Perks.Strategy;
using Features.Services.GameSettings;
using Features.Services.StaticData;
using Features.Services.UserProvider;
using Features.User.Data;
using UnityEngine;
using Zenject;

namespace Features.Perks.Observer
{
    public class PerksObserver : MonoBehaviour
    {
        private UserHands userHands;
        private PerkStrategyFactory factory;
        private IGameSettings gameSettings;
        private IStaticDataService staticDataService;
        private UserPointsData userPoints;

        private PerkSettings settings;
        private PerkStrategy strategy;

        public bool IsCanUsePerk => IsHavePerk && strategy.IsCanBeUsed() 
                                               && userPoints.IsEnough(settings.UseCost) && IsUsed == false;
        public bool IsHavePerk => strategy != null;
        public PerkType PerkType => settings.Type;
        public PerkTargetType Target => settings.TargetType;
        public bool IsUsed { get; private set; }

        [Inject]
        public void Construct(UserHands userHands, PerkStrategyFactory factory, IGameSettings gameSettings,
            IStaticDataService staticDataService, IUserProvider userProvider)
        {
            userPoints = userProvider.User.PointsData;
            this.staticDataService = staticDataService;
            this.gameSettings = gameSettings;
            this.factory = factory;
            this.userHands = userHands;
        }

        public void Initialize(ILevelStateMachine levelStateMachine)
        {
            if (gameSettings.PerkType == PerkType.None)
                return;

            settings = staticDataService.ForPerks().Perk(gameSettings.PerkType);
            strategy = factory.Create(settings, levelStateMachine);
        }

        public void Reset()
        {
            IsUsed = false;
        }

        public void Use(PerkTargetType targetType, Action callback = null)
        {
            userPoints.Reduce(settings.UseCost);
            IsUsed = true;
            if (targetType == settings.TargetType)
                Apply(callback);
        }

        public void Apply(Action callback)
        {
            strategy.Use(callback);
        }
    }
}
