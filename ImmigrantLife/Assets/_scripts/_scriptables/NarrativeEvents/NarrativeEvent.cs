using UnityEngine;

public enum EventType 
{
    Dialogue,
    Choice,
    Animation,
}

[CreateAssetMenu(fileName = "NarrativeEvent", menuName = "Scriptable Objects/NarrativeEvent")]
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
}
