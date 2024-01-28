using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillDetection : MonoBehaviour
{
    [SerializeField] private Collider spillCollider;

    public delegate void SpillEventHandler();
    public event SpillEventHandler SpillDetectedEvent;

    private void Awake()
    {
        if(spillCollider == null)
        {
            spillCollider = GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            SpillDetectedEvent?.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
