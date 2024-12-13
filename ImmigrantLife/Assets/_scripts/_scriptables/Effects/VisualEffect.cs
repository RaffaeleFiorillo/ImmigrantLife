using UnityEngine;
using UnityEngine.Audio;

public abstract class VisualEffect : Effect
{
    public override EffectType Type { get => EffectType.Visual; }

}
