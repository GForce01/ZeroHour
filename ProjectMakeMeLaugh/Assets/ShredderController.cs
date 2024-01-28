using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum FileType
{
    Mail,
    Shred
}
public class ShredderController : MonoBehaviour
{
    private float timer;
    public bool isMiniGameActive=false;

    public TextMeshPro timerText;
    // Lists to store files
    public List<FileInteraction> files = new List<FileInteraction>();


    public int successfulActions = 0;
    public int wrongActionCount = 0;
    public int maxWrongShredCount = 5;

    public FileMiniGame miniGame;

    void Update()
    {
        if (isMiniGameActive)
        {
            timer -= Time.deltaTime;

            timerText.text = timer.ToString("F2");
            if (timer <= 0f || wrongActionCount >= maxWrongShredCount)
            {
                EndMiniGame();
                return;
            }
        }
    }

    public void StartGame(float duration)
    {

        // Initialize or reset game variables
        timer = duration;
        isMiniGameActive = true;
        successfulActions = 0;
        wrongActionCount = 0;
        
    }

    public void EndMiniGame()
    {
        if (successfulActions>5)
        {
           miniGame.WinMiniGame();
        }
        else
        {
            miniGame.FailMiniGame();
        }

        isMiniGameActive = false;
    }

    // Method to handle file release
    public void HandleFileReleased(bool wasRightFileReleased, FileInteraction fileInteraction)
    {
        files.Remove(fileInteraction);
        Destroy(fileInteraction.gameObject);
        if (wasRightFileReleased == true)
        {
            successfulActions++;

            if (files.Count == 0)
            {
                miniGame.WinMiniGame();
            }
        }
        else
        {
            wrongActionCount++;
            //add more negative feedback
            
            if (wrongActionCount >= maxWrongShredCount)
            {
                // Too many wrong shredding attempts, fail the mini game
                miniGame.FailMiniGame();
            }
        }
    }
}
