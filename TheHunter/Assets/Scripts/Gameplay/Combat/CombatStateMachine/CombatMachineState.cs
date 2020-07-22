using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class CombatMachineState : StateMachineBehaviour
{
    [SerializeField]
    protected AnimatorVar combatAnimator;

    [SerializeField]
    protected ActionUIVar actionUI;

    [SerializeField]
    protected CombatSlotArrayVar players;

    [SerializeField]
    protected CombatSlotArrayVar enemies;
    
    [SerializeField]
    protected CombatActionVarArray actions;

}
