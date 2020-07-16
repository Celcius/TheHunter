using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScrollAction : CharacterAction
{
    public override IEnumerator ExecuteAction(CombatCharacter caster, CombatCharacter[]  targets)
    {
        Debug.Log("Scroll");
        yield break;
    }
}
