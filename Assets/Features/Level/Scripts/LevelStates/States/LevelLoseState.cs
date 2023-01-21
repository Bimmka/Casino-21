using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.Level.Scripts.LevelStates.States
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