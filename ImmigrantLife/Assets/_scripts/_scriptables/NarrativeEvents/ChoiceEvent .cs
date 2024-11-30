using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NarrativeEvent", menuName = "Scriptable Objects/NarrativeEvent")]
public abstract class ChoiceEvent : NarrativeEvent
{
    public override EventType Type {get => EventType.Choice;}

    [SerializeField]
    public List<Choice> Choices;
}

public class Choice
{
    public float Money;

    public float Anxiety;

    public NarrativeEvent NextEvent;
}