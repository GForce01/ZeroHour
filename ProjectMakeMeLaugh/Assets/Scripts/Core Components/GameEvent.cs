using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This can be anything from basic animations, audios to actual mini games. These can be used to add random events in the game
/// 
/// </summary>
public class GameEvent : MonoBehaviour
{
    public bool hasMiniGame;
    public UnityEvent OnGameEventStart;
    public UnityEvent OnGameEventEnd;
    public MiniGame MiniGame;


    public void StartGameEvent()
    {
        //can enable like a click or something on the respective mini game object and when the user clicks on it we can call the mini game methods
        
        OnGameEventStart?.Invoke();

        if (hasMiniGame)
        {
            MiniGame.ParentEvent = this;
        }
    }
    public void EndGameEvent()
    {
        OnGameEventEnd?.Invoke();
        GameManager.Instance.MoveToNextEvent();
    }
}
