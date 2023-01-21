using Features.Cards.Shuffle;
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
  }
}