using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingHand : MonoBehaviour
{
    [SerializeField] private Rigidbody2D cupRB; //rb for the cup
    [SerializeField] private Collider handCollider;
    [SerializeField] private GameObject holdGlass;
    [SerializeField] private GameObject feedGlass;
    [SerializeField] private GameObject splash;
    [SerializeField] private float xOffsert = 0;
    public float sensitivity = 1;
    public float speed = 1;
    private float mouseStartX;
    private float mouseEndX;
    [SerializeField] private float mouseXDistance;

    private void Awake()
    {
        if (handCollider == null)
        {
            handCollider = this.GetComponent<Collider>();
        }
    }

    void Update()
    {
        HandleInput();
        //HandleDragging();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
    }

    private void OnMouseUp()
    {
        //Check offset
        //Feed animation
        //anything else
    }



    private void FixedUpdate()
    {
        
    }

}
