using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterGame : MiniGame
{
    private FeedingHand hand;
    private CameraManager cameraManager;
    private SpillDetection spillDetection;

    public int maxSpillage = 30;
    [SerializeField] private int currentSpillage = 0;

    public UnityEvent OnFeedSuccess;
    public UnityEvent OnChoke;
    public UnityEvent OnSpilled;
    public UnityEvent OnNoFeed;

    private void Awake()
    {
        hand = FindObjectOfType<FeedingHand>();
        hand.FeedSuccessEvent += FeedSuccessDetected;
        hand.ChokeEvent += ChokeDetected;
        hand.SpillEvent += SpillDetected;
        hand.NoFeedEvent += NoFeedDetected;

        cameraManager = FindObjectOfType<CameraManager>();
        spillDetection = FindObjectOfType<SpillDetection>();
        spillDetection.SpillDetectedEvent += IncreaseWaterSpillage;
    }

    public override void StartMiniGame()
    {
        base.StartMiniGame();   
    }

    private void IncreaseWaterSpillage()
    {
        Debug.Log("Hello Water");
        currentSpillage += 1;
    }

    //Feed events

    public void FeedSuccessDetected()
    {
        OnFeedSuccess?.Invoke();
    }

    public void ChokeDetected()
    {
        OnChoke?.Invoke();
    }

    public void SpillDetected()
    {
        OnSpilled?.Invoke();
    }

    public void NoFeedDetected()
    {
        OnNoFeed?.Invoke();
    }

}
