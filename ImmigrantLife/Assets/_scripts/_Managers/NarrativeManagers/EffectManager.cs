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
public class EffectManager : BaseNarrativeEventManager
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

   
    float TimeWaited;


    int emotionIndex;



    [Header("Fade Properties")]
    [SerializeField] float FadeSpeed;
    [SerializeField] float TimeToWaitFade;

   public float FadeTime;
  [HideInInspector]  public float FadeTimeWaited;


    public bool FaddingIn;
    [SerializeField] Volume volumeEffect;


    [Header("Propriedades mem�rias")]
    [SerializeField] Color MemoryColor;
    [SerializeField] float ChangeContrast;
    [SerializeField] float ChangeSaturation;



    #endregion Propriedades

    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        throw new NotImplementedException("O EffectManager n�o gere eventos narrativos.");
    }

    private void Update()
    {
        UpdateCharacterImage(ref ShouldUpdateLeftCharacter, ref charImageLeft, speakerLeft, charLeft, emotionIndex);
        UpdateCharacterImage(ref ShouldUpdateRightCharacter, ref charImageRight, speakerRight, charRight, emotionIndex);

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
    public void reiceiveCharacter(CharacterScriptable currentChar, int charPosition, int receiveEmotion)
    {
        switch (charPosition)
        {
            case 0:


                UpdateCharacter(ref speakerLeft, ref charImageLeft, ref charLeft, currentChar, receiveEmotion, ref ShouldUpdateLeftCharacter);


                break;

            case 1:


                UpdateCharacter(ref speakerRight, ref charImageRight, ref charRight, currentChar, receiveEmotion, ref ShouldUpdateRightCharacter);
                break;

            default:
                Debug.LogWarning("Posi��o de personagem errada: " + charPosition);
                break;



        }



        charLeft.SetBool("GoToFront", charPosition == 0);
        charRight.SetBool("GoToFront", charPosition == 1);
    }


    #region M�todos :: Auxiliares

    private void UpdateCharacter(ref CharacterScriptable speaker, ref Image charImage, ref Animator charAnimator, CharacterScriptable currentChar, int receiveEmotion, ref bool shouldUpdateCharacter)
    {




        if (speaker == null)
        {
            speaker = currentChar;
            charImage.sprite = currentChar.emotionsSprites[receiveEmotion];
         
            charAnimator.SetTrigger("Appear");

            return;
        }

        // Mesmo personagem, mas emo��o diferente
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



            charAnimator.SetTrigger("Disappear");
            emotionIndex = receiveEmotion;
            shouldUpdateCharacter = !shouldUpdateCharacter;
            TimeWaited = 0;
        }
    }

    private void UpdateCharacterImage(ref bool shouldUpdateCharacter, ref Image charImage, CharacterScriptable speaker, Animator charAnimator, int emotionIndex)
    {
        if (shouldUpdateCharacter)
        {
            Debug.Log(emotionIndex);
            TimeWaited += Time.deltaTime;
            if (TimeWaited >= CharacterTimeToWait)
            {
                TimeWaited = 0;
                charImage.sprite = speaker.emotionsSprites[emotionIndex];
              //  charAnimator.SetBool("AppearingBool", true);
                charAnimator.SetTrigger("Appear");
                shouldUpdateCharacter = false;
            }
        }
    }


    public void RemoveCharacter()
    {

       // charRight.SetBool("AppearingBool", false);
        charRight.SetTrigger("Disappear");
        speakerRight = null;

        charRight.SetBool("GoToFront", false);
    }
    public void RemoveAllCharacters()
    {

      //  charRight.SetBool("AppearingBool", true);
       //charRight.SetTrigger("Appear");
        charRight.SetTrigger("GoBack");
        speakerRight = null;


       // charLeft.SetBool("AppearingBool", true);
       // charLeft.SetTrigger("Appear", true);
        charLeft.SetTrigger("GoBack");
        speakerLeft = null;



    }
    public void makeCharsDisappear()
    {



    }

    #endregion M�todos :: Auxiliares

    #endregion characterEffects


    #region fadeMetods
    public void FadeIn()
    {
        //  Debug.Log("im here");
        if (FadeTime <= 0)
            return;

        FadeTime -= FadeSpeed * Time.deltaTime;



        VolumeProfile profile = volumeEffect.sharedProfile;

        if (profile.TryGet<ColorAdjustments>(out var colorAj))
        {
            Color color = ((Color)colorAj.colorFilter);

             Color.RGBToHSV(color,out float colorH,out float colorS,out float colorV);

            color = Color.HSVToRGB(colorH, colorS, FadeTime);

            colorAj.colorFilter.Override(color);
        }

        if (FadeTime <= 0)
            MaintainFade();
           

    }
    void MaintainFade()
    {
   
        FadeTimeWaited += Time.deltaTime;
        if (TimeToWaitFade >= FadeTimeWaited)
        {



        EventManager.fadding = false;
        }


    }

    public void FadeOut()
    {
        if (FadeTime >= 1)
            return;


        FadeTime += FadeSpeed * Time.deltaTime;


        VolumeProfile profile = volumeEffect.sharedProfile;

        if (profile.TryGet<ColorAdjustments>(out var colorAj))
        {

            Color color = ((Color)colorAj.colorFilter);

            Color.RGBToHSV(color, out float colorH, out float colorS, out float colorV);

            color = Color.HSVToRGB(colorH, colorS, FadeTime);

            colorAj.colorFilter.Override(color);
        }








    }
#endregion fadeMetods


    #region MemoryEffectRegion


    //altera os para os tons visuais de memoria
    public void ChangeToMemory()
    {
        //procura o profile visual
        VolumeProfile profile = volumeEffect.sharedProfile;
        //n sei bem explicar para que serve
        if (profile.TryGet<ColorAdjustments>(out var colorAj))
        {
            //altera a satura��o
            colorAj.saturation.value = ChangeSaturation;
            //altera o contraste
            colorAj.contrast.value = ChangeContrast;

            //altera a cor
            colorAj.colorFilter.Override(MemoryColor);
        }


    }


    //altera os para os tons visuais normais
    //igual ao anterior mas mete as properties normais
    public void ChangeToNormal()
    {
       
        VolumeProfile profile = volumeEffect.sharedProfile;

        
        if (profile.TryGet<ColorAdjustments>(out var colorAj))
        {
            colorAj.saturation.value =

          colorAj.contrast.value = 0;
            
            Color normalColor = Color.HSVToRGB(0, 0, 100);


            colorAj.colorFilter.Override(normalColor);
        }



    }

    #endregion MemoryEffectRegion


}