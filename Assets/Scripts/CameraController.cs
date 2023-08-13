using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameData gameData;

    [Header("RUNTIME")]
    public Vector3 trackingPoint;

    public void SetTrackingPoint(Vector3 position, bool snap = false)
    {
        trackingPoint = position;

        if (snap)
        {
            UpdateCameraPosition(1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateCameraPosition(gameData.cameraLerpRate);
    }

    // Track only in Z
    private void UpdateCameraPosition(float lerp)
    { 
        Vector3 pos = transform.position;

        pos.z = Mathf.Lerp(pos.z, trackingPoint.z + gameData.cameraOffset, lerp);

        transform.position = pos;
    }
}
