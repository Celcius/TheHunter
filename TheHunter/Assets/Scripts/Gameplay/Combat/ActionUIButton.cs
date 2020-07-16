using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class ActionUIButton : MonoBehaviour
{
    private Button button;
    public Button Button => button;

    [SerializeField]
    private Image image;

    private Animator animationHandler;
    
    void Start()
    {

        button = GetComponent<Button>();
        animationHandler = GetComponent<Animator>();
    }

    public void SetCanAnimate(bool canAnimate)
    {
        animationHandler.enabled = canAnimate;
    }

    public void AnimateAdd(Sprite newImage)
    {   
        image.sprite = newImage;
        animationHandler.SetTrigger("AddAction");
    }

    public void AnimateUse()
    {
        animationHandler.SetTrigger("UseAction");
    }

    public void Cancel()
    {
        animationHandler.SetTrigger("CancelAction");
    }

    public void AnimateEnter()
    {
        animationHandler.SetTrigger("Appear");
    }

    public float GetCurrentAnimationLength()
    {
     //   animationHandler.GetCurrentAnimatorStateInfo(0).Length;   
        return 1.0f;
    }

    public void AnimateEnterWithAction()
    {
        animationHandler.SetTrigger("AppearWAction");
    }

    public void Hide()
    {
        animationHandler.SetTrigger("Hide");
    }

    public void UpdateModel()
    {

    }
}
