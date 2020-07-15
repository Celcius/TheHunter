using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTypeDefinition : ScriptableObject
{
    [SerializeField]
    private CombatType[] combatTypes;

    [SerializeField]
    private float WeakTypeModifier = 1.5f;

   [SerializeField]
    private float SameTypeModifier = 1.0f;

    [SerializeField]
    private float StrongTypeModifier = 0.75f;

    public float GetDamageModifier(CombatType attackerType, CombatType defenderType)
    {
        if(attackerType == null)
        {
            Debug.LogError($"Trying to get damage modifier from null combat types {attackerType} -> {defenderType}");
            return SameTypeModifier;
        }
        CombatTypeRelation relation = defenderType.GetDefenderRelation(attackerType);

        switch(relation)
        {
            case CombatTypeRelation.Weak:
                return WeakTypeModifier;
            case CombatTypeRelation.Same:
                return SameTypeModifier;
            case CombatTypeRelation.Strong:
                return StrongTypeModifier;
        }
        return SameTypeModifier;
    }
}
