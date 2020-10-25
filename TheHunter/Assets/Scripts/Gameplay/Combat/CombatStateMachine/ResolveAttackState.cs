using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class ResolveAttackState : CombatMachineState
{
    int actionToCast = 0;
    int actionsCount = 0;
    
    static CoroutineRunner resolveAttackRunner = null;
    
       // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(resolveAttackRunner == null)
        {
            resolveAttackRunner = CoroutineRunner.Instantiate("ResolveAttackRunner");
            DontDestroyOnLoad(resolveAttackRunner);
        }
        actionsCount = actions.Value.Length;
        actionToCast = 0;
        OnAction();
    }

    private void OnAction()
    {
        combatAnimator.Value.SetInteger("ActionsPerformed", actionToCast);
        if(actionToCast >= actionsCount)
        {
            actions.Clear();
            UpdateAlive();
            combatAnimator.Value.SetTrigger("ResolvedAttack");
            return;
        }

        CombatAction action = actions.Value[actionToCast];
        CombatAction[] actionsArr = actions.Value;
        actionsArr[actionToCast] = null;
        actions.Value = actionsArr;
        actionUI.Value.AnimateUse(actionToCast);
        actionToCast++;

        action.PerformAction(OnAction, resolveAttackRunner);
    }

    private void UpdateAlive()
    {
        int alivePlayers = CountSlot(players);
        int aliveEnemies = CountSlot(enemies);
        combatAnimator.Value.SetInteger("PlayerCount", alivePlayers);
        combatAnimator.Value.SetInteger("EnemyCount", aliveEnemies);
    }

    private int CountSlot(CombatSlotArrayVar var)
    {
        int count = 0;
        foreach(CombatSlotObserver observer in var.Value)
        {
            if(observer != null && observer.Character.IsAlive)
            {
                ++count;
            }
        }
        return count;
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

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
