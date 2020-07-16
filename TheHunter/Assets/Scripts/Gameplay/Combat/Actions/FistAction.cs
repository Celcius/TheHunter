using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAction : CharacterAction
{
    public override IEnumerator ExecuteAction(CombatCharacter caster, CombatCharacter[]  targets)
    {
        Debug.Log("Fists");
        yield break;
    }
}
