 using UnityEngine;

/// <summary>
/// A Base para outras classes com o objetivo de gerir tipos espec�ficos de eventos narrativos.
/// </summary>
[RequireComponent(typeof(EventManager))]
public abstract class BaseNarrativeEventManager : MonoBehaviour
{
    /// <summary>
    /// Esta propriedade serve para criar uma referencia ao EventManager. 
    /// Esta refer�ncia � dispon�vel para todas as Classes que herdarem do BaseNarrativeEventManager.
    /// N�o � dispon�vel para outras classes.
    /// </summary>
    protected static EventManager EventManager { get; private set; }
    protected static EffectManager EffectManager { get; private set; }
    /// <summary>
    /// Este m�todo oferece uma forma de atribuir a refer�ncia ao EventManager.
    /// </summary>
    /// <param name="eventManager">Inst�ncia de EventManager ao qual se quer estabelecer uma refer�ncia.</param>
    public static void GetEventManagerReference(EventManager eventManager,EffectManager effectManager)
    {
        EventManager = eventManager;
        EffectManager = effectManager;

    }

    

    /// <summary>
    /// Este m�todo vai implementar o tratamento de um evento narrativo.
    /// Cada Classe que herdar do BaseNarrativeEventManager deve ter a sua implementa��o de forma a tratar o tipo de evento narrativo ao qual diz respeito.
    /// </summary>
    /// <param name="narrativeEvent">O evento narrativo a ser tratado.</param>
    public abstract void StartNarrativeEvent(NarrativeEvent narrativeEvent);
}
