using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptableDialogue", menuName = "Scriptable Objects/scriptableDialogue")]
public class scriptableDialogue : ScriptableObject
{


    //Class com dialogo e quem diz
    [System.Serializable]
   public class dialogue
    {
        public CharacterScriptable charInfo;
   



        [TextArea(2,20)]
        public string theDialogue;



    }


    //lista de dialogos por cena
    public List<dialogue> dialogueList = new List<dialogue>();


    
}
