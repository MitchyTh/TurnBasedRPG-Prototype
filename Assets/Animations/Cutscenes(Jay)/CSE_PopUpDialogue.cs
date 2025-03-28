using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CSE_PopUpDialogue : CutsceneElementBase
{
    [SerializeField] private TMP_Text popUpText;
    //[SerializeField] private CanvasGroup canvasGroup;
    [TextArea] [SerializeField] private string dialogue;

    [SerializeField] private Animator anim;
   

    public override void Execute()
    {
        anim.Play("FadeIn");
        popUpText.text = dialogue;
    }

    public void ExecuteOut()
    {
        anim.Play("FadeOut");
    }
    
    /*
    public void UpdateDialogue(string newDialogue)
    {
        dialogue = newDialogue;
        popUpText.text = newDialogue;
    }
    */
}
