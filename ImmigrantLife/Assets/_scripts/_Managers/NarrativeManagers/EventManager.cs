using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
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


    /// <summary>
    /// Componente responsável por gerir a apresentação de Eventos Random.
    /// </summary>
    private RandomManager RandomManager { get; set; }


    private BackgroundManager BackgroundManager { get; set; }
   private EffectManager effectManager { get; set; }
    #endregion Managers

   [HideInInspector] public bool fadding;
    
    #region Propriedades

    /// <summary>
    /// Evento Narrativo atual.
    /// </summary>
    [SerializeField]
    public NarrativeEvent CurrentNarrativeEvent;

    //onde irá haver o output da musica
    [SerializeField]  
    AudioSource musicPlayer; 

    [SerializeField]  
    AudioSource backgroundNoisePlayer;

    /// <summary>
    /// Imagem apresentada no Background durante o jogo.
    /// </summary>
    [SerializeField]
    private Image BackgroundImage;

    /// <summary>
    /// Flag que indica se o Evento Narrativo atual ainda está a ser executado.
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

    #endregion Propriedades

    #region Métodos Unity

    void Start()
    {
        effectManager = GetComponent<EffectManager>();
        BaseNarrativeEventManager.GetEventManagerReference(this,GetComponent<EffectManager>());
        DialogueManager = GetComponent<DialogueManager>();
        ChoiceManager = GetComponent<ChoiceManager>();
        RandomManager = GetComponent<RandomManager>();
        BackgroundManager =GetComponent<BackgroundManager>();

     //   effectManager.FadeTime = 0f;
    }

    private void Update()
    {


        if (!CurrentEventHasBeenManaged)
            return;
        



        if (effectManager.FadeTime >= 1 && fadding==false || effectManager.FadeTime <= 0 &&fadding == false)
        {
           
            switch (CurrentNarrativeEvent.IsMemory)
            {
                case true:
                    effectManager.ChangeToMemory();
                    break;
                    case false:
                    effectManager.ChangeToNormal();
                    break;
            }
            ManageCurrentEvent();
            if(effectManager.FaddingIn ==true)
            effectManager.FaddingIn = false;
            CurrentNarrativeEvent.HasBeenManaged = false;
        }
       
    }
    
    public void FHasBeenManaged()
    {
        if (CurrentNarrativeEvent.FadeAfter !=TypeOfFade.nullFade)
        {
            effectManager.currentFade = CurrentNarrativeEvent.FadeAfter;
            effectManager.FaddingIn = true;
            fadding = true;
           // Debug.Log("primeiro");

        }

            CurrentNarrativeEvent = CurrentNarrativeEvent.NextEvent;
        CurrentNarrativeEvent.HasBeenManaged = true;



    }
    
    
    #endregion Métodos Unity

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
    /// Alterar a música que está sendo reproduzida (em loop).
    /// </summary>
    /// <param name="music"></param>
    public void ChangeMusic(AudioResource music, AudioResource noise)
    {
        if (musicPlayer.resource != music)
        {

            musicPlayer.Stop();

      
        
        if (music != null)
        {

        musicPlayer.resource = music;
        musicPlayer.Play();
        }
        }

        if (backgroundNoisePlayer.resource != noise)
        {

            backgroundNoisePlayer.Stop();
        if (noise == null)
            return;

        backgroundNoisePlayer.resource = noise;
        backgroundNoisePlayer.Play();
        }
        


    }

    /// <summary>
    /// Colcar aqui a lógica de quando chegar ao fim da execução dos eventos narrativos.
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

        // Se o Novo evento define uma música, esta é reproduzida
        // Caso não houver uma música, mantém-se a música antiga
        
            ChangeMusic(CurrentNarrativeEvent.musica,CurrentNarrativeEvent.backgroundNoice);


        //fazer characters desaparecer pós fade
        effectManager.RemoveAllCharacters();


        // Debug.Log($"Event Type: {CurrentNarrativeEvent.GetType()}");

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
            case EventType.Random: case EventType.BranchEvent:
            {
                RandomManager.StartNarrativeEvent(CurrentNarrativeEvent);
                break;
            }
            case EventType.Background:
            {
                BackgroundManager.StartNarrativeEvent(CurrentNarrativeEvent);
                break;
            }
        }
    }
}
