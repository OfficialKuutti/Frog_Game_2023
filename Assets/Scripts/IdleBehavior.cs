using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    [SerializeField]
    private float timeUntilOtherIdle;

    [SerializeField]
    private int numberOfOtherIdles;

    private bool isOtherIdle;
    private float idleTime;
    private int idleAnimation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isOtherIdle == false)
        {
            idleTime += Time.deltaTime;
            
            if(idleTime > timeUntilOtherIdle && stateInfo.normalizedTime % 1 < 0.02f)
            {
                isOtherIdle = true;
                idleAnimation = Random.Range(1, numberOfOtherIdles + 1);
                idleAnimation = idleAnimation * 2 - 1;

                animator.SetFloat("IdleAnimation", idleAnimation - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98f)
        {
            ResetIdle();
        }
        animator.SetFloat("IdleAnimation", idleAnimation, 0.2f, Time.deltaTime);
    }

    private void ResetIdle()
    {
        if(isOtherIdle)
        {
            idleAnimation--;
        }

        isOtherIdle = false;
        idleTime = 0;
    }
}
