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

    private void Awake()
    {
        if (handCollider != null)
        {
            handCollider = GetComponent<Collider>();
        }
    }

    private void FixedUpdate()
    {
        
    }

}
