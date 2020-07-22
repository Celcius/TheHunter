using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class CombatAction
{
    protected CombatCharacter caster;
    public CombatCharacter Caster => caster;

    protected CombatCharacter[] targets;
    public CombatCharacter[] Targets => targets;

    public CombatAction(CombatCharacter caster, CombatCharacter[] targets)
    {
        this.caster = caster;
        this.targets = targets;
    }

    public void PerformAction(Action onPerformedCallback, CoroutineRunner runner)
    {
        runner.StartCoroutine(PerformAction(onPerformedCallback));
    }

    private IEnumerator PerformAction(Action onPerformedCallback)
    {
        yield return caster.OnAction(targets);
        onPerformedCallback();
    }

    public Sprite GetRepresentation()
    {
        return caster.NextAction.Representation;
    }
}
