using UnityEngine;

/// <summary>
/// A Base para outras classes com o objetivo de gerir tipos específicos de eventos narrativos.
/// </summary>
[RequireComponent(typeof(EventManager))]
public abstract class BaseNarrativeEventManager : MonoBehaviour
{
    /// <summary>
    /// Esta propriedade serve para criar uma referencia ao EventManager. 
    /// Esta referência é disponível para todas as Classes que herdarem do BaseNarrativeEventManager.
    /// Não é disponível para outras classes.
    /// </summary>
    protected static EventManager EventManager { get; private set; }

    /// <summary>
    /// Este método oferece uma forma de atribuir a referência ao EventManager.
    /// </summary>
    /// <param name="eventManager">Instância de EventManager ao qual se quer estabelecer uma referência.</param>
    public static void GetEventManagerReference(EventManager eventManager)
    {
        EventManager = eventManager;
    }

    /// <summary>
    /// Este método vai implementar o tratamento de um evento narrativo.
    /// Cada Classe que herdar do BaseNarrativeEventManager deve ter a sua implementação de forma a tratar o tipo de evento narrativo ao qual diz respeito.
    /// </summary>
    /// <param name="narrativeEvent">O evento narrativo a ser tratado.</param>
    public abstract void StartNarrativeEvent(NarrativeEvent narrativeEvent);
}
