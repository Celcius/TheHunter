using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CombatCharacter))]
[RequireComponent(typeof(Animator))]
public class CombatCharacterUI : MonoBehaviour
{
    [SerializeField]
    private Image mainImage;

    [SerializeField]
    private Image typeImage;

    [SerializeField]
    private TextMeshProUGUI nameLabel;

    [SerializeField]
    private Image starImage;

    private CharacterDefinition definition;

    private CombatCharacter character;

    private Animator characterAnimator;

    public void Start()
    {
        character = GetComponent<CombatCharacter>();
        characterAnimator = GetComponent<Animator>();
    }

    public void OnDefinitionChange(CharacterDefinition definition)
    {
        characterAnimator.SetTrigger("Hide");
        this.definition = definition;
        if(definition == null)
        {
            mainImage.sprite = null;
            typeImage.sprite = null;
            nameLabel.text = "";
            starImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.sprite = definition.Representation;
            typeImage.sprite = definition.Type.Representation;
            nameLabel.text = definition.name;
            starImage.gameObject.SetActive(false);
            starImage.color = definition.Type.Color; 
        }
        this.gameObject.SetActive(definition != null);

        if(definition != null)
        {
            characterAnimator.SetBool("IsAlive", character.IsAlive);
        }
    }

    public void ShowStar(bool shouldShow)
    {
        shouldShow &= definition != null;
        starImage.gameObject.SetActive(shouldShow);
    }

    public void UpdateUI()
    {
        // TODO
    }

    public void AnimateAttack()
    {
        characterAnimator.SetTrigger("Attack");
    }

    public void AnimateBuff()
    {
        characterAnimator.SetTrigger("Buff");
    }

    public void AnimateDamage()
    {
        
        characterAnimator.SetBool("IsAlive", character.IsAlive);
        characterAnimator.SetTrigger("TakeDamage");
    }
}
