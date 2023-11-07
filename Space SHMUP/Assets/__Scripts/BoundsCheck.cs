using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum eType { center, inset, outset };

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;

    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        float checkRadius = boundsType switch
        {
            eType.inset => -radius,
            eType.outset => radius,
            _ => 0
        };

        Vector3 position = transform.position;

        if (position.x > camWidth) position.x = camWidth;
        if (position.x < -camWidth) position.x = -camWidth;
        if (position.y > camHeight) position.y = camHeight;
        if (position.y < -camHeight) position.y = -camHeight;

        transform.position = position;
    }
}
