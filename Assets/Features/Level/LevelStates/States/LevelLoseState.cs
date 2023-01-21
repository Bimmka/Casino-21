using Features.GameStates.States.Interfaces;
using Features.Hands.Scripts.User;
using Features.NPC.Scripts.Base;
using Features.Services.GameSettings;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;

namespace Features.Level.LevelStates.States
{
  public class LevelLoseState: IState
  {
    private readonly IWindowsService windowsService;

    public LevelLoseState(IWindowsService windowsService)
    {
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.Lose);
    }

    public void Exit()
    {
      
    }
  }
}