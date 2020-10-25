using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class SetupState : StateMachineBehaviour
{
   [SerializeField]
    private ActionUIVar actionUI;
    
    [SerializeField]
    private CombatActionVarArray actions;

    private bool needsToAnimate = false;

    [SerializeField]
    private AnimatorVar combatAnimator;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        actions.Clear();
        actionUI.Value.AnimateInitialLayout();
    }

//     OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatAnimator.Value.ResetTrigger("PrepareResolutionDone");
        combatAnimator.Value.ResetTrigger("InnitiativeSorted");
        combatAnimator.Value.ResetTrigger("ResolvedAttack");
        combatAnimator.Value.ResetTrigger("IncreasedAP");
        combatAnimator.Value.ResetTrigger("Entered");
        combatAnimator.Value.ResetTrigger("SelectTarget");
        combatAnimator.Value.ResetTrigger("TargetSelected");
        combatAnimator.Value.ResetTrigger("EnoughActions");
        combatAnimator.Value.SetInteger("EnemyCount",0);
        combatAnimator.Value.SetInteger("PlayerCount",0);
        combatAnimator.Value.SetInteger("ActionsCount",0);
        combatAnimator.Value.SetInteger("ActionsPerformed",0);
        if(actionUI.Value != null && needsToAnimate)
        {
            actionUI.Value.AnimateInitialLayout();
        }    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatAnimator.Value.SetInteger("ActionsCount", 0);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
