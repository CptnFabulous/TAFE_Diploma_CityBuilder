using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public float zoom;
    public float zoomChangeAmount = 10f;
    public float zoomMax = 30f;
    public float zoomMin = 100f;

    private static Minimap instance;
    public Camera minimapCamera;

    public void Start()
    {
        instance = this;
        minimapCamera = transform.GetComponent<Camera>();
        zoom = minimapCamera.orthographicSize;
    }

    public static void SetZoom(float size)
    {
        instance.minimapCamera.orthographicSize = size;
    }

    public static float GetZoom()
    {
        return instance.minimapCamera.orthographicSize;
    }

    public void ZoomIn()
    {
        instance.zoom -= zoomChangeAmount;
        if (instance.zoom <= zoomMin)
        {
            instance.zoom = zoomMin;
        }
        SetZoom(instance.zoom);
    }

    public void ZoomOut()
    {
        instance.zoom += zoomChangeAmount;
        if (instance.zoom >= zoomMax)
        {
            instance.zoom = zoomMax;
        }
        SetZoom(instance.zoom);
    }
}
