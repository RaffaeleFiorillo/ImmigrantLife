using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    /// <summary>
    /// Flag que indica que uma frase est� sendo escrita na Dialogue Box.
    /// </summary>
    public bool IsWritingSentence { get; set; }

    /// <summary>
    /// Indice da frase atual do di�logo.
    /// Ao alterar este valor altera-se automaticamente a frase e o Personagem que diz esta frase.
    /// </summary>
    private int CurrentSentenceIndex { get; set; } = 0;

    /// <summary>
    /// O Evento Narrativo que representa o di�logo. � passado pelo EventManager
    /// </summary>
    private DialogueEvent CurrentDialogueEvent { get; set; }

    /// <summary>
    /// Frase atual a ser dita do di�logo.
    /// � atualizado automaticamente ao alterar o <see cref="CurrentSentenceIndex"/>
    /// </summary>
    private string CurrentSentence { get=> CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].Sentence; }

    /// <summary>
    /// Nome da personagem que diz a frase atualmente sendo escrita.
    /// � atualizado automaticamente ao alterar o <see cref="CurrentSentenceIndex"/>
    /// </summary>
    private string CurrentSpeakerName { get => CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].Speaker.name; }

    #endregion Propriedades

    void Start()
    {
        SetCharacterSpeed(setToNormalSpeed:true);
    }

    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        CurrentDialogueEvent = (DialogueEvent)narrativeEvent;  // atribuiri o evento narrativo de di�logo a ser tratado.
        CurrentSentenceIndex = 0; // indicar que deve-se iniciar da primeira frase.

        // Iniciar automaticamente o tratamento da primeira frases do d�alogo
        // O restante das frases � mostrado quando o jogador clicar no devido bot�o 

        DialogBox.SetActive(true);
        GoToNextSentence();
    }

    private void GoToNextNarrativeEvent()
    {
        DialogBox.SetActive(false);
        EventManager.CurrentNarrativeEvent = CurrentDialogueEvent.NextEvent;
        EventManager.ManageCurrentEvent();
    }

    public void GoToNextSentence()
    {
        // se j� estiver falando, acelera-se o texto
        if (IsWritingSentence)
        {
            SetCharacterSpeed(setToNormalSpeed:false);
            return;
        }

        if (CurrentDialogueEvent == null)
            return;

        if (CurrentDialogueEvent.DialogueBlocks.Count == CurrentSentenceIndex)
        {
            Invoke("GoToNextNarrativeEvent", 0.1f);
            return;
        }

        DialogueTextBoxName.text = CurrentSpeakerName;
        SetCharacterSpeed(true);
        StartCoroutine(WriteSentence());  // come�a a escrever a frase
    }

    /// <summary>
    /// Escrever a atual frase do Dialogo na Caixa de Texto.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WriteSentence()
    {
        IsWritingSentence = true;
        DialogueTextBox.text = ""; // Clear the text box initially

        for (int i = 0; i < CurrentSentence.Length; i++)
        {
            DialogueTextBox.text += CurrentSentence[i]; // Adicionar os caracteres aos poucos (nesse caso um a um)
            yield return new WaitForSecondsRealtime(CharacterDelaySpeed);
        }

        // A frase terminou de ser escrita
        IsWritingSentence = false;
        CurrentSentenceIndex++;  // passar � frase seguinte
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
