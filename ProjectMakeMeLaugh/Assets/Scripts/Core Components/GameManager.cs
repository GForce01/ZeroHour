using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


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
    public List<GameEvent> GameEvents;          //list of events arranged in the game

    public float MinDelayBetweenEvents=10f;
    public float MaxDelayBetweenEvents=30f;

    public bool gameStarted;

    public int AcceptableFailures = 5;
    public UnityEvent OnGameWon;
    public UnityEvent OnGameLost;

    public Transform ChairTransform;
    public AudioSource bossAudio;

    private int failedEvents=0;
    

    private void Awake()
    {
        Instance = this;
        bossAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameStarted)
        {
            gameStarted = false;
            StartGame();
        }
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
        EventIndex = 0;
        
        GameEvents[EventIndex].StartGameEvent();
    }

    public void EndGame(bool hasWon)
    {
        gameStarted = false;
        if(hasWon)
            OnGameWon?.Invoke();
        else
            OnGameLost?.Invoke();
    }


    public void MoveToNextEvent()
    {
        var oldEvent = GameEvents[EventIndex];
        if (oldEvent.hasMiniGame)
        {
            if (oldEvent.miniGame.hasFailed)
                failedEvents++;
        }

        if (failedEvents > AcceptableFailures)
        {
            EndGame(false);
        }
        IEnumerator WaitForRandomWindow()
        {
            yield return new WaitForSeconds(Random.Range(MinDelayBetweenEvents, MaxDelayBetweenEvents));
            EventIndex++;
            if (EventIndex >= GameEvents.Count)
            {
                //end the game if there are no events left
                EndGame(true);
            }
            else if (GameEvents[EventIndex])
            {
                GameEvents[EventIndex].StartGameEvent();
            }
        }
        StopCoroutine(WaitForRandomWindow());
       
    }


    //Implement this when the we use scene management 
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartShredderGame()
    {
        if (GameEvents[EventIndex].hasMiniGame)
        {
            var game = GameEvents[EventIndex].GetComponentInChildren<FileMiniGame>();

            if (game)
            {
                game.StartGame();
            }
        }
    }
}
