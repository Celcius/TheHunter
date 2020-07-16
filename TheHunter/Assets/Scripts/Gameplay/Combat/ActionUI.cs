using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AmoaebaUtils;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ActionUI : MonoBehaviour
{
    [SerializeField]
    private ActionUIButton[] buttons;

    private Vector3[] initPositions;

    [SerializeField]
    private Animator combatAnimator;

    private float distanceRef;

    private IntArrVar positions;

    private IEnumerator turnAction = null;

    [SerializeField]
    private IntVar currentActionIndex;

    [SerializeField]
    private BoolVar usingActions;

    [SerializeField]
    private CombatActionVarArray actions;

    public void Start()
    {
        currentActionIndex.OnChange += OnActionIndexChange;

        initPositions = new Vector3[buttons.Length];
        for(int i = 0; i < buttons.Length; i++)
        {
            initPositions[i] = buttons[i].transform.position;
        }

        if(initPositions.Length >= 2)
        {
            distanceRef = Mathf.Abs(initPositions[1].x - initPositions[0].x);
        }
    }

    private void OnDestroy()
    {
        currentActionIndex.OnChange -= OnActionIndexChange;
    }

    public void AnimateInitialLayout()
    {
        StopTurnAnimation();
        for(int i = 0; i < buttons.Length; i++)
        {
            ActionUIButton button = buttons[i];
            button.transform.position = initPositions[i];
            
            if(i < buttons.Length/2.0f)
            {
                buttons[i].AnimateEnter();
            }
            else
            {
                buttons[i].Hide();
            }
        }
        combatAnimator.SetTrigger("Entered");
    }

    public void AnimateAddAction(int index)
    {
        ActionUIButton button = GetButton(index);
        if(button != null)
        {
            CombatAction action = actions.Value[index];
            button.AnimateAdd(action.GetRepresentation());
        }
    }
    

    private void OnActionIndexChange(int oldIndex, int newIndex)
    {
        ActionUIButton button = GetButton(newIndex);
        if(usingActions.Value && button != null)
        {
            button.AnimateUse();
        }
    }

    private void StopTurnAnimation()
    {
        if(turnAction != null)
        {
            StopCoroutine(turnAction);
        }

        turnAction = null;
    }

    private ActionUIButton GetButton(int index)
    {
        if(index < 0 || index >= buttons.Length)
        {
            return null;
        }
        return buttons[index];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ActionUI))]
public class ActionUIEditor : Editor
{
    
    private ActionUI actionUI;

    private void OnEnable()
    {
        actionUI = (ActionUI)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(UnityEngineUtils.IsInPlayModeOrAboutToPlay() && GUILayout.Button("Test Use"))
        {
            //actionUI.AnimateTurnActions(() => 
            //{
                
            //});
        }
    }
}

#endif
