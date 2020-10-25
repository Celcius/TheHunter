using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortInnitiativeState : CombatMachineState
{
    public class CombatActionComparer : IComparer<CombatAction>
    {
        public int Compare(CombatAction x, CombatAction y)
        {
            return (int) Mathf.Sign(x.Caster.innitiative - y.Caster.innitiative);
        }
    }
    
    private float elapsedTime = 0;
    [SerializeField]
    private float animationTime = 0.5f;
    private bool hasAnimatedEnter = false;
    private bool hasSetTrigger = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasAnimatedEnter = false;
        hasSetTrigger = false;
        List<CombatAction> actionsByInnitiative = new List<CombatAction>(actions.Value);
//        
        actionsByInnitiative.Sort(new CombatActionComparer());
        actionUI.Value.HideButtons(false);
        actions.Value = actionsByInnitiative.ToArray();
        
        elapsedTime = animationTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedTime -= Time.deltaTime;

        if(elapsedTime <= 0)
        {
            if(!hasAnimatedEnter)
            {
                actionUI.Value.EnterButtonsWithAction();
                elapsedTime = animationTime;
                hasAnimatedEnter = true;
            }
            else if(!hasSetTrigger)
            {
                combatAnimator.Value.SetTrigger("InnitiativeSorted");
                hasSetTrigger = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
