using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareCombatResolutionState : CombatMachineState
{
    private bool hasTransitioned = false;
    private float elapsedAnimationTime = 0;
    [SerializeField]
    private float enterAnimationDuration = 2.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        players.ShowStars(false);
        enemies.ShowStars(false);

        hasTransitioned =  false;
        SelectEnemyAttacks(); 

        for(int i = (int)(actions.Value.Length/2.0f); i < actions.Value.Length; i++)
        {
            actionUI.Value.AnimateEnterWithAction(i);
        }
        elapsedAnimationTime = enterAnimationDuration;
    }

//     OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedAnimationTime -= Time.deltaTime;
        if(!hasTransitioned && elapsedAnimationTime <= 0)
        {
            combatAnimator.Value.SetTrigger("PrepareResolutionDone");
            hasTransitioned = true;
        }
    }

    private void SelectEnemyAttacks()
    {
        if(hasTransitioned)
        {
            return;
        }
        List<CombatCharacter> enemySelections = new List<CombatCharacter>();
        List<CombatCharacter> playerSelections = new List<CombatCharacter>();
        foreach(CombatSlotObserver slot in enemies.Value)
        {
            if(slot.Character.IsAlive)
            {
                enemySelections.Add(slot.Character);
            }
        }

        foreach(CombatSlotObserver slot in players.Value)
        {
            if(slot.Character.IsAlive)
            {
                playerSelections.Add(slot.Character);
            }
        }

        int aliveEnemies = enemySelections.Count;
        int alivePlayers = playerSelections.Count;

        if(aliveEnemies == 0 || alivePlayers == 0)
        {
            return;
        }

        for(int i = 0 ; i < 3; i++)
        {
            int attackerIndex = Random.Range(0, aliveEnemies);
            

            CombatCharacter caster = enemySelections[attackerIndex];
            CombatCharacter[] selected;
            if(caster.IsSelectionCharacter)
            {
                int targetIndex = Random.Range(0, alivePlayers);
                selected = new CombatCharacter[]{playerSelections[targetIndex]};
            }
            else
            {
                selected = playerSelections.ToArray();
            }
            CombatAction action = new CombatAction(caster, selected);
            actions.Add(action);
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
