using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    [Header("LeftArmSwitch")]
    public GameObject LeftArm1;
    public GameObject LeftArm2;

    public float minInterval = 1.0f;
    public float maxInterval = 5.0f;

    private float LeftTimer = 0.0f;
    private float nextToggleTimeLeft = 0.0f;

    [Header("HeadSwitch")]
    public GameObject Head1;
    public GameObject Head2;

    public float minIntervalHead = 1.0f;
    public float maxIntervalHead = 5.0f;

    private float HeadTimer = 0.0f;
    private float nextToggleTimeHead = 0.0f;

    [Tooltip("Bool switch for deciding should head toggle")]
    public bool toggleHead
    {
        get { return toggleHead; }
        set
        {
            if (value == false)
            {
                Head1.SetActive(true);
                Head2.SetActive(false);
            }
            toggleHead = value;

        }
    }

    void Start()
    {
        toggleHead = true;
        InitializeLeftArm();
        InitializeHead();
    }

    void Update()
    {
        UpdateLeftArm();
        if(toggleHead)
            UpdateHead();
    }

    private void InitializeLeftArm()
    {
        LeftArm1.SetActive(true);
        LeftArm2.SetActive(false);
        SetNextToggleTimeLeftArm();
    }

    void SetNextToggleTimeLeftArm()
    {
        nextToggleTimeLeft = Random.Range(minInterval, maxInterval);
    }

    private void UpdateLeftArm()
    {
        LeftTimer += Time.deltaTime;

        if (LeftTimer >= nextToggleTimeLeft)
        {
            LeftArm1.SetActive(!LeftArm1.activeSelf);
            LeftArm2.SetActive(!LeftArm2.activeSelf);

            LeftTimer = 0.0f;
            SetNextToggleTimeLeftArm();
        }
    }

    private void InitializeHead()
    {
        Head1.SetActive(true);
        Head2.SetActive(false);
        SetNextToggleTimeHead();
    }

    private void SetNextToggleTimeHead()
    {
        nextToggleTimeHead = Random.Range(minIntervalHead, maxIntervalHead);
    }

    private void UpdateHead()
    {
        HeadTimer += Time.deltaTime;

        if( HeadTimer >= nextToggleTimeHead)
        {
            Head1.SetActive(!Head1.activeSelf);
            Head2.SetActive(!Head2.activeSelf);

            HeadTimer = 0.0f;
            SetNextToggleTimeHead();
        }
    }
}
