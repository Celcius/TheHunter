using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class CombatSlotArrayVar : ArrayVar<CombatSlotObserver>
{
    public void HideStars(bool shouldShow = true)
    {
        ShowStars(false);
    }

    public void ShowStars(bool shouldShow = true)
    {
        foreach(CombatSlotObserver slot in Value)
        {
            if(slot.CharacterUI != null)
            {
                slot.CharacterUI.ShowStar(shouldShow);
            }
        }
    }
}
