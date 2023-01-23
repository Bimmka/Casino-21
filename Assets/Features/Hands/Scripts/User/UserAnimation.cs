using System;
using Features.Hands.Scripts.Animation;
using UnityEngine;

namespace Features.Hands.Scripts.User
{
    public class UserAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationController animationController;
        
        private static readonly int Loss = Animator.StringToHash( "Lose");
        private static readonly int More = Animator.StringToHash( "More");
        private static readonly int Stop = Animator.StringToHash( "Stop");
        private static readonly int Win = Animator.StringToHash("Win");

        public void SetLose() => 
            animationController.SetTrigger(Loss);

        public void SetMore(Action callback)
        {
            animationController.SaveEvent(callback);
            animationController.SetTrigger(More);
        }
    
        public void SetStop(Action callback)
        {
            animationController.SaveEvent(callback);
            animationController.SetTrigger(Stop);
        }

        public void SetWin() => 
            animationController.SetTrigger(Win);
    }
}
