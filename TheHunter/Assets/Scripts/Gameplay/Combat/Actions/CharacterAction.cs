using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction : ScriptableObject
{
    [SerializeField]
    private bool isSelectionCharacter = false;
    public bool IsSelectionCharacter => isSelectionCharacter;

    [SerializeField]
    private Sprite representation;
    public Sprite Representation => representation;

    [SerializeField]
    private CombatTypeDefinition combatTypeDefinition;

    public IEnumerator ExecuteAction(CombatCharacter caster, CombatCharacter[]  targets)
    {
        if(caster == null || targets == null || targets.Length == 0)
        {
            yield break;
        }

        if(targets[0].Definition.Team == caster.Definition.Team)
        {
            yield return BuffAction(caster, targets);
        }
        else
        {
            yield return AttackAction(caster, targets);
        }
    }

    protected virtual IEnumerator AttackAction(CombatCharacter caster, CombatCharacter[] targets)
    {
        CharacterDefinition def = caster.Definition;
        caster.CharacterUI.AnimateAttack();

        yield return new WaitForSeconds(1.0f);

        foreach(CombatCharacter target in targets)
        {
            if(target != null && target.IsAlive)
            {
                int damage = Random.Range(def.DamageMin, def.DamageMax);
                damage = (int)Mathf.Round(damage * combatTypeDefinition.GetDamageModifier(def.Type, target.Definition.Type));
                target.TakeDamage(damage);
            }
        }

        yield return new WaitForSeconds(1.0f);
    }

    protected virtual IEnumerator BuffAction(CombatCharacter caster, CombatCharacter[] targets)
    {
        CharacterDefinition def = caster.Definition;
        caster.CharacterUI.AnimateBuff();
        yield return new WaitForSeconds(1.0f);
    }
}
