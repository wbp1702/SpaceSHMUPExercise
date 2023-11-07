using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero Instance { get; private set; }

    [Header("Inscribed")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [Header("Dynamic")]
    [Range(0, 4)]
    public float shieldLevel = 1;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("More than one Hero Instance");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 position = transform.position;
        position.x += hAxis * speed * Time.deltaTime;
        position.y += vAxis * speed * Time.deltaTime;
        transform.position = position;

        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);
    }
}
