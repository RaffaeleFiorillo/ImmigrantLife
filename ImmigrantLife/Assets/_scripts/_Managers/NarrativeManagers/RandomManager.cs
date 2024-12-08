using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomManager : BaseNarrativeEventManager
{
    private RandomEvent CurrentRandomEvent { get; set; }

    [SerializeField] 
    GameObject InteractableSpaceBox;

    [SerializeField] 
    GameObject ChoicePrefab;

    [SerializeField] 
    GameObject SetupDescriptionBox;

    public TextMeshProUGUI SetupDescriptionText;

    public  List<GameObject> PossibleEvents = new List<GameObject>();


    private void Start()
    {
        SetupDescriptionText = SetupDescriptionText.GetComponentInChildren<TextMeshProUGUI>();
    }

    private Vector2 GetTextPreferredSize(TextMeshProUGUI textComponent)
    {
        textComponent.ForceMeshUpdate(); // Ensure the text mesh is up-to-date
        var textBounds = textComponent.textBounds;

        return new Vector2(textBounds.size.x, textBounds.size.y);
    }

    private void StartBranchEvent(NarrativeEvent narrativeEvent)
    {
        var CurrentBranchEvent = (BranchEvent)narrativeEvent;
        SetupDescriptionBox.SetActive(true);

        SetupDescriptionText.text = CurrentRandomEvent.Description;
        // Calculate the total weight (sum of probabilities)
        double totalWeight = 0;
        foreach (var randomEvent in CurrentBranchEvent.PossibleRandomEvents)
        {
            totalWeight += randomEvent.Probability;
        }
    }

    private void StartRandomEvent(NarrativeEvent narrativeEvent)
    {
        CurrentRandomEvent = (RandomEvent)narrativeEvent;
        SetupDescriptionBox.SetActive(true);
        SetupDescriptionText.text = CurrentRandomEvent.Description;


        double randomDouble = Random.Range(0, 100); // Generates a random double between 0.0 and 1.0

        if(randomDouble > CurrentRandomEvent.Probability)
        {
            // Passar ao evento seguinte
            // EventManager.CurrentNarrativeEvent = CurrentNarrativeEvent.NextEvent;
            EventManager.CurrentNarrativeEvent.HasBeenManaged = true;
            return;
        }


    }

    /// <summary>
    /// Este método gere um evento narrativo de escolha. Disponibiliza as escolhas ao jogador.
    /// </summary>
    /// <param name="narrativeEvent">O evento narrativo que representa uma escolha a ser tomada.</param>
    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        if (narrativeEvent.Type == EventType.Random)
            // Gerir o evento narrativo de acordo com a sua tipologia
            switch (narrativeEvent.Type)
            {
                case EventType.Random:
                        StartRandomEvent(narrativeEvent);
                        break;
                case EventType.Choice:
                        StartBranchEvent(narrativeEvent);
                        break;
                default:
                    throw new System.Exception($"Tipo desconhecido: {narrativeEvent.Type}");
            }
    }

    /// <summary>
    /// Método chamado quando o jogador seleciona uma das escolhas disponibilizadas.
    /// </summary>
    /// <param name="playerChoiceIndex">Indice da escolha feita, dentro do da lista de escolhas possíveis.</param>
    public void ApplyChoiceEffects(int playerChoiceIndex)
    {
        // Choice playerChoice = CurrentRandomEvent.Choices[playerChoiceIndex];

        // Aplicar os efeitos da escolha às estatisticas do Personagem

        // Destruir os elementos de UI relativamente a este Evento Narrativo
        foreach (GameObject choiceButton in PossibleEvents)
            Destroy(choiceButton);
        PossibleEvents.Clear(); // Esvaziar a lista para usos futuros

        InteractableSpaceBox.SetActive(false);

        // Passar ao evento seguinte
        // EventManager.CurrentNarrativeEvent = playerChoice.NextEvent;
        EventManager.CurrentNarrativeEvent.HasBeenManaged = true;
    }
}
