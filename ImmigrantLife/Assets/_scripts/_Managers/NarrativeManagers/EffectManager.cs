using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Classe que gere todos os tipos de efeitos (visuais, sonoros,...) que ocorrem durante o jogo.
/// </summary>
/// 
public class EffectManager: BaseNarrativeEventManager
{
    #region Propriedades

    [Header("Character Properties")]

    [SerializeField] Animator charLeft;
    [SerializeField] Animator charRight;

    CharacterScriptable speakerLeft;
    CharacterScriptable speakerRight;

    [SerializeField] Image charImageLeft;
    [SerializeField] Image charImageRight;
    [SerializeField] 

    private bool ShouldUpdateLeftCharacter;

    private bool ShouldUpdateRightCharacter;

    [SerializeField] float CharacterTimeToWait;

    float LeftCharacterTimeWaited;
    float RightCharacterTimeWaited;


    int emotionIndex;



    [Header("Fade Properties")]
    [SerializeField] float FadeSpeed;

   public float FadeTimeWaited;


   public bool FaddingIn;
    [SerializeField]Volume volumeEffect;

    #endregion Propriedades
    
    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        throw new NotImplementedException("O EffectManager não gere eventos narrativos.");
    }

    private void Update()
    {
        UpdateCharacterImage(ref ShouldUpdateLeftCharacter, ref LeftCharacterTimeWaited, ref charImageLeft, speakerLeft, charLeft, emotionIndex);
        UpdateCharacterImage(ref ShouldUpdateRightCharacter, ref RightCharacterTimeWaited, ref charImageRight, speakerRight, charRight, emotionIndex);

switch (FaddingIn)
        {

            case false:
                FadeOut();

              //  Debug.Log("Segundo");


                break;


            case true:
                FadeIn();
                break;


        }


    }
    #region characterEffects
    public void reiceiveCharacter(CharacterScriptable currentChar,int charPosition,int receiveEmotion)
    {
        switch (charPosition)
        {
            case 0:


                UpdateCharacter(ref speakerLeft, ref charImageLeft, ref charLeft, currentChar, receiveEmotion, ref ShouldUpdateLeftCharacter, ref LeftCharacterTimeWaited);


                break;

            case 1:

                
                UpdateCharacter(ref speakerRight, ref charImageRight, ref charRight, currentChar, receiveEmotion, ref ShouldUpdateRightCharacter, ref LeftCharacterTimeWaited);
                break;

            default:
                Debug.LogWarning("Posição de personagem errada: " + charPosition);
                break;



        }



        charLeft.SetBool("GoToFront", charPosition==0);
        charRight.SetBool("GoToFront", charPosition == 1);
    }


    #region Métodos :: Auxiliares

    private void UpdateCharacter(ref CharacterScriptable speaker, ref Image charImage, ref Animator charAnimator, CharacterScriptable currentChar, int receiveEmotion, ref bool shouldUpdateCharacter, ref float timeWaited)
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
           // charImage.sprite = currentChar.emotionsSprites[receiveEmotion];
           


            charAnimator.SetBool("AppearingBool", false);
            shouldUpdateCharacter = !shouldUpdateCharacter;
            timeWaited = 0;
        }
    }

    private void UpdateCharacterImage(ref bool shouldUpdateCharacter, ref float timeWaited, ref Image charImage, CharacterScriptable speaker, Animator charAnimator, int emotionIndex)
    {
        if (shouldUpdateCharacter)
        {
            timeWaited += Time.deltaTime;
            if (timeWaited >= CharacterTimeToWait)
            {
                timeWaited = 0;
                charImage.sprite = speaker.emotionsSprites[emotionIndex];
                charAnimator.SetBool("AppearingBool", true);
                shouldUpdateCharacter = false;
            }
        }
    }


    public void RemoveCharacter()
    {

        charRight.SetBool("AppearingBool", false);


        charRight.SetBool("GoToFront", false);
    }


    #endregion Métodos :: Auxiliares

    #endregion characterEffects



    public void FadeIn()
    {
      //  Debug.Log("im here");
        if (FadeTimeWaited <=0)
            return;

            FadeTimeWaited -= FadeSpeed * Time.deltaTime;

        

        VolumeProfile profile = volumeEffect.sharedProfile;

        if (profile.TryGet<ColorAdjustments>(out var colorAj))
        {

            Color color = Color.HSVToRGB(0, 0, FadeTimeWaited);

            colorAj.colorFilter.Override(color);
        }

        if (FadeTimeWaited <= 0)
            EventManager.Fading = false;

    }

    public void FadeOut()
    {
        if (FadeTimeWaited >= 1)
            return;


        FadeTimeWaited += FadeSpeed * Time.deltaTime;


        VolumeProfile profile = volumeEffect.sharedProfile;

        if (profile.TryGet<ColorAdjustments>(out var colorAj))
        {
         
            Color color = Color.HSVToRGB(0, 0, FadeTimeWaited);

            colorAj.colorFilter.Override( color);
        }
       

        
        
      



    }
    void FadingTimer()
{
        /*
        FadeTimeWaited += Time.deltaTime;


        if (FadeTimeWaited < FadeTimeToWait)
            return;

        FadeTimeWaited = 0;



        isFadding = false;
        EventManager.fadding = false;
        */



        

        




}
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
