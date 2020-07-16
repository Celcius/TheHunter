using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class SelectTargetState : StateMachineBehaviour
{
    [SerializeField]
    private CombatSlotArrayVar players;

    [SerializeField]
    private CombatSlotArrayVar enemies;

    [SerializeField]
    private AnimatorVar combatAnimator;

    [SerializeField]
    private ActionUIVar actionUI;

    [SerializeField]
    private CombatCharacterVar caster;

    [SerializeField]
    private CombatActionVarArray actions;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        players.ShowStars(true);
        enemies.ShowStars(true);
        
        foreach(CombatSlotObserver observer in players.Value)
        {
            if(observer.Character != null)
            {
                observer.Character.onSelection += OnSelection;
            }
        }

        foreach(CombatSlotObserver observer in enemies.Value)
        {
            if(observer.Character != null)
            {
                observer.Character.onSelection += OnSelection;
            }
        }
    }

    private void OnSelection(CombatCharacter character)
    {

        List<CombatCharacter> selected = new List<CombatCharacter>();
        if(caster.Value.IsSelectionCharacter)
        {
            selected.Add(character);
        }
        else
        {
            CombatSlotArrayVar selectedVars = character.Definition.Team == CharacterDefinition.TeamType.Player? players : enemies;
        
            foreach(CombatSlotObserver selectedCharacter in selectedVars.Value)
            {
                if(selectedCharacter.Character != null && selectedCharacter.Character.IsAlive)
                {
                    selected.Add(selectedCharacter.Character);
                }
            }
            
        }
        CombatAction action = new CombatAction(caster.Value, selected.ToArray());
        actions.Add(action);
        combatAnimator.Value.SetInteger("ActionsCount", actions.Count());
        combatAnimator.Value.SetTrigger("TargetSelected");
        actionUI.Value.AnimateAddAction(actions.Count()-1);
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

                foreach(CombatSlotObserver observer in enemies.Value)
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
