using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatType : ScriptableObject
{
    [SerializeField]
    private Sprite representation;
    public Sprite Representation => representation;

    [SerializeField]
    private CombatType weakTo;
    
    [SerializeField]
    private CombatType strongTo;

    [SerializeField]
    private Color color;
    public Color Color => color;

    [SerializeField]
    private int innitiative = 0;
    public int Innitiative => innitiative;

    public CombatTypeRelation GetDefenderRelation(CombatType attackerType)
    {
        if(attackerType == weakTo)
        {
            return CombatTypeRelation.Weak;
        } 
        else if(attackerType == strongTo)
        {
            return CombatTypeRelation.Strong;
        }

        return CombatTypeRelation.Same;
    }
}
