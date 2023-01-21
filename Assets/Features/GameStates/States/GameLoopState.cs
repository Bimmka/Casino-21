using Features.GameStates.States.Interfaces;

namespace Features.GameStates.States
{
  public class GameLoopState : IState
  {
    public GameLoopState(IGameStateMachine gameStateMachine)
    {
      gameStateMachine.Register(this);
    }
    
    public void Enter()
    {
      
    }

    public void Exit()
    {
      
    }
  }
}