using UnityEngine;

/// <summary>
/// Os tipos de ações que podem ser efetuados em cima de efeitos
/// </summary>
public enum EffectType
{
    Visual,
    Sound,
    Character
}

public abstract class Effect : ScriptableObject
{
    /// <summary>
    /// Propriedade que indica o tipo de Efeito desse scriptable.
    /// </summary>
    public virtual EffectType Type { get => throw new System.Exception("A propriedade *Type* não foi implementada."); }
}
