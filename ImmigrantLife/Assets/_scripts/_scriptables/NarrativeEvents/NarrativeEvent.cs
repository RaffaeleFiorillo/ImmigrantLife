using UnityEngine;
using UnityEngine.Audio;

public enum EventType 
{
    Dialogue,
    Choice,
    Animation,
}

public abstract class NarrativeEvent : ScriptableObject
{
    /// <summary>
    /// Propriedade que indica o tipo de Entidade da instância.
    /// </summary>
    public virtual EventType Type { get => throw new System.Exception("A propriedade *Type* não foi implementada."); }

    /// <summary>
    /// O Evento Narrativo que vem a seguir deste.
    /// </summary>
    [SerializeField]
    public NarrativeEvent NextEvent;
   public AudioResource musica;
}
