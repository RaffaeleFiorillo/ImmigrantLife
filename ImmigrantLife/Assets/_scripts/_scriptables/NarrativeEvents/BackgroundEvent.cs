using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "BackgroundeEvent", menuName = "Scriptable Objects/NarrativeEvents/BackgroundEvent")]
public class BackgroundEvent : NarrativeEvent
{
    public override EventType Type { get => EventType.Background; }

    public List<BackGroundBlock> BackgroundBlocks = new List<BackGroundBlock>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class BackGroundBlock
    {
        public Sprite Background;
        public AudioResource Som;


    }


}
