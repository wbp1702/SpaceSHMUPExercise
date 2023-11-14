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
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("Dynamic")]
    [SerializeField] [Range(0, 4)]
    private float _shieldLevel = 1;
    [Tooltip("This field holds a reference to the last triggering GameObject")]
    private GameObject lastTriggerGameObject = null;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    void TempFire()
    {
        GameObject projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.transform.position = transform.position;
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.up * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform root = other.transform.root;
        GameObject go = root.gameObject;
        //Debug.Log("Shild trigger hit by: " + gameObject.name);

        if (go == lastTriggerGameObject) return;
        lastTriggerGameObject = go;

        Enemy enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            shieldLevel--;
            Destroy(go);
        }
        else
        {
            Debug.LogWarning("Shild trigger hit by non-Enemy: " + go.name);
        }
    }

    public float shieldLevel
    {
        get { return _shieldLevel; }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);

            if (value < 0)
            {
                Destroy(gameObject);
                Main.HERO_DIED();
            }
        }
    }
}
