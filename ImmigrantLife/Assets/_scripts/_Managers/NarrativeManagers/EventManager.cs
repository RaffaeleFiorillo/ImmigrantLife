using UnityEngine;


/// <summary>
/// Esta classe é responsável por gerir a sequência de eventos que definem o desenrolar da narrativa. 
/// </summary>
[RequireComponent (typeof(DialogueManager))]
public class EventManager : MonoBehaviour
{
    #region Managers

    /// <summary>
    /// Componente responsável por gerir a apresentação de diálogos.
    /// </summary>
    private DialogueManager DialogueManager { get; set; }

    /// <summary>
    /// Componente responsável por gerir a apresentação de diálogos.
    /// </summary>
    private ChoiceManager ChoiceManager { get; set; }

    #endregion Managers

    /// <summary>
    /// Evento Narrativo atual.
    /// </summary>
    [SerializeField]
    NarrativeEvent CurrentNarrativeEvent;

    /// <summary>
    /// No inicio, são obtidas as referencias para os managers.
    /// </summary>
    void Start()
    {
        BaseNarrativeEventManager.GetEventManagerReference(this);
        DialogueManager = GetComponent<DialogueManager>();
        ChoiceManager = GetComponent<ChoiceManager>();
        ManageCurrentEvent();
    }

    public void ManageCurrentEvent()
    {
        if (CurrentNarrativeEvent == null)
        {
            // Colcar aqui a lógica de quando chegar ao fim da execução dos eventos narrativos.
            return;
        }

        // Gerir o evento narrativo de acordo com a sua tipologia
        switch (CurrentNarrativeEvent.Type)
        {
            case EventType.Dialogue:
            {
                DialogueManager.StartNarrativeEvent(CurrentNarrativeEvent);
                break;
            }
            case EventType.Choice:
            {
                ChoiceManager.StartNarrativeEvent(CurrentNarrativeEvent);
                break;
            }
        }
    }
}
