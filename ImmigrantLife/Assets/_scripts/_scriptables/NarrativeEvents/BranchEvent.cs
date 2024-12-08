using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class BranchEvent : RandomEvent
{
    /// <summary>
    /// Propriedade que indica o tipo de Entidade da instância.
    /// </summary>
    public override EventType Type { get => EventType.BranchEvent; }

    /// <summary>
    /// A probabilidade deste evento ocorrer
    /// </summary>
    [SerializeField, Range(0, 100)]
    public override float Probability 
    { 
        get => _probability;
        set 
        {
            // Sum the probabilities using LINQ
            float totalProbability = PossibleRandomEvents.Sum(e => e.Probability);
            _probability = totalProbability; 
        }
    }
    
    [SerializeField]
    public List<RandomEvent> PossibleRandomEvents;

}
