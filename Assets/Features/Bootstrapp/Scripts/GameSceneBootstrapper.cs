using Features.Cards.Scripts.Container;
using Features.Cards.Scripts.Data.Deck;
using Features.Cards.Scripts.Deck;
using Features.Cards.Scripts.Factory;
using Features.Cards.Scripts.Shuffle;
using Features.Hands.Scripts.Dealer;
using Features.Hands.Scripts.User;
using Features.Level.LevelStates.Factory;
using Features.Level.LevelStates.Machine;
using Features.Level.Observer;
using Features.Services.UI.Factory.BaseUI;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class GameSceneBootstrapper : MonoInstaller
  {
    [SerializeField] private LevelObserver levelObserverPrefab;
    [SerializeField] private CardDeck deckPrefab;
    [SerializeField] private Transform deckSpawnParent;
    [SerializeField] private DeckSettings deckSettings;
    [SerializeField] private UserHands userHands;
    [SerializeField] private Transform userHandsParent;
    [SerializeField] private DealerHands dealerHands;
    [SerializeField] private Transform dealerHandsParent;

    private CardDeck spawnedDeck;
    
    public override void Start()
    {
      base.Start();
      Container.Resolve<IUIFactory>();
      Container.Resolve<LevelObserver>();
    }

    public override void InstallBindings()
    {
      BindUIFactory();
      BindLevelStateMachine();
      BindLevelStatesFactory();
      BindDeckShuffler();
      BindLevelObserverPrefab();
      BindDeckPrefab();
      BindCardFactory();
      BindCardsContainer();
      BindUserHands();
      BindDealerHands();
    }

    private void BindLevelStateMachine() => 
      Container.Bind<ILevelStateMachine>().To<LevelStateMachine>().FromNew().AsSingle();

    private void BindLevelStatesFactory() => 
      Container.Bind<LevelStatesFactory>().ToSelf().FromNew().AsSingle();

    private void BindDeckShuffler() => 
      Container.Bind<CardDeckShuffler>().ToSelf().FromNew().AsSingle();

    private void BindUIFactory() =>
      Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();

    private void BindLevelObserverPrefab() => 
      Container.Bind<LevelObserver>().ToSelf().FromComponentInNewPrefab(levelObserverPrefab).AsSingle();

    private void BindDeckPrefab() => 
      Container.Bind<CardDeck>().ToSelf().FromMethod(Deck).AsSingle();

    private CardDeck Deck()
    {
      if (spawnedDeck == null)
      {
        spawnedDeck = Container.InstantiatePrefab(deckPrefab, deckSpawnParent).GetComponent<CardDeck>();
        spawnedDeck.InitializeDeck(deckSettings);
        Container.Inject(spawnedDeck);
      }
      return spawnedDeck;
    }

    private void BindCardFactory() => 
      Container.Bind<CardFactory>().ToSelf().FromNew().AsSingle();

    private void BindCardsContainer() => 
      Container.Bind<CardsContainer>().ToSelf().FromNew().AsSingle();

    private void BindUserHands() => 
      Container.Bind<UserHands>().ToSelf().FromMethod(UserHands).AsSingle();

    private UserHands UserHands() => 
      Container.InstantiatePrefab(userHands, userHandsParent).GetComponent<UserHands>();

    private void BindDealerHands() => 
      Container.Bind<DealerHands>().ToSelf().FromMethod(DealerHands).AsSingle();

    private DealerHands DealerHands() => 
      Container.InstantiatePrefab(dealerHands, dealerHandsParent).GetComponent<DealerHands>();
  }
}