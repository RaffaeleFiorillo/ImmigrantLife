using UnityEngine;
using UnityEngine.Audio;

public abstract class SoundEffect : Effect
{
    public override EffectType Type { get => EffectType.Sound; }

}
