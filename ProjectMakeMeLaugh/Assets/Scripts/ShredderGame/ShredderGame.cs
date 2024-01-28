using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FileMiniGame : MiniGame
{
    public ShredderController ShredderControllerObject;

    public float miniGameDuration = 30;
    private void Awake()
    {
        ShredderControllerObject = FindObjectOfType<ShredderController>();
    }
    
    public override void StartMiniGame()
    {
        // Invoke the mini game started event
        OnMiniGameStarted?.Invoke();
    }

    public void StartGame()
    {
        ShredderControllerObject.miniGame = this;
        ShredderControllerObject.StartGame(miniGameDuration);
        
    }
    
    
}
