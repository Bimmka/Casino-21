using Features.GameStates.States;

namespace Features.GameStates
{
  public class Game
  {
    public readonly IGameStateMachine StateMachine;

    public Game(IGameStateMachine gameStateMachine)
    {
      StateMachine = gameStateMachine;
    }

    public void StartGame()
    {
      StateMachine.Enter<MainMenuState>();
    }

    public void Cleanup()
    {
      
    }
  }
}