using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptable", menuName = "Scriptable Objects/CharacterScriptable")]
public class CharacterScriptable : ScriptableObject
{
    public string speakerName;

    public List<Sprite> emotionsSprites;
}
