using UnityEngine;
using UnityEngine.Serialization;

public class AirPumpHandle : MonoBehaviour
{
    [SerializeField]
    private Collider pumpHandleCollider;

    [SerializeField]
    private Transform pumpHandleTransform;
    
    [SerializeField] private Animator _pipeAnimator;

    // Define the y-axis limits
    public float minHeight = 0.0f;
    public float maxHeight = 4.0f;

    // Threshold for considering a pump complete
    [FormerlySerializedAs("pumpCompleteThreshold")] public float pumpTopThreshold = 0.1f;
    [FormerlySerializedAs("pumpCompleteThreshold")] public float pumpBottomThreshold = 0.1f;
    // Rates for movement
    public float dragRate = 10f;
    public float lowerDownRate = 2f;

    private bool isDragging = false;
    public bool hasTouchedBottom = false;
    public bool hasTouchedTop = false;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private Coroutine lowerDownCoroutine;
    
    // Define a delegate for the pump completion event
    public delegate void PumpCompletedEventHandler();

    // Define the event based on the delegate
    public event PumpCompletedEventHandler PumpCompletedEvent;

    void Start()
    {
        // If collider is not assigned, use the collider on this GameObject
        if (pumpHandleCollider == null)
        {
            pumpHandleCollider = GetComponent<Collider>();
        }

        // If transform is not assigned, use the transform on this GameObject
        if (pumpHandleTransform == null)
        {
            UnityEngine.Debug.LogError("Pump handle transform not defined");
        }

        targetPosition = originalPosition = pumpHandleTransform.localPosition;
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
            OnMouseDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
    }

    private void HandleDragging()
    {
        if (isDragging)
        {
            // Get the current mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the new position of the handle along the y-axis within the defined limits
            float newY = Mathf.Clamp(mousePosition.y, minHeight, maxHeight);
            targetPosition.y = newY;

            // Smoothly move towards the new position
            pumpHandleTransform.localPosition = Vector3.Lerp(pumpHandleTransform.localPosition, targetPosition, Time.deltaTime * dragRate);

            // Check if pump is complete when dragging
            CheckPumpCompletion();
        }
        
    }

    private void OnMouseDown()
    {
        // Cast a ray from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits the collider of the pump handle
        if (Physics.Raycast(ray, out hit) && hit.collider == pumpHandleCollider)
        {
            // Click occurred on the pump handle
            isDragging = true;

            // Cancel the lower down coroutine if it's running
            if (lowerDownCoroutine != null)
            {
                StopCoroutine(lowerDownCoroutine);
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        //cancels the pump if you stop dragging
        hasTouchedTop = hasTouchedBottom = false;
        StartLowerDownCoroutine();
    }

    private void CheckPumpCompletion()
    {
        // Check if the pump is complete based on the threshold
        float deltaYToMaxHeight = targetPosition.y - maxHeight;
        float deltaYToOriginal = targetPosition.y - originalPosition.y;

        if (Mathf.Abs(deltaYToMaxHeight) <= pumpTopThreshold)
        {
            hasTouchedTop = true;
        }

        if (hasTouchedTop && Mathf.Abs(deltaYToOriginal) <= pumpBottomThreshold)
        {
            hasTouchedBottom = true;
        }

        if (hasTouchedBottom && hasTouchedBottom)
        {
            hasTouchedTop = hasTouchedBottom = false;
            PumpCompleted();
        }
        
    }

    private void StartLowerDownCoroutine()
    {
        // Start the coroutine to smoothly move the handle down to its final position
        lowerDownCoroutine = StartCoroutine(LowerHandleDown());
    }

    private System.Collections.IEnumerator LowerHandleDown()
    {
        Vector3 targetDownPosition = new Vector3(originalPosition.x, minHeight, originalPosition.z);

        while (Mathf.Abs(pumpHandleTransform.localPosition.y - minHeight)> 0.1f  )
        {
            pumpHandleTransform.localPosition = Vector3.Lerp(pumpHandleTransform.localPosition, targetDownPosition, Time.deltaTime * lowerDownRate);
            targetPosition = pumpHandleTransform.localPosition;
            yield return null;
        }

        // Ensure the handle is exactly at the minimum position
        pumpHandleTransform.localPosition = targetDownPosition;
        
    }

    private void PumpCompleted()
    {
        _pipeAnimator.SetTrigger("PumpAir");
        PumpCompletedEvent?.Invoke();
    }
    
}
