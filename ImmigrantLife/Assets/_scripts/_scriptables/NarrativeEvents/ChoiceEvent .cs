using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choice Event", menuName = "Scriptable Objects/NarrativeEvents/Choice Event")]
public  class ChoiceEvent : NarrativeEvent
{
    public override EventType Type {get => EventType.Choice;}

    /// <summary>
    /// Lista de escolhas poss�veis que podem ser feitas durante este evento.
    /// </summary>
    public string question;
    [SerializeField]
    public List<Choice> Choices;
}

/// <summary>
/// Esta classe representa uma escolha (e os respetivos efeitos) que o jogador pode tomar.
/// </summary>
[System.Serializable]
public class Choice
{
    /// <summary>
    /// Valor a ser adicionado � estatistica do Personagem relativamente ao seu dinheiro.
    /// </summary>
    /// 
    public float Money;

    /// <summary>
    /// Valor a ser adicionado � estatistica do Personagem relativamente ao seu n�vel de ansiedade.
    /// </summary>
    public float Anxiety;

    /// <summary>
    /// Descri��o que vai ser apresentada no bot�o da escolha.
    /// </summary>
<<<<<<< Updated upstream
    /// 
    [TextArea(10,20)]
=======
  
>>>>>>> Stashed changes
    public string Description;

    /// <summary>
    /// Evento que vem a seguir se for selecionada esta escolha.
    /// </summary>
    public NarrativeEvent NextEvent;
}