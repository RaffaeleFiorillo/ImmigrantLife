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

    [SerializeField]
    GameObject questionBox;

    TextMeshProUGUI questionText;

    List<GameObject> ChoiceButtons = new List<GameObject>();

  
    private void Start()
    {
        questionText = questionBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    private Vector2 GetTextPreferredSize(TextMeshProUGUI textComponent)
    {
        textComponent.ForceMeshUpdate(); // Ensure the text mesh is up-to-date
        var textBounds = textComponent.textBounds;

        return new Vector2(textBounds.size.x, textBounds.size.y);
    }

    /// <summary>
    /// Este m�todo gere um evento narrativo de escolha. Disponibiliza as escolhas ao jogador.
    /// </summary>
    /// <param name="narrativeEvent">O evento narrativo que representa uma escolha a ser tomada.</param>
    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        CurrentChoiceEvent = (ChoiceEvent)narrativeEvent;
        questionBox.SetActive(true);
        questionText.text = CurrentChoiceEvent.Question;


        for (int i = 0; i < CurrentChoiceEvent.Choices.Count; i++)
        {
            int cIndex = i; // atribui��o necess�ria para que o valor do �ndice seja o correto quando for usado na lambda function.

            // Criar um bot�o para a escolha
            GameObject choiceButton = Instantiate(ChoicePrefab, ButtonBox.transform);
            ChoiceButtons.Add(choiceButton);

            // Set up the button's functionality and text
            Button buttonComponent = choiceButton.transform.GetChild(0).GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => ApplyChoiceEffects(cIndex)); // Funcionamento do bot�o
            TextMeshProUGUI buttonText = choiceButton.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = CurrentChoiceEvent.Choices[cIndex].Description; // adicionar o texto do bot�o

            // Adjust the button size based on the text length
            //RectTransform buttonRectTransform = choiceButton.GetComponent<RectTransform>();
            //Vector2 preferredSize = GetTextPreferredSize(buttonText);
            //buttonRectTransform.sizeDelta = new Vector2(preferredSize.x + 20f, buttonRectTransform.sizeDelta.y); // Add padding to width
        }
    }

    /// <summary>
    /// M�todo chamado quando o jogador seleciona uma das escolhas disponibilizadas.
    /// </summary>
    /// <param name="playerChoiceIndex">Indice da escolha feita, dentro do da lista de escolhas poss�veis.</param>
    public void ApplyChoiceEffects(int playerChoiceIndex)
    {
        Choice playerChoice = CurrentChoiceEvent.Choices[playerChoiceIndex];

        // Aplicar os efeitos da escolha �s estatisticas do Personagem

        // Destruir os elementos de UI relativamente a este Evento Narrativo
        foreach (GameObject choiceButton in ChoiceButtons)
            Destroy(choiceButton);
        ChoiceButtons.Clear(); // Esvaziar a lista para usos futuros

        questionBox.SetActive(false);

        // Passar ao evento seguinte
        EventManager.CurrentNarrativeEvent = playerChoice.NextEvent;
        EventManager.CurrentNarrativeEvent.HasBeenManaged = true;
    }
}
