using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

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
    [HideInInspector] public float FadeTimeWaited;


    public bool FaddingIn;
    [SerializeField] Volume volumeEffect;


    [Header("Propriedades memórias")]
    [SerializeField] Color MemoryColor;
    [SerializeField] Color NormalColor;

    [SerializeField] float ChangeContrast;
    [SerializeField] float ChangeSaturation;

    public TypeOfFade currentFade;

    #endregion Propriedades
    private void Start()
    {

        resetFadeProperties();
    }
    public override void StartNarrativeEvent(NarrativeEvent narrativeEvent)
    {
        throw new NotImplementedException("O EffectManager não gere eventos narrativos.");
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
                Debug.LogWarning("Posição de personagem errada: " + charPosition);
                break;



        }



        charLeft.SetBool("GoToFront", charPosition == 0);
        charRight.SetBool("GoToFront", charPosition == 1);
    }


    #region Métodos :: Auxiliares

    private void UpdateCharacter(ref CharacterScriptable speaker, ref Image charImage, ref Animator charAnimator, CharacterScriptable currentChar, int receiveEmotion, ref bool shouldUpdateCharacter)
    {




        if (speaker == null)
        {
            speaker = currentChar;
            charImage.sprite = currentChar.emotionsSprites[receiveEmotion];

            charAnimator.SetTrigger("Appear");

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

    #endregion Métodos :: Auxiliares

    #endregion characterEffects



    #region fadeMetods

    void resetFadeProperties()
    {

        VolumeProfile profile = volumeEffect.sharedProfile;

        if (profile.TryGet<ColorAdjustments>(out var colorAj))
        {
            Color resetFadeColor = Color.white;
            colorAj.colorFilter.value = resetFadeColor;

           

            FadeTime = 0;


        }

        if (profile.TryGet<LiftGammaGain>(out var colorLift))
        {
            

            colorLift.lift.value = new Vector4(0f, 0f, 0f,0f);
        }
    }

    public void FadeIn()
    {
        //  Debug.Log("im here");
        if (FadeTime <= 0)
            return;



        FadeTime -= FadeSpeed * Time.deltaTime;

        VolumeProfile profile = volumeEffect.sharedProfile;
        switch (currentFade)
        {
            case TypeOfFade.NormalFade:
                if (profile.TryGet<ColorAdjustments>(out var colorAj))
                {


                    Color color = ((Color)colorAj.colorFilter);

                    Color.RGBToHSV(color, out float colorH, out float colorS, out float colorV);

                    color = Color.HSVToRGB(colorH, colorS, FadeTime);

                    colorAj.colorFilter.Override(color);



                }
                break;

            case TypeOfFade.MemoryFade:
                if (profile.TryGet<LiftGammaGain>(out var colorLift))
                {
                    Debug.Log("changin1");
                   float addFadeTime = 1f- FadeTime;
                    colorLift.lift.overrideState = true;
                    colorLift.lift.Override(new Vector4(1f,1f,1f, addFadeTime));
                }

                break;

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
        switch (currentFade)
        {
            case TypeOfFade.NormalFade:
                if (profile.TryGet<ColorAdjustments>(out var colorAj))
                {


                    Color color = ((Color)colorAj.colorFilter);

                    Color.RGBToHSV(color, out float colorH, out float colorS, out float colorV);

                    color = Color.HSVToRGB(colorH, colorS, FadeTime);

                    colorAj.colorFilter.Override(color);



                }
                break;
            case TypeOfFade.MemoryFade:
                if (profile.TryGet<LiftGammaGain>(out var colorLift))
                {
                   // Debug.Log(colorLift.lift);
                    float addFadeTime = 1f - FadeTime;

                    colorLift.lift.overrideState = true;
                    colorLift.lift.Override(new Vector4(1f, 1f,1f, addFadeTime));
                }

                break;
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
            //altera a saturação
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
        // return;

        VolumeProfile profile = volumeEffect.sharedProfile;


        if (profile.TryGet<ColorAdjustments>(out var colorAj))
        {




            colorAj.saturation.value = 0;

            colorAj.contrast.value = 0;

            //  Color normalColor = Color.HSVToRGB(0, 0, 100,false);

            // normalColor.
            colorAj.colorFilter.value = NormalColor;

        }



    }

    #endregion MemoryEffectRegion


}