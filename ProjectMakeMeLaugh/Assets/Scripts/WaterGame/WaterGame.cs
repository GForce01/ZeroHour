using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGame : MiniGame
{
    private FeedingHand hand;
    private CameraManager cameraManager;
    private SpillDetection spillDetection;

    public int maxSpillage = 30;
    [SerializeField] private int currentSpillage = 0;

    private void Awake()
    {
        hand = FindObjectOfType<FeedingHand>();
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
    }
}
