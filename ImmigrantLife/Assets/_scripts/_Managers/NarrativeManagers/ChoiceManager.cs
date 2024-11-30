using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class ChoiceManager : BaseNarrativeEventManager
{
    private ChoiceEvent CurrentChoiceEvent { get; set; }

    [SerializeField] GameObject buttonBox;
    [SerializeField] GameObject choisePrefab;

    List<GameObject> storedInstances = new List<GameObject>();
    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        CurrentChoiceEvent = (ChoiceEvent)narrativeEvent;


      
            for(int i = 0; i<CurrentChoiceEvent.Choices.Count; i++)
            {
            int cIndex = i;

        GameObject choiceButton =   Instantiate(choisePrefab,buttonBox.transform);
            storedInstances.Add(choiceButton);
            choiceButton.GetComponent<Button>().onClick.AddListener(()=>reiceiveChoice(cIndex));

           
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = CurrentChoiceEvent.Choices[cIndex].choiceName; 

            }



    }
    public void reiceiveChoice(int choiceIndex)
    {



        Debug.Log(choiceIndex);

    }

  




}
