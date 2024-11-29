using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(EventManager))]
public class DialogueManager : MonoBehaviour
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
  /*  [SerializeField]
    List<scriptableDialogue> dialogueList = new List<scriptableDialogue>();
  */

    scriptableDialogue storedDialog;


    /// <summary>
    /// QUANDO A VELOCIDADE É NORMAL: Este é o intervalo de tempo entre um caracter ser mostrado e o próximo, durante a fase de escrita de uma frase.
    /// A unidade de medida é Milissegundos.
    /// </summary>
    [SerializeField]
    [Range(0, 10)]
    float NormalCharacterDisplaySpeed;

    /// <summary>
    /// QUANDO A VELOCIDADE É RÁPIDA: Este é o intervalo de tempo entre um caracter ser mostrado e o próximo, durante a fase de escrita de uma frase.
    /// A unidade de medida é Milissegundos.
    /// </summary>
    [SerializeField]
    [Range(0, 10)]
    float FastCharacterDisplaySpeed;

    #endregion Campos Serializados

    #region Propriedades

    //velocidade de texto
    private float sentenceDelay { get; set; } = 0.01f;

    /// <summary>
    /// O intervalo de tempo entre um caracter ser mostrado e o próximo, durante a fase de escrita de uma frase.
    /// A unidade de medida é Segundos.
    /// </summary>
    private float CharacterDelaySpeed { get; set; }

    /// <summary>
    /// Flag que indica que uma frase está sendo escrita na Dialogue Box.
    /// </summary>
    public bool IsWritingSentence { get; set; }

    /// <summary>
    /// Indice da frase que está sendo escrita na Dialogue Box.
    /// </summary>
    int SentenceIndex { get; set; } = 0;

    /// <summary>
    /// Indíce do diálogo
    /// </summary>
    int DialogueIndex { get; set; } = 0;

    string TheSentence ;
//{ get=>    dialogueList[DialogueIndex].dialogueList[SentenceIndex].Sentence;     }

    #endregion Propriedades


    EventManager theEventManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theEventManager = GetComponent<EventManager>();


        SetCharacterSpeed(setToNormalSpeed:true);
    }


    //trigger de dialogo 
    //ao clicar uma segunda vez aumenta a velocidade 

    public void receiveDialogue(scriptableDialogue theDialogue)
    {
        storedDialog = theDialogue;
        StartDialogue();


    }

    public void StartDialogue()
    {



        // se já estiver falando, acelera-se o texto
        if (IsWritingSentence)
        {
            SetCharacterSpeed(setToNormalSpeed:false);
            return;
        }
        if (storedDialog != null)
        {

        if (storedDialog.dialogueList.Count == SentenceIndex)
        {
            //da reset às sentences
            SentenceIndex = 0;

            //altera o dialogo
            //DialogueIndex++;

            theEventManager.endDialog();
                storedDialog = null;

                return;
        }
            SetCharacterSpeed(true);
        TheSentence = storedDialog.dialogueList[SentenceIndex].Sentence;
        //muda o nome 
        DialogueTextBoxName.text = storedDialog.dialogueList[SentenceIndex].Speaker.name;

        //muda o que vai ser escrito

        //começa a escrever em IEnumerator
        StartCoroutine(Write());
        }
    }


    /// <summary>
    /// Método para alterar a velocidade de display dos caracteres;
    /// </summary>
    /// <param name="setToNormalSpeed"></param>
    public void SetCharacterSpeed(bool setToNormalSpeed=true)
    {
        CharacterDelaySpeed = (setToNormalSpeed ? NormalCharacterDisplaySpeed : FastCharacterDisplaySpeed)*0.01f;
    }

    IEnumerator Write()
    {
        IsWritingSentence = true;
        DialogueTextBox.text = ""; // Clear the text box initially

        for (int i = 0; i < TheSentence.Length; i++)
        {
            DialogueTextBox.text += TheSentence[i]; // Append the next character
            yield return new WaitForSecondsRealtime(CharacterDelaySpeed);
        }

        // A frase terminou de ser escrita
        IsWritingSentence = false;
        SentenceIndex++;
        // StopCoroutine(Write());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    public void WriteText(string text)
    {
        if (IsWritingSentence) return; // Prevent multiple coroutines from running simultaneously

     //   TheSentence = text;
        StartCoroutine(Write());
    }

    public void StopWriting()
    {
        StopAllCoroutines();
        IsWritingSentence = false; // Reset state
    }
}
