using UnityEngine;


public abstract class RandomEvent : NarrativeEvent
{
    /// <summary>
    /// Propriedade que indica o tipo de Entidade da instância.
    /// </summary>
    public override EventType Type { get => EventType.Random; }

    public string Description { get; set; }


    /// <summary>
    /// A probabilidade deste evento ocorrer
    /// </summary>
    [SerializeField, Range(0, 100)]
    public virtual float Probability { get => _probability; set { _probability = value; } }
    protected float _probability;

}
