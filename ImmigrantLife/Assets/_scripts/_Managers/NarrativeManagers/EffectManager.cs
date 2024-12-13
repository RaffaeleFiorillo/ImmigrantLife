using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;



/// <summary>
/// Classe que gere todos os tipos de efeitos (visuais, sonoros,...) que ocorrem durante o jogo.
/// </summary>
public class EffectManager: BaseNarrativeEventManager
{
    #region Propriedades
    #endregion Propriedades

    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        throw new NotImplementedException("O EffectManager n�o gere eventos narrativos.");
    }

    private void Start()
    {
     
    }

    public void ApplyEffect(Effect effect)
    {
        if (effect == null) Debug.Log("Tentativa de utilizar um efeito nulo!");

        // Gerir o evento narrativo de acordo com a sua tipologia
        switch (effect.Type)
        {
            case EffectType.Visual:
            {
                var visualEffect = (VisualEffect)effect;
                ApplyVisualEffect(visualEffect);

                break;
            }
            case EffectType.Sound:
            {
                var soundEffect = (SoundEffect)effect;
                ApplySoundEffect(soundEffect);

                break;
            }
            default:
                throw new Exception($"Efeito de tipo desconhecido: {effect.Type}");
        }
    }

    #region M�todos :: Efeitos Visuais

    public void ApplyVisualEffect(VisualEffect effect)
    {
    }

    #endregion M�todos :: Efeitos Visuais


    #region M�todos :: Efeitos Sonoros

    public void ApplySoundEffect(SoundEffect effect)
    {

    }

    #endregion M�todos :: Efeitos Visuais

    #region M�todos :: Efeitos Visuais

    public void ApplyCharacterEffect(CharacterEffect effect)
    {
        // Colocar a imagem do personagem na frente do seu agrupamento
        // Remover a imagem do personagem, de dentro do seu grupo
        // Alterar a imagem de um personagem de acordo com o mood/tipologia (irritado, nervoso, com medo, ...)
    }

    #endregion M�todos :: Efeitos Visuais


    #region M�todos :: Auxiliares
    #endregion M�todos :: Auxiliares
}
