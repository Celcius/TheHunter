using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDefinition : ScriptableObject
{
    public enum TeamType 
    {
        Player,
        Enemy
    }

    [SerializeField]
    private int health;
    public int Health => health;
    
    [SerializeField]
    private CombatType type;
    public CombatType Type => type;
    
    [SerializeField]
    private Sprite representation;
    public Sprite Representation => representation;

    [Header("Prototype Stats")]    
    
    [SerializeField]
    private int damageMin;

    public int DamageMin => damageMin;

    [SerializeField]
    private int damageMax;

    public int DamageMax => damageMax;

    [SerializeField]
    private TeamType team;
    public TeamType Team => team;
    
    [SerializeField]
    private CharacterAction action;
    public CharacterAction Action => action;

}
