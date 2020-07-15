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

    [SerializeField]
    private int damageMax;

    [SerializeField]
    private Color representationColor = Color.white;
    public Color RepresentationColor => representationColor;

    [SerializeField]
    private TeamType team;
    public TeamType Team => team;

    [SerializeField]
    private Sprite actionRepresentation;
    public Sprite ActionRepresentation;


}
