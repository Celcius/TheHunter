using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CombatSlotObserver : ArrayVarIndexObserver<CharacterArrayVar, CharacterDefinition>
{
    [SerializeField]
    private CombatCharacter slot;
    public CombatCharacter Character => slot;

    private CombatCharacterUI characterUI;
    public CombatCharacterUI CharacterUI => characterUI;

    [SerializeField]
    private CombatActionVarArray combatActions;

    protected override void Start()
    {
        base.Start();
        characterUI = slot.GetComponent<CombatCharacterUI>(); 
    }

    public override void OnChange(CharacterDefinition var)
    {
        gameObject.SetActive(var != null);
        
        slot.Setup(var);    
    }

    public void OnClick()
    {
        //CombatAction newAction = new CombatAction(slot);
    }
}
