using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent (typeof(DialogueManager))]
public class EventManager : MonoBehaviour
{

    DialogueManager dialogueManager;

    [SerializeField]
    List<scriptableDialogue> dialogueList = new List<scriptableDialogue>();

    EventDialogue storedEvent;

    bool isWriting;
    int dialogueIndex;

    bool isEvent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    // Update is called once per frame
   
  public  void chooseDialog()
    {
        if (isWriting)
            return;

        isWriting = true;

        if (dialogueList[dialogueIndex] is EventDialogue)
        {
            storedEvent = dialogueList[dialogueIndex].ConvertTo<EventDialogue>();




            
        }


        dialogueManager.receiveDialogue(dialogueList[dialogueIndex]);



    }
    public void endDialog()
    {
        if (storedEvent !=null)
        {
            //instanciar botões de opçoes ou fazer aparecer


            return;
        }


        isWriting = false;

        dialogueIndex++;




    }

    public void choise(int choiseId)
    {

        dialogueManager.receiveDialogue(storedEvent.eventosPossiveis[choiseId]);

        storedEvent = null;

    }


}
