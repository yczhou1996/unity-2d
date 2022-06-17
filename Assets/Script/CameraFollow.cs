using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing;
    public Vector2 minPosition;
    public Vector2 maxPosition;
        
    void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 pos = target.position;
                pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
                pos.y = Mathf.Clamp(pos.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, pos, smoothing);
            }
        }
    }

    public void SetCameraLimit(Vector2 minPos, Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }

    
}
