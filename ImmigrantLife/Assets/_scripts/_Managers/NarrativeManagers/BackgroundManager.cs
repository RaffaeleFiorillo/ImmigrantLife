using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : BaseNarrativeEventManager
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool changingBackground;
    [SerializeField] float changingTime;
    [SerializeField] float changingSpeed;

    [SerializeField] GameObject currentBackgroundI;
    [SerializeField] Image backgroundToFade;
    [SerializeField] GameObject imageToInstance;
    [SerializeField] GameObject backgroundPlace;


    DialogueEvent currentDialogEvent;
    DialogueEvent lastDialogEvent;
    int lastBIndex;
    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    { 
    
    
    }



    public void changeBackgound(DialogueEvent toChange,int  backgoundIndex) 
    {

        if (changingBackground)
        {
            Destroy(backgroundToFade.gameObject);
            changingBackground = false;
           

        }


        if (currentDialogEvent != null)
        {

        lastDialogEvent = currentDialogEvent;
            
        }


        if (currentDialogEvent == null)
        {
            currentDialogEvent = toChange;
            justChange(backgoundIndex);
            
            return;
        }

backgroundToFade =  Instantiate(imageToInstance, backgroundPlace.transform).GetComponent<Image>();

        backgroundToFade.sprite = currentBackgroundI.GetComponent<Image>().sprite;

        justChange(backgoundIndex) ;

        changingBackground = true;



    }
    public void justChange(int backgoundIndex)
    {
        currentBackgroundI.GetComponent<Image>().sprite = currentDialogEvent.DialogueBlocks[backgoundIndex].BackgroundImage;
        lastBIndex = backgoundIndex;


    }


    private void Update()
    {
        if (!changingBackground)
            return;
        Debug.Log("estou aqui");
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
