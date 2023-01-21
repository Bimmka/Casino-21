using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
  public class RegistrationState : IState
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IWindowsService windowsService;

    public RegistrationState(ISceneLoader sceneLoader, IWindowsService windowsService)
    {
      this.sceneLoader = sceneLoader;
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      sceneLoader.Load(GameConstants.RegistrationScene, OnLoaded);
    }

    public void Exit()
    {
      
    }

    private void OnLoaded()
    {
      windowsService.Open(WindowId.Registration);
    }
  }
}