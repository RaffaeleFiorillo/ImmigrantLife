using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventDialogue", menuName = "Scriptable Objects/EventDialogue")]
public class EventDialogue : scriptableDialogue
{


    public int dinheiro;
    public int anciedade;
    public List<scriptableDialogue> eventosPossiveis = new List<scriptableDialogue>();
    






}
