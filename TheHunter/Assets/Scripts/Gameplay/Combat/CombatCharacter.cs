﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


[RequireComponent(typeof(CombatCharacterUI))]
public class CombatCharacter : MonoBehaviour
{
    private CombatCharacterUI characterUI;
    public CombatCharacterUI CharacterUI => characterUI;

    private CharacterDefinition definition;
    public  CharacterDefinition Definition => definition;

    public bool IsSelectionCharacter => NextAction.IsSelectionCharacter;

    public int innitiative => Definition.Type.Innitiative;
    
    public delegate void OnSelectionStarted(CombatCharacter Character);

    public event OnSelectionStarted onSelection;

    private int currentHealth = 0;
    public int Health => currentHealth;
    public bool IsAlive => Health > 0;

    private int currentActionPoints = 0;
    public int ActionPoints => currentActionPoints;

    private const int maxActionPoints = 3;

    public CharacterAction NextAction => Definition.Action;

    [SerializeField]
    private CombatCharacter[] playerCharacters;

    private void Start()
    {
        characterUI = GetComponent<CombatCharacterUI>();
    }

    public void Setup(CharacterDefinition definition)
    {
#if UNITY_EDITOR
        if(characterUI == null)
        {
            characterUI  = GetComponent<CombatCharacterUI>();
        }
#endif
        this.definition = definition;
        this.currentHealth = definition == null? 0 : definition.Health;
        characterUI.OnDefinitionChange(definition);
    }

    public void IncrementActionPoints()
    {
        currentActionPoints = Mathf.Clamp(++currentActionPoints, 0, maxActionPoints);
        characterUI.UpdateUI();
    }

    public void ClearActionPoints()
    {
        currentActionPoints = 0;
        characterUI.UpdateUI();
    }

    public void ChangeHealth(int offset)
    {
        currentHealth = Mathf.Clamp(currentHealth + offset, 0, definition.Health);
    }

    public IEnumerator OnAction(CombatCharacter[] targets)
    {
        yield return definition.Action.ExecuteAction(this, targets);
    }

    public void OnClick()
    {
        onSelection?.Invoke(this);
    }

    
    public void TakeDamage(int damage)
    {
        ChangeHealth(damage);
        characterUI.AnimateDamage();
    }

#if UNITY_EDITOR
[CustomEditor(typeof(CombatCharacter))]
public class UndrawableGraphicEditor : Editor
{
    private CombatCharacter character;
    private CharacterDefinition prevDefinition;

    public void OnEnable()
    {
        character = (CombatCharacter)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();
        prevDefinition = (CharacterDefinition)EditorGUILayout.ObjectField(prevDefinition, typeof(CharacterDefinition), false);

        if (EditorGUI.EndChangeCheck())
        {
            character.Setup(prevDefinition);
            EditorUtility.SetDirty(target);
        }
    }
}
#endif
}
