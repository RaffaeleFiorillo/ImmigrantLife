using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : BaseNarrativeEventManager
{
    #region Campos Serializados
  
    /// <summary>
    /// Parte do UI onde � mostrado o texto dos dialogos.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI DialogueTextBox;

    /// <summary>
    /// Nome da Caixa de texto de dialogo. Aparece no UI, em cima do <see cref="DialogueTextBox"/>.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI DialogueTextBoxName;



    [SerializeField] 
    GameObject DialogBox;

    /// <summary>
    /// QUANDO A VELOCIDADE � NORMAL: Este � o intervalo de tempo entre um caracter ser mostrado e o pr�ximo, durante a fase de escrita de uma frase.
    /// A unidade de medida � Milissegundos.
    /// </summary>
    [SerializeField]
    [Range(0, 10)]
    float NormalCharacterDisplaySpeed;

    /// <summary>
    /// QUANDO A VELOCIDADE � R�PIDA: Este � o intervalo de tempo entre um caracter ser mostrado e o pr�ximo, durante a fase de escrita de uma frase.
    /// A unidade de medida � Milissegundos.
    /// </summary>
    [SerializeField]
    [Range(0, 10)]
    float FastCharacterDisplaySpeed;

    #endregion Campos Serializados

    #region Propriedades

    /// <summary>
    /// O intervalo de tempo entre um caracter ser mostrado e o pr�ximo, durante a fase de escrita de uma frase.
    /// A unidade de medida � Segundos.
    /// </summary>
    private float CharacterDelaySpeed { get; set; }

    private float TimeWaited { get; set; }

    /// <summary>
    /// Flag que indica que uma frase est� sendo escrita na Dialogue Box.
    /// </summary>
    public bool IsWritingSentence { get; set; }

    /// <summary>
    /// Indice da frase atual do di�logo.
    /// Ao alterar este valor altera-se automaticamente a frase e o Personagem que diz esta frase.
    /// </summary>
    private int CurrentSentenceIndex { get; set; } = 0;

    private int CurrentSentenceCharacterIndex { get; set; }

    /// <summary>
    /// O Evento Narrativo que representa o di�logo. � passado pelo EventManager
    /// </summary>
    private DialogueEvent CurrentDialogueEvent { get; set; }

    /// <summary>
    /// Bloco de dialogo atualmente a ser tratado
    /// </summary>
    private DialogueBlock effect { get => CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex]; }

    /// <summary>
    /// Frase atual a ser dita do di�logo.
    /// � atualizado automaticamente ao alterar o <see cref="CurrentSentenceIndex"/>
    /// </summary>
    private string CurrentSentence { get => effect.Sentence;  }

    /// <summary>
    /// Nome da personagem que diz a frase atualmente sendo escrita.
    /// � atualizado automaticamente ao alterar o <see cref="CurrentSentenceIndex"/>
    /// </summary>
    private string CurrentSpeakerName { get => effect.Speaker.speakerName; }

    #endregion Propriedades

    [SerializeField] 
    GameObject skipIndicator;

    //onde ir� haver o output dos sons
    [SerializeField] AudioSource soundPlayer;
    void Start()
    {
        SetCharacterSpeed(setToNormalSpeed:true);     
    }

    private void Update()
    {
        if (!IsWritingSentence)
            return;

        if (CurrentSentenceCharacterIndex == CurrentSentence.Length)
        {
            IsWritingSentence = false;
            CurrentSentenceCharacterIndex = 0;
            CurrentSentenceIndex++;
            skipIndicator.SetActive(true);
            return;
        }

        TimeWaited += Time.deltaTime;
        if( TimeWaited >= CharacterDelaySpeed)
        {
           
            TimeWaited = 0;
            DialogueTextBox.maxVisibleCharacters = CurrentSentenceCharacterIndex+1;
            CurrentSentenceCharacterIndex++;
        }
    }

    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        CurrentDialogueEvent = (DialogueEvent)narrativeEvent;  // atribuiri o evento narrativo de di�logo a ser tratado.
        CurrentSentenceIndex = 0; // indicar que deve-se iniciar da primeira frase.
        CurrentSentenceCharacterIndex = 0;

        // Iniciar automaticamente o tratamento da primeira frases do d�alogo
        // O restante das frases � mostrado quando o jogador clicar no devido bot�o 

        DialogBox.SetActive(true);
    
        GoToNextSentence();
    }

    private void GoToNextNarrativeEvent()
    {     
        DialogBox.SetActive(false);

        if (CurrentDialogueEvent.NextEvent == null)
            return ;

        EventManager.FHasBeenManaged();
    }

    public void GoToNextSentence()
    {
        if (CurrentDialogueEvent == null)
            return;

        // se j� estiver falando, acelera-se o texto
        if (IsWritingSentence)
        {
            // SetCharacterSpeed(setToNormalSpeed:false);
            DialogueTextBox.maxVisibleCharacters = CurrentSentence.Length + 1;
            IsWritingSentence = false;
            CurrentSentenceCharacterIndex = 0;

            CurrentSentenceIndex++;
          
            skipIndicator.SetActive(true);
            return;
        }

        if (CurrentDialogueEvent.DialogueBlocks.Count == CurrentSentenceIndex)
        {
            Invoke("GoToNextNarrativeEvent", 0.1f);
            return;
        }

        //altera o background(eu sei que aqui n � o melhor sitio mas ya )
        skipIndicator.SetActive(false);

        //implementa��o do som
        if (effect.som != null)
        {      
            soundPlayer.resource = effect.som;
            soundPlayer.Play();
        }

        if (effect.CharacterIsAlone)
            EffectManager.RemoveCharacter();

        DialogueTextBox.text = "";
      
        EventManager.ChangeBackGround(effect.BackgroundImage);

        DialogBox.SetActive(true);

        //   Debug.Log(CurrentDialogueBlock.Speaker + "" + CurrentDialogueBlock.positionIndex + "" + CurrentDialogueBlock.emotionIndex);
        if (effect.emotionIndex != 0)
            EffectManager.reiceiveCharacter(effect.Speaker, effect.positionIndex, effect.emotionIndex);
        else
            EffectManager.RemoveAllCharacters();
        // Desligar o texto do Speaker anterior
       
        DialogueTextBoxName.text = CurrentSpeakerName;
        
        SetCharacterSpeed(true);

        DialogueTextBox.text = CurrentSentence;
        DialogueTextBox.maxVisibleCharacters = 0;
        
        IsWritingSentence = true;
    }

    /// <summary>
    /// M�todo para alterar a velocidade de display dos caracteres;
    /// </summary>
    /// <param name="setToNormalSpeed"></param>
    public void SetCharacterSpeed(bool setToNormalSpeed = true)
    {
        CharacterDelaySpeed = (setToNormalSpeed ? NormalCharacterDisplaySpeed : FastCharacterDisplaySpeed) * 0.01f;
    }
}
