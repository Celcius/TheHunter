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

    public IEnumerator Selection => (IsSelectionCharacter? SelectCharacter() : SelectSide());
    
    // Start is called before the first frame update
    private IEnumerator SelectCharacter()
    {
        yield break;
    }

    private IEnumerator SelectSide()
    {
        yield break;
    }

    public abstract IEnumerator ExecuteAction(CombatCharacter caster, CombatCharacter[]  targets);
}
