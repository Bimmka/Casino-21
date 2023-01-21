using Features.Cards.Shuffle;
using Features.Level.LevelStates.Factory;
using Features.Level.LevelStates.Machine;
using Features.Services.UI.Factory.BaseUI;
using Features.UI.Windows.Base.Scripts;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class GameSceneBootstrapper : MonoInstaller
  {
    public override void Start()
    {
      base.Start();
      Container.Resolve<IUIFactory>();
    }

    public override void InstallBindings()
    {
      BindUIFactory();
      BindLevelStateMachine();
      BindLevelStatesFactory();
      BindDeckShuffler();
    }

    private void BindLevelStateMachine() => 
      Container.Bind<ILevelStateMachine>().To<LevelStateMachine>().FromNew().AsSingle();

    private void BindLevelStatesFactory() => 
      Container.Bind<LevelStatesFactory>().ToSelf().FromNew().AsSingle();

    private void BindDeckShuffler() => 
      Container.Bind<CardDeckShuffler>().ToSelf().FromNew().AsSingle();

    private void BindUIFactory() =>
      Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();
  }
}