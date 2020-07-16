using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerAction : CharacterAction
{
    public override IEnumerator ExecuteAction(CombatCharacter caster, CombatCharacter[]  targets)
    {
        Debug.Log("Daggers");
        yield break;
    }
}
