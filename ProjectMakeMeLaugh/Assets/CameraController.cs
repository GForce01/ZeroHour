using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public Transform Table;
    public Transform ActualMiniGame;
    public Transform OriginalPosition;

    public float zoomSpeed = 2f;
    public float moveSpeed = 5f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void ZoomInAndMoveToActualMiniGame()
    {
        StartCoroutine(ZoomAndMoveCoroutine(Table.position, ActualMiniGame.position));
    }

    public void ZoomOutAndMoveToOriginalPosition()
    {
        StartCoroutine(ZoomAndMoveCoroutine(ActualMiniGame.position, OriginalPosition.position));
    }

    private IEnumerator ZoomAndMoveCoroutine(Vector3 initialPosition, Vector3 targetPosition)
    {
        targetPosition.z = -10;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            mainCamera.fieldOfView = Mathf.Lerp(60f, 30f, t); // Adjust the FOV values as needed

            yield return null;
        }

        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            yield return null;
        }
    }
}