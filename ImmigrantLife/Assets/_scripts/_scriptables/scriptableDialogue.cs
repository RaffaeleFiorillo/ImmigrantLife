using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptableDialogue", menuName = "Scriptable Objects/scriptableDialogue")]
public class scriptableDialogue : ScriptableObject
{
    //Class com dialogo e quem diz
    [System.Serializable]
    public class Dialogue
    {
        public CharacterScriptable Speaker;

        [TextArea(2,20)]
        public string Sentence;
    }

    //lista de dialogos por cena
    public List<Dialogue> dialogueList = new List<Dialogue>();   
}
