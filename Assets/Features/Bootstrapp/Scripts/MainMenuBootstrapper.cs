using Features.Services.UI.Factory.BaseUI;
using Features.UI.Windows.Base.Scripts;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class MainMenuBootstrapper : MonoInstaller
  {
    public override void Start()
    {
      base.Start();
      Container.Resolve<IUIFactory>();
    }

    public override void InstallBindings()
    {
      BindUIFactory();
    }
    
    private void BindUIFactory() =>
      Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();
  }
}