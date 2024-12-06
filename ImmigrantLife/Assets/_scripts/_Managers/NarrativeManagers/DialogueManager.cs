using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : BaseNarrativeEventManager
{
    #region Campos Serializados

    /// <summary>
    /// Parte do UI onde é mostrado o texto dos dialogos.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI DialogueTextBox;

    [SerializeField]
    GameObject toPassText;

    /// <summary>
    /// Nome da Caixa de texto de dialogo. Aparece no UI, em cima do <see cref="DialogueTextBox"/>.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI DialogueTextBoxName;

    [SerializeField] 
    GameObject DialogBox;

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

    /// <summary>
    /// O intervalo de tempo entre um caracter ser mostrado e o próximo, durante a fase de escrita de uma frase.
    /// A unidade de medida é Segundos.
    /// </summary>
    private float CharacterDelaySpeed { get; set; }

    private float TimeWaited { get; set; }

    /// <summary>
    /// Flag que indica que uma frase está sendo escrita na Dialogue Box.
    /// </summary>
    public bool IsWritingSentence { get; set; }

    /// <summary>
    /// Indice da frase atual do diálogo.
    /// Ao alterar este valor altera-se automaticamente a frase e o Personagem que diz esta frase.
    /// </summary>
    private int CurrentSentenceIndex { get; set; } = 0;

    private int CurrentSentenceCharacterIndex { get; set; }

    /// <summary>
    /// O Evento Narrativo que representa o diálogo. É passado pelo EventManager
    /// </summary>
    private DialogueEvent CurrentDialogueEvent { get; set; }

    /// <summary>
    /// Frase atual a ser dita do diálogo.
    /// É atualizado automaticamente ao alterar o <see cref="CurrentSentenceIndex"/>
    /// </summary>
    private string CurrentSentence { get => CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].Sentence;  }

    /// <summary>
    /// Nome da personagem que diz a frase atualmente sendo escrita.
    /// É atualizado automaticamente ao alterar o <see cref="CurrentSentenceIndex"/>
    /// </summary>
    private string CurrentSpeakerName { get => CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].Speaker.name; }

    #endregion Propriedades


    [SerializeField] GameObject skipIndicator;

    //onde irá haver o output dos sons
    [SerializeField] 
    AudioSource soundPlayer;

    void Start()
    {
        SetCharacterSpeed(setToNormalSpeed:true);
    }

    private void Update()
    {
        if (!IsWritingSentence)
            return;
        timer();
        
    }


    private void timer()
    {
        if (CurrentSentenceCharacterIndex == CurrentSentence.Length)
        {
            IsWritingSentence = false;
            CurrentSentenceCharacterIndex = 0;
            CurrentSentenceIndex++;
            skipIndicator.SetActive(true);
            toPassText.SetActive(true);
            return;
        }

        TimeWaited += Time.deltaTime;
        if (TimeWaited >= CharacterDelaySpeed)
        {
            TimeWaited = 0;
            DialogueTextBox.text += CurrentSentence[CurrentSentenceCharacterIndex];
            CurrentSentenceCharacterIndex++;
           
        }
    }

    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        CurrentDialogueEvent = (DialogueEvent)narrativeEvent;  // atribuiri o evento narrativo de diálogo a ser tratado.
        CurrentSentenceIndex = 0; // indicar que deve-se iniciar da primeira frase.
        CurrentSentenceCharacterIndex = 0;

        // Iniciar automaticamente o tratamento da primeira frases do díalogo
        // O restante das frases é mostrado quando o jogador clicar no devido botão 

        DialogBox.SetActive(true);

        GoToNextSentence();
    }

    private void GoToNextNarrativeEvent()
    {    
        DialogBox.SetActive(false);
        EventManager.CurrentNarrativeEvent = CurrentDialogueEvent.NextEvent;
        //  EventManager.ManageCurrentEvent();

        // EventManager.ChangeEventOccurence();
        DialogBox.SetActive(false);
        EventManager.CurrentNarrativeEvent = CurrentDialogueEvent.NextEvent;
        // EventManager.changeEvent();
    }

    public void GoToNextSentence()
    {
        if (CurrentDialogueEvent == null)
            return;

        // se já estiver falando, acelera-se o texto
        if (IsWritingSentence)
        {
            // SetCharacterSpeed(setToNormalSpeed:false);
            DialogueTextBox.text = ""; // Clear the text box initially
            DialogueTextBox.text = CurrentSentence;
            IsWritingSentence = false;
            CurrentSentenceCharacterIndex = 0;
            CurrentSentenceIndex++;
            skipIndicator.SetActive(true);
        }

        if (CurrentDialogueEvent.DialogueBlocks.Count == CurrentSentenceIndex)
        {
            Invoke("GoToNextNarrativeEvent", 0.1f);
            return;
        }

        //altera o background(eu sei que aqui n é o melhor sitio mas ya )
        skipIndicator.SetActive(false);


        //implementação do som
        if (CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].som != null)
        {  
            soundPlayer.resource = CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].som;
            soundPlayer.Play();
        }

        DialogueTextBox.text = "";
        EventManager.ChangeBackGround(CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].BackgroundImage);

        //altera o background(eu sei que aqui n é o melhor sitio mas ya )
        toPassText.SetActive(false);
        DialogueTextBox.text = "";
        EventManager.ChangeBackGround(CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].BackgroundImage);

        DialogueTextBoxName.text = CurrentSpeakerName;
        SetCharacterSpeed(true);
        IsWritingSentence = true;
        // StartCoroutine(WriteSentence());  // começa a escrever a frase
    }

    /// <summary>
    /// Método para alterar a velocidade de display dos caracteres;
    /// </summary>
    /// <param name="setToNormalSpeed"></param>
    public void SetCharacterSpeed(bool setToNormalSpeed = true)
    {
        CharacterDelaySpeed = (setToNormalSpeed ? NormalCharacterDisplaySpeed : FastCharacterDisplaySpeed) * 0.01f;
    }
}
