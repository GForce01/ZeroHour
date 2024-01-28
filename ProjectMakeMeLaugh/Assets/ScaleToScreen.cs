using UnityEngine;

public class ScaleToScreen : MonoBehaviour
{
    public float padding = 0.1f; // Optional padding around the sprite

    void Start()
    {
        ScaleSpriteToScreen();
    }

    void ScaleSpriteToScreen()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
            return;
        }

        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = spriteRenderer.bounds.size.x / spriteRenderer.bounds.size.y;

        float scaleMultiplier = screenRatio / targetRatio;

        // Adjust scale with padding
        scaleMultiplier *= (1f - padding);

        // Set the new scale
        transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1f);
    }
}