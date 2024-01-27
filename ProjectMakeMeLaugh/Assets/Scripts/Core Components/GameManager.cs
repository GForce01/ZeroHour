using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


/// <summary>
/// The game manager will scroll through all the events in the list and move to the next event, it will also wait for a random time between min and max delay
/// the game manager can also be used to reload the game. 
/// </summary>
///
///

/*todo Need to add a camera manager, i dont know how to implement the perspective change whenever we enter a mini game. Do we load a new scene or do we make the current scene
 elements invisible and make the mini game elements active.
*/
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnGameStart;

    public int EventIndex = 0;
    public List<GameEvent> GameEvents;

    public float MinDelayBetweenEvents=10f;
    public float MaxDelayBetweenEvents=30f;

    public bool gameStarted;

    public UnityEvent OnGameEnd;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void StartGame()
    {
        gameStarted = true;
        OnGameStart?.Invoke();
    }

    public void EndGame()
    {
        gameStarted = false;
        OnGameEnd?.Invoke();
    }


    public void MoveToNextEvent()
    {
        EventIndex++;
        if (GameEvents[EventIndex])
        {
            GameEvents[EventIndex].StartGameEvent();
        }
        else
        {
            //end the game if there are no events left
            EndGame();
        }
    }


    //Implement this when the we use scene management 
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
