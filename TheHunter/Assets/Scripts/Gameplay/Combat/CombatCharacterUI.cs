using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CombatCharacter))]
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

    public void Start()
    {
        character = GetComponent<CombatCharacter>();
    }

    public void OnDefinitionChange(CharacterDefinition definition)
    {
        this.definition = definition;
        if(definition == null)
        {
            mainImage.sprite = null;
            mainImage.color = Color.white;
            typeImage.sprite = null;
            nameLabel.text = "";
            starImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.sprite = definition.Representation;
            mainImage.color = definition.RepresentationColor;
            typeImage.sprite = definition.Type.Representation;
            nameLabel.text = definition.name;
            starImage.gameObject.SetActive(false);
            starImage.color = definition.Type.Color; 
        }
        this.gameObject.SetActive(definition != null);
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
}
