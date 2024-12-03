using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceManager : BaseNarrativeEventManager
{
    private ChoiceEvent CurrentChoiceEvent { get; set; }

    [SerializeField] 
    GameObject ButtonBox;

    [SerializeField] 
    GameObject ChoicePrefab;

    //nova box para a question
    [SerializeField] GameObject QuestionBox;
    TextMeshProUGUI QuestionText;

<<<<<<< Updated upstream
=======

    List<GameObject> ChoiceButtons = new List<GameObject>();
    private void Start()
    {
        //chamar a text box dentro da caixa
        QuestionText=QuestionBox.GetComponentInChildren<TextMeshProUGUI>();
    }
    private Vector2 GetTextPreferredSize(TextMeshProUGUI textComponent)
    {
        textComponent.ForceMeshUpdate(); // Ensure the text mesh is up-to-date
        var textBounds = textComponent.textBounds;
        return new Vector2(textBounds.size.x, textBounds.size.y);
    }

>>>>>>> Stashed changes
    /// <summary>
    /// Este método gere um evento narrativo de escolha. Disponibiliza as escolhas ao jogador.
    /// </summary>
    /// <param name="narrativeEvent">O evento narrativo que representa uma escolha a ser tomada.</param>
    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        CurrentChoiceEvent = (ChoiceEvent)narrativeEvent;

        //liga a question box
        QuestionBox.SetActive(true);
        QuestionText.text = CurrentChoiceEvent.question;
        for(int i = 0; i<CurrentChoiceEvent.Choices.Count; i++)
        {
            int cIndex = i;  // atribuição necessária para que o valor do índice seja o correto quando for usado na lambda function.

            // Criar um botão para a escolha
            GameObject choiceButton = Instantiate(ChoicePrefab, ButtonBox.transform);
            ChoiceButtons.Add(choiceButton);
            choiceButton.GetComponent<Button>().onClick.AddListener(() => ApplyChoiceEffects(cIndex));  // Funcionamento do botão
            choiceButton.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = CurrentChoiceEvent.Choices[cIndex].Description; // adicionar o texto do botão
        }
    }

    /// <summary>
    /// Método chamado quando o jogador seleciona uma das escolhas disponibilizadas.
    /// </summary>
    /// <param name="playerChoiceIndex">Indice da escolha feita, dentro do da lista de escolhas possíveis.</param>
    public void ApplyChoiceEffects(int playerChoiceIndex)
    {
        Choice playerChoice = CurrentChoiceEvent.Choices[playerChoiceIndex];

        // Aplicar os efeitos da escolha às estatisticas do Personagem
        QuestionBox.SetActive(false) ;
        // Destruir os elementos de UI relativamente a este Evento Narrativo
        foreach (GameObject choiceButton in ChoiceButtons)
            Destroy(choiceButton);
        ChoiceButtons.Clear(); // Esvaziar a lista para usos futuros

        // Passar ao evento seguinte
        EventManager.CurrentNarrativeEvent = playerChoice.NextEvent;

        //indica quansdo a funçao ja acabou
        EventManager.changeEvent();
    }
}
