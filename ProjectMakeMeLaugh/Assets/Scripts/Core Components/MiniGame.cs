using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Each mini game controller should inherit from this class and then have it's game loops and all the mechanics
/// but it should use the below 3 methods to communicate within the hierarchy 
/// </summary>
public class MiniGame : MonoBehaviour
{
    public GameEvent ParentEvent;

    
    public UnityEvent OnMiniGameStarted;
    public UnityEvent OnMiniGameWon;
    public UnityEvent OnMiniGameFailed;
    
    
    public virtual void StartMiniGame()
    {
        OnMiniGameStarted?.Invoke();
    }

    public virtual void WinMiniGame()
    {
        OnMiniGameWon?.Invoke();
        
        ParentEvent.EndGameEvent();
    }

    public virtual void FailMiniGame()
    {
        OnMiniGameFailed?.Invoke();
        
        ParentEvent.EndGameEvent();
    }
}
