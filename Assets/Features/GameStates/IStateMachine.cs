using Features.GameStates.States.Interfaces;
using Features.Services;

namespace Features.GameStates
{
  public interface IStateMachine
  {
    void Enter<TState>() where TState : class, IState;
    TState GetState<TState>() where TState : class, IExitableState;
  }
}