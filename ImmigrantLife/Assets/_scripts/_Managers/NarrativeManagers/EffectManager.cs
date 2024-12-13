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


 [SerializeField]   Image charImageLeft;
    [SerializeField]Image charImageRight;


    bool TimerIsOn;

    [SerializeField] float TimerToWait;
    float TimeWaited;


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


        if (!TimerIsOn)
            return;


        TimeWaited += Time.deltaTime;
        if (TimeWaited >= TimerToWait)
        {
            TimeWaited = 0;

            charImageLeft.sprite = speakerLeft.emotionsSprites[emotionIndex];
            charLeft.SetBool("AppearingBool", true);
            TimerIsOn = false;




        }




    }

    public void reiceiveCharacter(CharacterScriptable currentChar,int charPosition,int reiceiveEmotion)
    {

       
        switch (charPosition)
        {

            case 0:
                if(speakerLeft == null)
                {
                    
                    speakerLeft = currentChar;


                    charImageLeft.sprite = currentChar.emotionsSprites[reiceiveEmotion];

                    charLeft.SetBool("AppearingBool",true);


                    return;

                }

                if (speakerLeft == currentChar)
                {

                    charImageLeft.sprite = currentChar.emotionsSprites[reiceiveEmotion];

                    return;
                }
    speakerLeft = currentChar;
  emotionIndex = reiceiveEmotion;
                if (!TimerIsOn)
                {


            
              


                charLeft.SetBool("AppearingBool", false);


                TimerIsOn = true;

                }
                else
                {

                    charLeft.SetBool("AppearingBool", true);
                    TimerIsOn = false;
                    TimeWaited = 0;
                    charImageLeft.sprite = speakerLeft.emotionsSprites[emotionIndex];

                }

                break;
                case 1:



                break;



        }






    }





    #region Métodos :: Auxiliares
    #endregion Métodos :: Auxiliares
}
