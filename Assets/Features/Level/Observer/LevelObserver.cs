using System;
using Features.Level.LevelStates.Machine;
using Features.Level.LevelStates.States;
using UnityEngine;
using Zenject;

namespace Features.Level.Observer
{
  public class LevelObserver : MonoBehaviour
  {
    private ILevelStateMachine levelStateMachine;

    [Inject]
    public void Construct(ILevelStateMachine levelStateMachine)
    {
      this.levelStateMachine = levelStateMachine;
    }

    private void Start()
    {
      levelStateMachine.Enter<LevelBetState>();
    }
  }
}