using UnityEngine;
using UnityEngine.Audio;

public abstract class CharacterEffect : Effect
{
    public override EffectType Type { get => EffectType.Character; }

    public Sprite CharacterImage;
}
