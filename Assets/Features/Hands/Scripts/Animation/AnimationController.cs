using System;
using UnityEngine;

namespace Features.Hands.Scripts.Animation
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private event Action savedEvent;

        public void SaveEvent(Action callback) => 
            savedEvent = callback;

        public void SetBool(int hash, bool flag) => 
            _animator.SetBool(hash, flag);

        public void SetTrigger(int hash) => 
            _animator.SetTrigger(hash);

        public void TriggerEvent()
        {
            savedEvent?.Invoke();
            savedEvent = null;
        }
    }
}
