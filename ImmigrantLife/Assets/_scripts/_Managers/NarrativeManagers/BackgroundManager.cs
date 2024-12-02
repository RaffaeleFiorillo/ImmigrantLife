using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    DialogueEvent storedEvent;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void reiceiveDialogEvent(DialogueEvent reicevedEvent)
    {
        //recebe e guarda o dialogue event
        storedEvent = reicevedEvent;
        ChangeBackGround(0);

    }

    public void ChangeBackGround(int backGroundIndex)
    {
        Debug.Log(backGroundIndex);
        //altera o background
        if (storedEvent.DialogueBlocks[backGroundIndex].backGround != null)
            backgroundImage.sprite = storedEvent.DialogueBlocks[backGroundIndex].backGround;



    }



}
