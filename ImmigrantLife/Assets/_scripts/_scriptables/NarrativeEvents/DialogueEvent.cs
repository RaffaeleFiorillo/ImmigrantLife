using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe que representa um Di�logo entre pelo menos duas Personagens.
/// </summary>
[CreateAssetMenu(fileName = "ScriptableDialogue", menuName = "Scriptable Objects/NarrativeEvents/DialogueEvent")]
public class DialogueEvent : NarrativeEvent
{
    /// <summary>
    /// Lista de componentes (Bloco) do di�logo. Cada bloco cont�m: - A frase a ser dita; - A personagem que diz a frase;
    /// </summary>
    public List<DialogueBlock> DialogueBlocks;

    public override EventType Type { get => EventType.Dialogue; }
}


/// <summary>
/// Classe que representa um Bloco (unidade) do com dialogo.
/// Um Dialogo � formado por uma lista de Blocos.
/// </summary>
[System.Serializable]
public class DialogueBlock
{
    /// <summary>
    /// Personagem que representa o emissor da Frase ao qual este Bloco se refere.
    /// </summary>
    public CharacterScriptable Speaker;
  public  Sprite backGround;
    /// <summary>
    /// A Frase (texto) que � dita neste Bloco.
    /// </summary>
    [TextArea(2, 20)]
    public string Sentence;
}
