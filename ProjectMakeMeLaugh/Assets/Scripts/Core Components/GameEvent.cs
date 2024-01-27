using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// This can be anything from basic animations, audios to actual mini games. These can be used to add random events in the game
/// 
/// </summary>
public class GameEvent : MonoBehaviour
{
    public bool hasMiniGame;
    public UnityEvent OnGameEventStart;
    public UnityEvent OnGameEventEnd;
    [FormerlySerializedAs("miniGamee")] [FormerlySerializedAs("MiniGame")] public MiniGame miniGame;


    public void StartGameEvent()
    {
        //can enable like a click or something on the respective mini game object and when the user clicks on it we can call the mini game methods
        
        OnGameEventStart?.Invoke();

        if (hasMiniGame)
        {
            miniGame.ParentEvent = this;
        }
    }
    public void EndGameEvent()
    {
        OnGameEventEnd?.Invoke();
        GameManager.Instance.MoveToNextEvent();
    }
}
