using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : BaseNarrativeEventManager
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool changingBackground;
    bool changeNextEvent;


    [SerializeField] float changingTime;
    [SerializeField] float changingSpeed;

    [SerializeField] GameObject currentBackgroundI;
    [SerializeField] Image backgroundToFade;

    Sprite storedSprite;
    Sprite currentSprite;
    [SerializeField] GameObject imageToInstance;
    [SerializeField] GameObject backgroundPlace;

    [SerializeField] GameObject toPass;

    BackgroundEvent backgroundEvent;
    int backgroundIndex;

    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {


        backgroundEvent = (BackgroundEvent)narrativeEvent;
        toPass.SetActive(true);
        nextBackGround();



    
    }

    private void GoToNextNarrativeEvent()
    {
        backgroundIndex = 0;
        toPass.SetActive(false); 
        EventManager.CurrentNarrativeEvent =backgroundEvent.NextEvent;
        EventManager.CurrentNarrativeEvent.HasBeenManaged = true;


    }
      public  void nextBackGround()
    {

        if(backgroundIndex == (backgroundEvent.BackgroundBlocks.Count-1)){

            GoToNextNarrativeEvent();

            return;
        }

        backgroundIndex++;

        changeBackgound(backgroundEvent.BackgroundBlocks[backgroundIndex].Background);


    }


    public void changeBackgound(Sprite reiceiveBackground)    {

        if (changingBackground)
        {
            Destroy(backgroundToFade.gameObject);
            changingBackground = false;
           

        }


        if (currentBackgroundI.GetComponent<Image>().sprite == null)
        {
           // currentDialogEvent = toChange;
           
            justChange(reiceiveBackground);
            storedSprite = reiceiveBackground;
            
            return;
        }

backgroundToFade =  Instantiate(imageToInstance, backgroundPlace.transform).GetComponent<Image>();

        backgroundToFade.sprite = storedSprite;
        storedSprite = reiceiveBackground;

     //   currentSprite = reiceiveBackground;


        justChange(reiceiveBackground);

        changingBackground = true;



    }
    public void justChange(Sprite newBackground)
    {
        currentBackgroundI.GetComponent<Image>().sprite = newBackground;
        


    }


    private void Update()
    {


        if (!changingBackground)
            return;
       // Debug.Log("estou aqui");
       changingTime = changingSpeed * Time.deltaTime;

        if (backgroundToFade.color.a <= 0)
        {
            changingBackground = false;
            Destroy(backgroundToFade.gameObject);

            return;

        }

        Color BackColor;
        BackColor = backgroundToFade.color;
       BackColor.a -= changingTime;

        backgroundToFade.color = BackColor;

        



    }


}
