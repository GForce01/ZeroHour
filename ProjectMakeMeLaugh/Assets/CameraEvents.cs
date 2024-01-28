using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraEvents : MonoBehaviour
{
    public UnityEvent OnStartZoomOut;
    public UnityEvent OnWaterZoomIn;
    public UnityEvent OnWaterZoomOut;

    public void StartZoomOut()
    {
        OnStartZoomOut?.Invoke();
    }

    public void WaterZoomIn()
    {
        OnWaterZoomIn?.Invoke();
    }

    public void WaterZoomOut()
    {
        OnWaterZoomOut.Invoke();
    }
}
