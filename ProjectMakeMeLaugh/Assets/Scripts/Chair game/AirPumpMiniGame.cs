using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HydraulicChairMiniGame : MiniGame
{
    public float acceptableHeight;
    public float increment = 0.2f;
    public float lowerIntervalMin = 3.0f;
    public float lowerIntervalMax = 6.0f;
    public int requiredFixCount = 4;
    public Transform chairTransform;
    public float lowerRate = 0.1f;

    private float currentHeight;
    private float originalHeight;
    private Coroutine chairLowerCoroutine;
    private int fixCount;
    private bool restartCoroutine;

    private AirPumpHandle handle;
    private bool hasStarted = false;


    private void Awake()
    {
        handle = FindObjectOfType<AirPumpHandle>();
        handle.PumpCompletedEvent += IncreaseChairHeight;
    }

    public override void StartMiniGame()
    {
        Debug.LogError("Mini game start");
        base.StartMiniGame();
        chairTransform = GameManager.Instance.ChairTransform;
        originalHeight = currentHeight = chairTransform.localPosition.y; // Initialize currentHeight based on the chair's initial local position
        fixCount = 0;
        restartCoroutine = true;
        chairLowerCoroutine = StartCoroutine(LowerChairRoutine());
        
        
        hasStarted = true;
        
    }
    

    private IEnumerator LowerChairRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(restartCoroutine ? 0f : Random.Range(lowerIntervalMin, lowerIntervalMax));

            while (currentHeight >= acceptableHeight)
            {
                // Lower the chair
                currentHeight -= Time.deltaTime * lowerRate;

                // Check if the chair is at its normal height
                if (currentHeight <= acceptableHeight)
                {
                    // Invoke MiniGameFailed event
                    FailMiniGame();
                    yield break;
                }

                // Update the chair's local position based on the current height
                chairTransform.localPosition = new Vector3(chairTransform.localPosition.x, currentHeight, chairTransform.localPosition.z);

                yield return new WaitForEndOfFrame();
            }
            // Reset the coroutine to start lowering again
            restartCoroutine = false;
        }
    }

    public void IncreaseChairHeight()
    {
        if(!hasStarted) return;
        // Check if the chair is at its maximum height
        if (currentHeight <= originalHeight)
        {
            if(chairLowerCoroutine!=null)
                StopCoroutine(chairLowerCoroutine);
            // Increase the chair height
            currentHeight += increment;
            
            if (currentHeight > originalHeight)
                currentHeight = originalHeight;
            
            // Update the chair's local position based on the current height
            chairTransform.localPosition = new Vector3(chairTransform.localPosition.x, currentHeight, chairTransform.localPosition.z);
            
            if (currentHeight >= originalHeight)
            {
                fixCount++;
            }

            if (fixCount >= requiredFixCount)
            {
                // Invoke MiniGameWon event
                WinMiniGame();
            }
            else
            {
                chairLowerCoroutine = StartCoroutine(LowerChairRoutine());
            }
        }
    }

    public override void WinMiniGame()
    {
        base.WinMiniGame(); // Call the base class method

        Debug.LogError("win mini game");
        // Reset current height to the original height
        currentHeight = originalHeight;
        chairTransform.localPosition = new Vector3(chairTransform.localPosition.x, currentHeight, chairTransform.localPosition.z);
    }
}
