using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private const string Condition = "state";

    private States State
    {
        get { return (States)_animator.GetInteger(Condition); }
        set { _animator.SetInteger(Condition, (int)value); }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayJumpAnimation()
    {
        State = States.jump;
    }

    public void PlayMoveAnimation()
    {
        State = States.move;
    }

    public void PlayIdleAnimation()
    {
        State = States.idle;
    }

    public enum States
    {
        idle,
        move,
        jump
    }
}
