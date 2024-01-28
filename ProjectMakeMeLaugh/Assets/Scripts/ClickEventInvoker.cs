using UnityEngine;
using UnityEngine.Events;

public class ClickEventInvoker : MonoBehaviour
{
    // Create a UnityEvent
    public UnityEvent onClickEvent;

    private void OnMouseDown()
    {
        // Check if the UnityEvent is assigned
        if (onClickEvent != null)
        {
            // Invoke the UnityEvent
            onClickEvent.Invoke();
        }
    }
}