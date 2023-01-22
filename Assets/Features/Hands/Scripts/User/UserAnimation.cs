using UnityEditor.Animations;
using UnityEngine;

public class UserAnimation : MonoBehaviour
{
    private AnimationController _animationController;
    private Animator _animator;

    private string idle = "Sceletal_Hand|Hand_1_Idle";
    private string loss = "Sceletal_Hand|Hand_1_Lose";
    private string more = "Sceletal_Hand|Hand_1_More";
    private string stop = "Sceletal_Hand|Hand_1_Stop";
    private string win = "Sceletal_Hand|Hand_1_Win";

    void Start()
    {
        _animationController = GetComponent<AnimationController>();
        
        AnimatorController animatorController = (AnimatorController) _animator.runtimeAnimatorController;

        animatorController.AddParameter (Animator.StringToHash(idle).ToString(), AnimatorControllerParameterType.Bool);
        animatorController.AddParameter (Animator.StringToHash(loss).ToString(), AnimatorControllerParameterType.Trigger);
        animatorController.AddParameter (Animator.StringToHash(more).ToString(), AnimatorControllerParameterType.Trigger);
        animatorController.AddParameter (Animator.StringToHash(stop).ToString(), AnimatorControllerParameterType.Trigger);
        animatorController.AddParameter (Animator.StringToHash(win).ToString(), AnimatorControllerParameterType.Trigger);
        
    }

    public void Idle(bool flag)
    {
        _animationController.SetBool(Animator.StringToHash(idle),flag);
    }
    
    public void Loss(bool flag)
    {
        _animationController.SetTrigger(Animator.StringToHash(loss));
    }
    
    public void More(bool flag)
    {
        _animationController.SetTrigger(Animator.StringToHash(more));
    }
    
    public void Stop(bool flag)
    {
        _animationController.SetTrigger(Animator.StringToHash(stop));
    }
    
    public void Win(bool flag)
    {
        _animationController.SetTrigger(Animator.StringToHash(win));
    }

}
