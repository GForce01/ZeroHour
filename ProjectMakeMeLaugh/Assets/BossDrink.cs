using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrink : MonoBehaviour
{
    [SerializeField] private Collider spillCollider;

    private void Awake()
    {
        if (spillCollider == null)
        {
            spillCollider = GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Destroy(collision.gameObject);
        }
    }
}
