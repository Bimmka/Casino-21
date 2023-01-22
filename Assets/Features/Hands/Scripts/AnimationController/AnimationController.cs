using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    
    public bool isPlaying = false;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();

    }
    
    public void SetBool(int hash, bool flag)
    {
        _animator.SetBool(hash, flag);
    }
    
    public void SetTrigger(int hash)
    {
        _animator.SetTrigger(hash);
    }
    
    public void StopAnimation()
    {
        isPlaying = false;
    }

}
