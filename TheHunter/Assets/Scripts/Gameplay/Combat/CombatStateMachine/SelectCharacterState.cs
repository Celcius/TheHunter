﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class SelectCharacterState : StateMachineBehaviour
{
    [SerializeField]
    private CombatSlotArrayVar players;

    [SerializeField]
    private CombatSlotArrayVar enemies;

    [SerializeField]
    private AnimatorVar combatAnimator;

    [SerializeField]
    private CombatCharacterVar caster;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        players.ShowStars(true);
        enemies.ShowStars(false);
        
        foreach(CombatSlotObserver observer in players.Value)
        {
            if(observer.Character != null)
            {
                observer.Character.onSelection += OnSelection;
            }
        }
    }

    private void OnSelection(CombatCharacter character)
    {
        caster.Value = character;
        combatAnimator.Value.SetTrigger("SelectTarget");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(CombatSlotObserver observer in players.Value)
        {
            if(observer.Character != null)
            {
                observer.Character.onSelection -= OnSelection;
            }
        }
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
