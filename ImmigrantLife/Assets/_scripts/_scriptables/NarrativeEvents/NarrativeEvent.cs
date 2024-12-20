using UnityEngine;
using UnityEngine.Audio;

public enum EventType 
{
    Dialogue,
    Choice,
    Animation,
    Random,
    BranchEvent,
    Minigame,
    Background
}

public abstract class NarrativeEvent : ScriptableObject
{
    /// <summary>
    /// Propriedade que indica o tipo de Entidade da instância.
    /// </summary>
    public virtual EventType Type { get => throw new System.Exception("A propriedade *Type* não foi implementada."); }

    /// <summary>
    /// Flag que indica se o Evento Narrativo atual terminou de ser tratado (true) ou ainda está neste processo (false).
    /// </summary>
    public bool HasBeenManaged { get => _hasBeenManaged; set { _hasBeenManaged = value; } }
    protected bool _hasBeenManaged = false;
    public bool IsMemory;
    public TypeOfFade FadeAfter;

    /// <summary>
    /// O Evento Narrativo que vem a seguir deste.
    /// </summary>
    [SerializeField]
    public NarrativeEvent NextEvent;

    /// <summary>
    /// Audio a ser reproduzido quanto o evento for gerido.
    /// </summary>
    public AudioResource musica;
    public AudioResource backgroundNoice;
}
