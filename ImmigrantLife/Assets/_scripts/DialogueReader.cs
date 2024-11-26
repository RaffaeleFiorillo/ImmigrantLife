using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class DialogueReader : MonoBehaviour
{
    //caixa de texto
  [SerializeField]  TextMeshProUGUI textBox;
  [SerializeField]  TextMeshProUGUI nameBox;

    //lista de dialogos
    [SerializeField]List<scriptableDialogue> dialogueList = new List<scriptableDialogue>();


    //velocidade de texto



    float sentenceDelay;
    [SerializeField] float delaySpeed;
    [SerializeField] float delayAccelaration;




    //stored info
    bool isTalking;
    int sentenceNumber = 0;
    int dialogueNumber = 0;
    string theSentence;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //delay inicial
            sentenceDelay = delaySpeed*0.01f;
        
    }

   


    //trigger de dialogo 
    //ao clicar uma segunda vez aumenta a velocidade 
    public void dialoguing()
    {
        if (!isTalking)
        {
            if (dialogueList[0].dialogueList.Count == sentenceNumber)
            {
                //da reset às sentences
                sentenceNumber = 0;

                //altera o dialogo
                dialogueNumber++;


            }
            //muda o nome 
            nameBox.text = dialogueList[dialogueNumber].dialogueList[sentenceNumber].charInfo.name;

            //muda o que vai ser escrito
            theSentence = dialogueList[dialogueNumber].dialogueList[sentenceNumber].theDialogue;

            //começa a escrever em IEnumerator
        StartCoroutine(write());

        }
        else
        {
            //acelera o texto
            sentenceDelay = delayAccelaration * 0.01f;
        }

      
    }

  



    //colocar o delay à velocidade normal
    public void unDelay()
    {
        if(isTalking)
        sentenceDelay = delaySpeed * 0.01f;


    }

    //escreve o texto aos poucos
    IEnumerator write()
    {
        isTalking = true;
        //writing = true;
        for (int i = 0; i <= theSentence.Length; i++)
        {

            //coloca cada letra na textbox
            textBox.text = theSentence.Substring(0, i);


            if (i == theSentence.Length)
            {//quando acaba a sentence
                isTalking = false;
                sentenceNumber++;

            }


            //para o IEnumerator
            StopCoroutine(write());

            //tempo para proimo loop
            yield return new WaitForSecondsRealtime(sentenceDelay);
        }

    }

    


}
