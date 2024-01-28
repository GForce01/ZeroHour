using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using System.Linq.Expressions;

public class WaterGame : MiniGame
{
    private FeedingHand hand;
    private BossDrink drinkFace;
    private CameraManager cameraManager;
    private SpillDetection spillDetection;
    public Collider dispencer;

    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip[] dialogueClips;
    public AudioClip chokeClip;
    public AudioClip gulpClip;
    public AudioClip bgm;
    public AudioClip[] fail;
    public AudioClip win;

    public int maxSpillage = 250;
    [SerializeField] private int currentSpillage = 0;

    public UnityEvent OnFeedSuccess;
    public UnityEvent OnChoke;
    public UnityEvent OnSpilled;
    public UnityEvent OnNoFeed;

    public UnityEvent OnCameraStart;
    public UnityEvent OnCameraStop;

    int round = 1;

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
        if (currentSpillage >= maxSpillage)
        {
            StopAllCoroutines();
            FailMiniGame();
        }
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
        audioSource2.loop = true;
        audioSource2.clip = bgm;
        audioSource2.Play();
        PlayAudioAndWait(dialogueClips[0], () =>
        {
            drinkFace.gameObject.SetActive(true);
            hand.isDrinking = true;
            WaitRandom(() =>
            {
                PlayAudioAndWait(fail[1], () =>
                {
                    RoundTwo();
                });
            });   
            //type method name here
        });
  
    }

    public void RoundTwo()
    {
        PlayAudioAndWait(dialogueClips[1], () =>
        {
            drinkFace.gameObject.SetActive(true);
            hand.isDrinking = true;
            WaitRandom(() =>
            {
                PlayAudioAndWait(fail[1], () =>
                {
                    RoundTwo();
                });
            });
            //type method name here
        });
    }

    public void RoundThree()
    {
        PlayAudioAndWait(dialogueClips[2], () =>
        {
            drinkFace.gameObject.SetActive(true);
            hand.isDrinking = true;
            WaitRandom(() =>
            {
                PlayAudioAndWait(fail[1], () =>
                {
                    RoundTwo();
                });
            });
            //type method name here
        });
    }



    private void IncreaseWaterSpillage()
    {
        Debug.Log("Hello Water");
        currentSpillage += 1;
    }

    public override void WinMiniGame()
    {
        base.WinMiniGame(); // Call the base class method
        Debug.LogError("win mini game");
        audioSource.clip = win;
        audioSource.Play();
        OnCameraStop?.Invoke();
        dispencer.enabled = false;
        // Reset current height to the original 
    }

    public override void FailMiniGame()
    {
        base.FailMiniGame();
        audioSource.clip = fail[2];
        audioSource.Play();
        OnCameraStop?.Invoke();
        dispencer.enabled = false;
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

    public void WaitRandom(Action callback = null)
    {
        StartCoroutine(WaitRandomTime(callback));
    }

    IEnumerator WaitRandomTime(Action callback = null)
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        drinkFace.gameObject.SetActive(false);
        hand.isDrinking = false;
        callback?.Invoke();
    }




    //Feed events

    public void FeedSuccessDetected()
    {
        drinkFace.gameObject.SetActive(false);
        hand.isDrinking = false;
        StopCoroutine("WaitRandomTime");
        PlayAudioAndWait(gulpClip, () =>
        {
            NextStage();
        });
        OnFeedSuccess?.Invoke();
    }

    public void ChokeDetected()
    {
        drinkFace.gameObject.SetActive(false);
        hand.isDrinking = false;
        StopCoroutine("WaitRandomTime");
        PlayAudioAndWait(chokeClip, () =>
        {
            NextStage();
        });
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


    public void NextStage()
    {
        if(round == 1)
        {
            round++;
            RoundTwo();
        }
        else if(round == 2)
        {
            round++;
            RoundThree();
        }
        else
        {
            WinMiniGame();
        }
    }
}
