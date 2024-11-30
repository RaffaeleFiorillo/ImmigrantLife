using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choice Event", menuName = "Scriptable Objects/NarrativeEvents/Choice Event")]
public  class ChoiceEvent : NarrativeEvent
{
    public override EventType Type {get => EventType.Choice;}

    [SerializeField]
    public List<Choice> Choices;
}


[System.Serializable]
public class Choice
{
    public float Money;

    public float Anxiety;

    public string choiceName;
    public NarrativeEvent NextEvent;
}