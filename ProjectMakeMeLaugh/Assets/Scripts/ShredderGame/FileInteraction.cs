using System.Collections;
using TMPro;
using UnityEngine;

public class FileInteraction : MonoBehaviour
{
    public FileType fileType;
    public string fileText;
    public TextMeshPro tmp;

    private bool isDragging = false;
    private Vector3 offset;

    private Rigidbody2D rb2D;
    public float slowDownSpeed = 0.1f;
    public float releaseVelocity = 3f;
    private Vector2 clickPosition;

    // Set the file text when a file enters
    public void SetFileText(string text, FileType type, int num)
    {
        fileText = text;
        tmp.text = text;
        tmp.sortingOrder = num;
        fileType = type;
    }
    

    // Get the file text when needed
    public string GetFileText()
    {
        return fileText;
    }
    

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void ApplyReleaseVelocity()
    {
        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(0.04f);
            // Calculate the direction from the initial click position to the current mouse position
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (currentMousePosition - clickPosition);

            // Add a slight velocity in the direction of movement
            rb2D.velocity = direction * releaseVelocity;
        }

        StartCoroutine(Coroutine());

    }

    void Update()
    {
        // Implement simple linear drag to slow down the file over time

        if (rb2D.velocity.magnitude > 0.1f)
        {
            rb2D.velocity -= rb2D.velocity.normalized * (slowDownSpeed * Time.deltaTime); // Adjust the drag factor as needed
        }
        else
        {
            rb2D.velocity = Vector2.zero;
        }
        
        // Check for mouse button release to apply release velocity
        if (isDragging && Input.GetMouseButtonUp(0)) // Assuming left mouse button (button index 0)
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            ApplyReleaseVelocity();
        }
    }

    void OnMouseDown()
    {
        if (!isDragging)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        rb2D.velocity = Vector2.zero;
        
    }

    void OnMouseDrag()
    {
        if (!isDragging)
        {
            isDragging = true;
            rb2D.velocity = Vector2.zero;
        }

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
