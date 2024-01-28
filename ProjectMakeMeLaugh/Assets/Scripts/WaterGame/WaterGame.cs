using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterGame : MiniGame
{
    private FeedingHand hand;
    private BossDrink drinkFace;
    private CameraManager cameraManager;
    private SpillDetection spillDetection;
    public Collider dispencer;

    public AudioSource audioSource;
    public AudioClip[] dialogueClips;
    public AudioClip chokeClip;
    public AudioClip gulpClip;

    public int maxSpillage = 250;
    [SerializeField] private int currentSpillage = 0;

    public UnityEvent OnFeedSuccess;
    public UnityEvent OnChoke;
    public UnityEvent OnSpilled;
    public UnityEvent OnNoFeed;

    public UnityEvent OnCameraStart;
    public UnityEvent OnCameraStop;

    private void Awake()
    {
        hand = FindObjectOfType<FeedingHand>();
        drinkFace = FindObjectOfType<BossDrink>();
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
        dispencer.enabled = true;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
    }

    private void MouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider == dispencer)
        {
            OnCameraStart?.Invoke();
        }
    }


    public void RoundOne()
    {
        PlayAudioAndWait(dialogueClips[0], () =>
        {
            
            
            
            //type method name here
        });
        
        
    }




    private void IncreaseWaterSpillage()
    {
        Debug.Log("Hello Water");
        currentSpillage += 1;
    }


    ///Audio

    public void PlayAudioAndWait(AudioClip clip, Action callback=null)
    {
        StartCoroutine(PlayAudioClip(clip, callback));
    }

    IEnumerator PlayAudioClip(AudioClip clip, Action callback=null)
    {
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitForSeconds(clip.length);
        callback?.Invoke();
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
