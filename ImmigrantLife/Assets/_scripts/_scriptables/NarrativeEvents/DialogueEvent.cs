using NUnit.Framework;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Classe que representa um Diálogo entre pelo menos duas Personagens.
/// </summary>
[CreateAssetMenu(fileName = "ScriptableDialogue", menuName = "Scriptable Objects/NarrativeEvents/DialogueEvent")]
public class DialogueEvent : NarrativeEvent
{
    /// <summary>
    /// Lista de componentes (Bloco) do diálogo. Cada bloco contém: - A frase a ser dita; - A personagem que diz a frase;
    /// </summary>

    public ChooseColorEnum DialogueBoxColorIndex;
    

    public List<DialogueBlock> DialogueBlocks;

    public override EventType Type { get => EventType.Dialogue; }
}


/// <summary>
/// Classe que representa um Bloco (unidade) do com dialogo.
/// Um Dialogo é formado por uma lista de Blocos.
/// </summary>
[System.Serializable]
public class DialogueBlock
{
    /// <summary>
    /// Personagem que representa o emissor da Frase ao qual este Bloco se refere.
    /// </summary>
    /// 
   
        public CharacterScriptable Speaker;
    
    public int emotionIndex =1;
    //public int emotionIndex =1;
    
    public int positionIndex;


    public bool CharacterIsAlone;
    /// <summary>
    /// Imagem que aparece no Background quando este bloco de diálogo é apresentado.
    /// </summary>
    public Sprite BackgroundImage;
    public AudioResource som;
    /// <summary>
    /// A Frase (texto) que é dita neste Bloco.
    /// </summary>
    [TextArea(2, 20)]
    public string Sentence;


}
