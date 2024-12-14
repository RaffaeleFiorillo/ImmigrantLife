using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



/// <summary>
/// Classe que gere todos os tipos de efeitos (visuais, sonoros,...) que ocorrem durante o jogo.
/// </summary>
/// 
public class EffectManager: BaseNarrativeEventManager
{
    #region Propriedades

    [SerializeField] Animator charLeft;
    [SerializeField] Animator charRight;

  //  DialogueEvent currentEvent;

    CharacterScriptable speakerLeft;
    CharacterScriptable speakerRight;


    [SerializeField] Image charImageLeft;
    [SerializeField]Image charImageRight;


    private bool ShouldUpdateLeftCharacter { get; set; }

    bool ShouldUpdateRightCharacter;

    [SerializeField] float TimerToWait;

    float LeftCharacterTimeWaited;
    float RightCharacterTimeWaited;


    int emotionIndex;

    #endregion Propriedades

    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        throw new NotImplementedException("O EffectManager não gere eventos narrativos.");
    }

    private void Start()
    {
     
    }

    /*
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

    #region Métodos :: Efeitos Visuais

    public void ApplyVisualEffect(VisualEffect effect)
    {
    }

    #endregion Métodos :: Efeitos Visuais


    #region Métodos :: Efeitos Sonoros

    public void ApplySoundEffect(SoundEffect effect)
    {

    }

    #endregion Métodos :: Efeitos Visuais

    #region Métodos :: Efeitos Visuais

    public void ApplyCharacterEffect(CharacterEffect effect)
    {

        switch (effect.Action)
        {
            case CaracterActionType.Add:

                AddCharacter(effect);

                break;



        }

        // Colocar a imagem do personagem na frente do seu agrupamento
        // Remover a imagem do personagem, de dentro do seu grupo
        // Alterar a imagem de um personagem de acordo com o mood/tipologia (irritado, nervoso, com medo, ...)



    }
   
    

    #endregion Métodos :: Efeitos Visuais */

    private void Update()
    {
        UpdateCharacterTimer(ref ShouldUpdateLeftCharacter, ref LeftCharacterTimeWaited, TimerToWait, ref charImageLeft, speakerLeft, charLeft, emotionIndex);
        UpdateCharacterTimer(ref ShouldUpdateRightCharacter, ref RightCharacterTimeWaited, TimerToWait, ref charImageRight, speakerRight, charRight, emotionIndex);
    }

    public void reiceiveCharacter(CharacterScriptable currentChar,int charPosition,int reiceiveEmotion)
    {
        switch (charPosition)
        {
            case 0:
                UpdateCharacter(charPosition, ref speakerLeft, ref charImageLeft, ref charLeft, currentChar, receiveEmotion, ref ShouldUpdateLeftCharacter, ref LeftCharacterTimeWaited);
                break;

            case 1:
                UpdateCharacter(charPosition, ref speakerRight, ref charImageRight, ref charRight, currentChar, receiveEmotion, ref ShouldUpdateRightCharacter, ref LeftCharacterTimeWaited);
                break;

            default:
                Debug.LogWarning("Posição de personagem errada: " + charPosition);
                break;
        }
    }

    #region Métodos :: Auxiliares

    private void UpdateCharacter(int charPosition, ref GameObject speaker, ref SpriteRenderer charImage, ref Animator charAnimator, Character currentChar, int receiveEmotion, ref bool shouldUpdateCharacter, ref float timeWaited)
    {
        if (speaker == null)
        {
            speaker = currentChar;
            charImage.sprite = currentChar.emotionsSprites[receiveEmotion];
            charAnimator.SetBool("AppearingBool", true);
            return;
        }

        // Mesmo personagem, mas emoção diferente
        if (speaker == currentChar && charImage.sprite != currentChar.emotionsSprites[receiveEmotion])
        {
            charImage.sprite = currentChar.emotionsSprites[receiveEmotion];
            return;
        }

        // Logica de troca de personagem
        if (speaker != currentChar || shouldUpdateCharacter)
        {
            speaker = currentChar;
            charImage.sprite = currentChar.emotionsSprites[receiveEmotion];
            charAnimator.SetBool("AppearingBool", true);
            shouldUpdateCharacter = !shouldUpdateCharacter;
            timeWaited = 0;
        }
    }

    private void UpdateCharacterImage(ref bool shouldUpdateCharacter, ref float timeWaited, float timerToWait, ref SpriteRenderer charImage, GameObject speaker, Animator charAnimator, int emotionIndex)
    {
        if (shouldUpdateCharacter)
        {
            timeWaited += Time.deltaTime;
            if (timeWaited >= timerToWait)
            {
                timeWaited = 0;
                charImage.sprite = speaker.emotionsSprites[emotionIndex];
                charAnimator.SetBool("AppearingBool", true);
                shouldUpdateCharacter = false;
            }
        }
    }

    #endregion Métodos :: Auxiliares
}
