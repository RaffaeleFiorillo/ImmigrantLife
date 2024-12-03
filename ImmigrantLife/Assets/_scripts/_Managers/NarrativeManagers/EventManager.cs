using UnityEngine;
using UnityEngine.UI;


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

    //onde irá haver o output da musica
    [SerializeField]  AudioSource musicPlayer;
    /// <summary>
    /// Imagem apresentada no Background durante o jogo.
    /// </summary>
    [SerializeField]
    private Image BackgroundImage;

    //bool para saber quando um evento é ativo
    private bool eventOccuring { get; set; }


    /// <summary>
    /// No inicio, são obtidas as referencias para os managers.
    /// </summary>
    /// 

    void Start()
    {
        BaseNarrativeEventManager.GetEventManagerReference(this);
        DialogueManager = GetComponent<DialogueManager>();
        ChoiceManager = GetComponent<ChoiceManager>();
        ManageCurrentEvent();
    }

    /// <summary>
    /// Alterar a imagem de background apresentada no jogo.
    /// </summary>
    /// <param name="newBackgroundImage"></param>
    public void ChangeBackGround(Sprite newBackgroundSprite)
    {
        if (newBackgroundSprite != null)
            BackgroundImage.sprite = newBackgroundSprite;
    }

    public void changeEventOccurence()
    {

        eventOccuring = false;

    }

    private void Update()
    {
        if (eventOccuring)
            return;

        eventOccuring = true;
        ManageCurrentEvent();
    }
    public void ManageCurrentEvent()
    {
        if (CurrentNarrativeEvent == null)
        {
            // Colcar aqui a lógica de quando chegar ao fim da execução dos eventos narrativos.
            return;
        }
        if(CurrentNarrativeEvent.musica != null) {

        musicPlayer.Stop();
            musicPlayer.resource = CurrentNarrativeEvent.musica;
            musicPlayer.Play();
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
