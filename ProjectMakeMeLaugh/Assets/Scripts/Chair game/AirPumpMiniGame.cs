using System.Collections;
using UnityEngine;

public class HydraulicChairMiniGame : MiniGame
{
    public float maxHeight = 3.0f;
    public float acceptableHeight;
    public float increment = 0.2f;
    public float lowerIntervalMin = 3.0f;
    public float lowerIntervalMax = 6.0f;
    public int requiredFixCount = 4;
    public Transform chairTransform;

    private float currentHeight;
    private float originalHeight;
    private Coroutine chairLowerCoroutine;
    private int fixCount;
    private bool restartCoroutine;

    private AirPumpHandle handle;

    private void Start()
    {
        originalHeight = currentHeight = chairTransform.localPosition.y; // Initialize currentHeight based on the chair's initial local position
        fixCount = 0;
        restartCoroutine = true;
        chairTransform = GameManager.Instance.ChairTransform;
        StartMiniGame();
        chairLowerCoroutine = StartCoroutine(LowerChairRoutine());
        
        handle = FindObjectOfType<AirPumpHandle>();

        handle.PumpCompletedEvent += IncreaseChairHeight;

    }

    private IEnumerator LowerChairRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(restartCoroutine ? 0f : Random.Range(lowerIntervalMin, lowerIntervalMax));

            while (currentHeight >= acceptableHeight)
            {
                // Lower the chair
                currentHeight -= Time.deltaTime;

                // Check if the chair is at its normal height
                if (currentHeight <= acceptableHeight)
                {
                    // Invoke MiniGameFailed event
                    FailMiniGame();
                    yield break;
                }

                // Update the chair's local position based on the current height
                chairTransform.localPosition = new Vector3(chairTransform.localPosition.x, currentHeight, chairTransform.localPosition.z);

                yield return null;
            }
            // Reset the coroutine to start lowering again
            restartCoroutine = false;
        }
    }

    public void IncreaseChairHeight()
    {
        // Check if the chair is at its maximum height
        if (currentHeight + increment <= originalHeight)
        {
            // Increase the chair height
            currentHeight += increment;

            // Update the chair's local position based on the current height
            chairTransform.localPosition = new Vector3(chairTransform.localPosition.x, currentHeight, chairTransform.localPosition.z);
            

            if (currentHeight >= originalHeight)
            {
                fixCount++;
                
                if(chairLowerCoroutine!=null)
                    StopCoroutine(chairLowerCoroutine);
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

        // Reset current height to the original height
        currentHeight = originalHeight;
        chairTransform.localPosition = new Vector3(chairTransform.localPosition.x, currentHeight, chairTransform.localPosition.z);
    }
}
