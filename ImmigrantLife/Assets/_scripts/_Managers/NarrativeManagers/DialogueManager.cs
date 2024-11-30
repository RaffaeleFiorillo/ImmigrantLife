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

    /// <summary>
    /// Nome da Caixa de texto de dialogo. Aparece no UI, em cima do <see cref="DialogueTextBox"/>.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI DialogueTextBoxName;

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

    /// <summary>
    /// Flag que indica que uma frase está sendo escrita na Dialogue Box.
    /// </summary>
    public bool IsWritingSentence { get; set; }

    /// <summary>
    /// Indice da frase atual do diálogo.
    /// Ao alterar este valor altera-se automaticamente a frase e o Personagem que diz esta frase.
    /// </summary>
    private int CurrentSentenceIndex { get; set; } = 0;

    /// <summary>
    /// O Evento Narrativo que representa o diálogo. É passado pelo EventManager
    /// </summary>
    private DialogueEvent CurrentDialogueEvent { get; set; }

    /// <summary>
    /// Frase atual a ser dita do diálogo.
    /// É atualizado automaticamente ao alterar o <see cref="CurrentSentenceIndex"/>
    /// </summary>
    private string CurrentSentence { get=> CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].Sentence; }

    /// <summary>
    /// Nome da personagem que diz a frase atualmente sendo escrita.
    /// É atualizado automaticamente ao alterar o <see cref="CurrentSentenceIndex"/>
    /// </summary>
    private string CurrentSpeakerName { get => CurrentDialogueEvent.DialogueBlocks[CurrentSentenceIndex].Speaker.name; }

    #endregion Propriedades

    void Start()
    {
        SetCharacterSpeed(setToNormalSpeed:true);
    }

    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        CurrentDialogueEvent = (DialogueEvent)narrativeEvent;  // atribuiri o evento narrativo de diálogo a ser tratado.
        CurrentSentenceIndex = 0; // indicar que deve-se iniciar da primeira frase.
        // Iniciar automaticamente o tratamento da primeira frases do díalogo
        // O restante das frases é mostrado quando o jogador clicar no devido botão 
        GoToNextSentence();
    }

    public void GoToNextSentence()
    {
        // se já estiver falando, acelera-se o texto
        if (IsWritingSentence)
        {
            SetCharacterSpeed(setToNormalSpeed:false);
            return;
        }

        if (CurrentDialogueEvent == null)
            return;

        if (CurrentDialogueEvent.DialogueBlocks.Count == CurrentSentenceIndex)
        {
            return;
        }
        DialogueTextBox.name = CurrentSpeakerName;
        SetCharacterSpeed(true);
        StartCoroutine(WriteSentence());  // começa a escrever a frase
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
        CurrentSentenceIndex++;  // passar à frase seguinte
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
