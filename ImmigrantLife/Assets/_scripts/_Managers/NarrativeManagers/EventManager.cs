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
    public NarrativeEvent CurrentNarrativeEvent;

    /// <summary>
<<<<<<< Updated upstream
    /// No inicio, são obtidas as referencias para os managers.
    /// </summary>
=======
    /// Imagem apresentada no Background durante o jogo.
    /// </summary>
    [SerializeField]
    private Image BackgroundImage;


    bool eventOccurring;
        /// <summary>
        /// No inicio, são obtidas as referencias para os managers.
        /// </summary>
>>>>>>> Stashed changes
    void Start()
    {
        BaseNarrativeEventManager.GetEventManagerReference(this);
        DialogueManager = GetComponent<DialogueManager>();
        ChoiceManager = GetComponent<ChoiceManager>();
        ManageCurrentEvent();
    }

<<<<<<< Updated upstream
=======
    /// <summary>
    /// Alterar a imagem de background apresentada no jogo.
    /// </summary>
    /// <param name="newBackgroundImage"></param>
    public void ChangeBackGround(Sprite newBackgroundSprite)
    {
        if (newBackgroundSprite != null)
            BackgroundImage.sprite = newBackgroundSprite;
    }
    private void Update()
    {
        if (eventOccurring)
            return;
        //coloquei aqui pq assim ele ativa quando a bool ta off e n da aquele problema
        ManageCurrentEvent();
    }
    public void changeEvent()
    {//quando ativada religa a bool 
        eventOccurring = false;



    }
>>>>>>> Stashed changes
    public void ManageCurrentEvent()
    {
        if (CurrentNarrativeEvent == null)
        {
            // Colcar aqui a lógica de quando chegar ao fim da execução dos eventos narrativos.
            return;
        }

        // Gerir o evento narrativo de acordo com a sua tipologia

        
        //ativa a bool para desligar multiplos loops
        eventOccurring = true;
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
