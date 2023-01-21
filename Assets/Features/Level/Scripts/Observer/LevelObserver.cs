using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using UnityEngine;
using Zenject;

namespace Features.Level.Scripts.Observer
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
      levelStateMachine.Enter<LevelPrepareState>();
    }
  }
}