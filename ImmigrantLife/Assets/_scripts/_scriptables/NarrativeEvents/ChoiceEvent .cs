using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choice Event", menuName = "Scriptable Objects/NarrativeEvents/Choice Event")]
public  class ChoiceEvent : NarrativeEvent
{
    public override EventType Type {get => EventType.Choice;}

    /// <summary>
    /// Lista de escolhas possíveis que podem ser feitas durante este evento.
    /// </summary>
    [TextArea(2,5)]
    public string Question;

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
    /// Valor a ser adicionado à estatistica do Personagem relativamente ao seu dinheiro.
    /// </summary>
    /// 
    public float Money;

    /// <summary>
    /// Valor a ser adicionado à estatistica do Personagem relativamente ao seu nível de ansiedade.
    /// </summary>
    public float Anxiety;

    /// <summary>
    /// Descrição que vai ser apresentada no botão da escolha.
    /// </summary>
    [TextArea(5,10)]
    public string Description;

    /// <summary>
    /// Evento que vem a seguir se for selecionada esta escolha.
    /// </summary>
    public NarrativeEvent NextEvent;
}