using System;
using System.Collections.Generic;
using Features.GameStates.Factory;
using Features.GameStates.States.Interfaces;
using Zenject;

namespace Features.GameStates
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly GameStatesFactory factory;
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    [Inject]
    public GameStateMachine(GameStatesFactory factory)
    {
      this.factory = factory;
      _states = new Dictionary<Type, IExitableState>(5);
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Enter<TState, TPayload, TCallback>(TPayload payload, TCallback loadedCallback, TCallback curtainHideCallback) where TState : class, IPayloadedCallbackState<TPayload, TCallback>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload, loadedCallback, curtainHideCallback);
    }

    public TState GetState<TState>() where TState : class, IExitableState
    {
      if (_states.ContainsKey(typeof(TState)) == false)
        _states.Add(typeof(TState), factory.Create<TState>(this));
      
      return _states[typeof(TState)] as TState;
    }


    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    public void Cleanup()
    {
      
    }
  }
}