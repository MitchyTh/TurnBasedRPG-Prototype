using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenInitiator : MonoBehaviour
{
    private CutsceneHandler cutsceneHandler;
    private CSE_PopUpDialogue popUpDialogue;
    //private CanvasGroup textBoxCanvasGroup;
    public string newDialogue;

    public void Start()
    {
        cutsceneHandler = GetComponent<CutsceneHandler>();
        popUpDialogue = GetComponentInChildren<CSE_PopUpDialogue>();
        //textBoxCanvasGroup = popUpDialogue.GetComponentInParent<CanvasGroup>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            /*
            if (popUpDialogue != null)
            {
                popUpDialogue.UpdateDialogue(newDialogue);
            }
            if (textBoxCanvasGroup != null)
            {
                textBoxCanvasGroup.alpha = 0;  
                popUpDialogue.Execute();  
            }
            */
            cutsceneHandler.ResetCutscene(); 
            cutsceneHandler.PlayNextElement();
        }
    }

       private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && popUpDialogue != null)
        {
            if (popUpDialogue != null)
            {
                popUpDialogue.ExecuteOut();
            }
        }
    }
}
