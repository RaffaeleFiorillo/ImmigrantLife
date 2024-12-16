using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


/// <summary>
/// Esta classe � respons�vel por gerir a sequ�ncia de eventos que definem o desenrolar da narrativa. 
/// </summary>
[RequireComponent (typeof(DialogueManager))]
public class EventManager : MonoBehaviour
{
    #region Managers

    /// <summary>
    /// Componente respons�vel por gerir a apresenta��o de di�logos.
    /// </summary>
    private DialogueManager DialogueManager { get; set; }

    /// <summary>
    /// Componente respons�vel por gerir a apresenta��o de di�logos.
    /// </summary>
    private ChoiceManager ChoiceManager { get; set; }


    /// <summary>
    /// Componente respons�vel por gerir a apresenta��o de Eventos Random.
    /// </summary>
    private RandomManager RandomManager { get; set; }


    private BackgroundManager BackgroundManager { get; set; }

    private EffectManager EffectManager { get; set; }

    #endregion Managers
    
    #region Propriedades

    /// <summary>
    /// Evento Narrativo atual.
    /// </summary>
    [SerializeField]
    public NarrativeEvent CurrentNarrativeEvent;

    //onde ir� haver o output da musica
    [SerializeField]  
    AudioSource musicPlayer;

    /// <summary>
    /// Imagem apresentada no Background durante o jogo.
    /// </summary>
    [SerializeField]
    private Image BackgroundImage;

    /// <summary>
    /// Flag que indica se o Evento Narrativo atual ainda est� a ser executado.
    /// </summary>
    private bool CurrentEventHasBeenManaged {
        get
        {
            if (_IsFirstEvent)
            {
                _IsFirstEvent = false;
                return true;
            }

            return CurrentNarrativeEvent.HasBeenManaged;
        }
    }
    private bool _IsFirstEvent = true;

    public bool Fading;

    #endregion Propriedades

    #region M�todos Unity

    void Start()
    {
        EffectManager = GetComponent<EffectManager>();
        BaseNarrativeEventManager.GetManagersReference(this, EffectManager);
        DialogueManager = GetComponent<DialogueManager>();
        ChoiceManager = GetComponent<ChoiceManager>();
        RandomManager = GetComponent<RandomManager>();
        BackgroundManager =GetComponent<BackgroundManager>();

        EffectManager.FadeTimeWaited = 0f;
    }

    private void Update()
    {
        if (!CurrentEventHasBeenManaged)
            return;

        if (!Fading && (EffectManager.FadeTimeWaited >= 1 || EffectManager.FadeTimeWaited <= 0))
        {
            if (CurrentNarrativeEvent.IsMemory) 
                EffectManager.ChangeToMemory(); 
            else
                EffectManager.ChangeToNormal();

            ManageCurrentEvent();

            EffectManager.FaddingIn = !(EffectManager.FaddingIn == true);
            CurrentNarrativeEvent.HasBeenManaged = false;
        }    
    }

    #endregion M�todos Unity

    public void FHasBeenManaged()
    {
        if (CurrentNarrativeEvent.FadeAfter)
        {
            EffectManager.FaddingIn = true;
            Fading = true;
        }

        CurrentNarrativeEvent = CurrentNarrativeEvent.NextEvent;
        CurrentNarrativeEvent.HasBeenManaged = true;
    }

    /// <summary>
    /// Alterar a imagem de background apresentada no jogo.
    /// </summary>
    /// <param name="newBackgroundSprite"></param>
    public void ChangeBackGround(Sprite newBackgroundSprite)
    {
        if (newBackgroundSprite != null)
            BackgroundManager.changeBackgound(newBackgroundSprite);
                
        // BackgroundImage.sprite = newBackgroundSprite;        
    }

    /// <summary>
    /// Alterar a m�sica que est� sendo reproduzida (em loop).
    /// </summary>
    /// <param name="music"></param>
    public void ChangeMusic(AudioResource music)
    {
        musicPlayer.Stop();
        musicPlayer.resource = music;
        musicPlayer.Play();
    }

    /// <summary>
    /// Colcar aqui a l�gica de quando chegar ao fim da execu��o dos eventos narrativos.
    /// </summary>
    public void GameOver()
    {

    }

    public void ManageCurrentEvent()
    {
        if (CurrentNarrativeEvent == null)
        {
            GameOver();
            return;
        }

        // Se o Novo evento define uma m�sica, esta � reproduzida
        // Caso n�o houver uma m�sica, mant�m-se a m�sica antiga
        if (CurrentNarrativeEvent.musica != null)
            ChangeMusic(CurrentNarrativeEvent.musica);

        //fazer characters desaparecer p�s fade
        EffectManager.RemoveAllCharacters();

        // Debug.Log($"Event Type: {CurrentNarrativeEvent.GetType()}");

        // Gerir o evento narrativo de acordo com a sua tipologia
        switch (CurrentNarrativeEvent.Type)
        {
            case EventType.Dialogue:
                DialogueManager.StartNarrativeEvent(CurrentNarrativeEvent);
                break;

            case EventType.Choice:
                ChoiceManager.StartNarrativeEvent(CurrentNarrativeEvent);
                break;

            case EventType.Random: case EventType.BranchEvent:
                RandomManager.StartNarrativeEvent(CurrentNarrativeEvent);
                break;

            case EventType.Background:
                BackgroundManager.StartNarrativeEvent(CurrentNarrativeEvent);
                break;
        }
    }
}
