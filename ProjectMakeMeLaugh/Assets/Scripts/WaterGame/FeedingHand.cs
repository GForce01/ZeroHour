using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingHand : MonoBehaviour
{
    [SerializeField] private Rigidbody2D cupRB; //rb for the cup
    private Vector3 rbStartPos;
    [SerializeField] private Collider handCollider;
    [SerializeField] private GameObject holdGlass;
    [SerializeField] private GameObject feedGlass;
    [SerializeField] private GameObject splash;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites = new Sprite[2];

    private bool isDragging = false;
    private Vector3 mouseStart;
    private Vector3 mouseEnd;
    [SerializeField] private float mouseXDistance;

    [SerializeField] private float xOffset = 0;
    public float minOffset = 1; //min and max distance for success
    public float maxOffset = 3;
    public float sensitivity = 1;
    public float minMoveSpeed = 1;
    public float maxMoveSpeed = 1;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private bool isMoving = false;

    private Vector2 targetPosition;

    public bool isDrinking = false;

    public delegate void FeedSuccessEventHandler();
    public event FeedSuccessEventHandler FeedSuccessEvent;

    public delegate void ChokeEventHandler();
    public event ChokeEventHandler ChokeEvent;

    public delegate void SpillEventHandler();
    public event SpillEventHandler SpillEvent;

    public delegate void NoFeedEventHandler();
    public event NoFeedEventHandler NoFeedEvent;

    private void Awake()
    {
        if (handCollider == null)
        {
            handCollider = this.GetComponent<Collider>();
        }

        rbStartPos = cupRB.position;
    }

    void Update()
    {
        HandleInput();
        HandleDragging();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }

    private void MouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && hit.collider == handCollider)
        {
            Debug.Log("Handshake!");
            isDragging = true;
            isMoving = true;
            mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void MouseUp()
    {
        if(isDragging)
        {
            isDragging = false;
            spriteRenderer.sprite = sprites[1];
            if (!isDrinking)
            {
                ChokeEvent?.Invoke();
                //end corutine
            }
            else
            {
                if (minOffset <= xOffset && xOffset <= maxOffset)
                {
                    FeedSuccessEvent?.Invoke();
                    //end corutine
                }
                else
                {
                    SpillEvent?.Invoke();
                    //end corutine
                }
            }
            //cupRB.transform.localPosition = Vector3.zero;
        }
        //anything else
    }

    private void HandleDragging()
    {
        if (isDragging)
        {
            mouseEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseXDistance = mouseEnd.x - mouseStart.x;

            xOffset = mouseXDistance * sensitivity;
            float newX = Mathf.Clamp(xOffset, 0, 12.3f);
            targetPosition = new Vector2(rbStartPos.x + newX, cupRB.position.y);
        }
    }


    private void FixedUpdate()
    {
        if (isDragging)
        {
            distance = Vector2.Distance(cupRB.position, targetPosition);
            speed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, distance);

            if (distance < 0.01f)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            if (isMoving)
            {
                Vector2 newPosition = Vector2.MoveTowards(cupRB.position, targetPosition, speed * Time.fixedDeltaTime);
                cupRB.MovePosition(newPosition);
            }
            
        }
        
    }

}
