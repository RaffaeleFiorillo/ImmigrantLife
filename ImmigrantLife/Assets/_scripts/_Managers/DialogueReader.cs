using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueReader : MonoBehaviour
{
    #region Campos Serializados

    /// <summary>
    /// Parte do UI onde é mostrado o texto dos dialogos.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI DialogueTextBox;

    /// <summary>
    /// Nome da Caixa de texto de dialogo. Aparece no UI, em cima do <see cref="DialogueTextBox"/>.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI DialogueTextBoxName;

    /// <summary>
    /// Lista de dialogos a ser mostrada.
    /// </summary>
    [SerializeField]
    List<scriptableDialogue> dialogueList = new List<scriptableDialogue>();

    #endregion Campos Serializados

    //velocidade de texto
    private float sentenceDelay { get; set; }

    [SerializeField] 
    float delaySpeed { get; set; }

    [SerializeField] 
    float delayAccelaration { get; set; }


    //stored info
    public bool isTalking { get; set; }
    int sentenceNumber { get; set; } = 0;
    int dialogueNumber { get; set; } = 0;

    string theSentence { get; set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //delay inicial
        sentenceDelay = delaySpeed*0.01f;
    }

   
    //trigger de dialogo 
    //ao clicar uma segunda vez aumenta a velocidade 
    public void StartDialogue()
    {
        // se já estiver falando, acelera-se o texto
        if (isTalking)
        {
            sentenceDelay = delayAccelaration * 0.01f;
            return;
        }

        if (dialogueList[0].dialogueList.Count == sentenceNumber)
        {
            //da reset às sentences
            sentenceNumber = 0;

            //altera o dialogo
            dialogueNumber++;
        }

        //muda o nome 
        DialogueTextBoxName.text = dialogueList[dialogueNumber].dialogueList[sentenceNumber].Speaker.name;

        //muda o que vai ser escrito
        theSentence = dialogueList[dialogueNumber].dialogueList[sentenceNumber].Sentence;

        //começa a escrever em IEnumerator
        StartCoroutine(write());
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
            DialogueTextBox.text = theSentence.Substring(0, i);

            if (i == theSentence.Length)
            {
                //quando acaba a sentence
                isTalking = false;
                sentenceNumber++;
            }

            //para o IEnumerator
            StopCoroutine(write());

            //tempo para proimo loop
            yield return new WaitForSecondsRealtime(sentenceDelay);
        }
    }

    IEnumerator Write()
    {
        isTalking = true;
        DialogueTextBox.text = ""; // Clear the text box initially

        for (int i = 0; i < theSentence.Length; i++)
        {
            DialogueTextBox.text += theSentence[i]; // Append the next character
            yield return new WaitForSecondsRealtime(sentenceDelay);
        }

        // Sentence finished
        isTalking = false;
        sentenceNumber++;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    public void WriteText(string text)
    {
        if (isTalking) return; // Prevent multiple coroutines from running simultaneously

        theSentence = text;
        StartCoroutine(Write());
    }

    public void StopWriting()
    {
        StopAllCoroutines();
        isTalking = false; // Reset state
    }
}
