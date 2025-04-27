using System;
using System.Collections;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    private GameObject map;
    public float cooldownRotate = 0.5f;
    private float cooldownTimer = 0f;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider; // Nuevo: referencia al collider
    private string _WhereIsDown;
    public string WhereIsDown { get { return _WhereIsDown; } }
    private int currentRotationState = 0;
    public Action OnMapRotated;

    [Header("Controles")]
    public KeyCode k_Rotatemap = KeyCode.A;
    public KeyCode k_Rotatemap2 = KeyCode.D;

    [Header("Rotación suave")]
    public float rotationDuration = 0.5f;
    private bool isRotating = false;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>(); // Nuevo: agarrar el collider

        if (map == null)
        {
            Debug.LogError("No se ha encontrado un Mapa. Pon el tag 'Map' al mapa.");
        }
    }

    void Update()
    {
        if (ChangeCam.isMapActive)
        {
            Rotate();
        }
    }

    void Rotate()
    {
        cooldownTimer -= Time.deltaTime;

        if ((Input.GetKeyDown(k_Rotatemap) || Input.GetKeyDown(k_Rotatemap2)) && cooldownTimer <= 0f && !isRotating)
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;

            float angle = (Input.GetKeyDown(k_Rotatemap2)) ? 90f : -90f;
            StartCoroutine(SmoothRotate(angle)); 

            cooldownTimer = cooldownRotate;
        }
    }

    IEnumerator SmoothRotate(float angle)
    {
        isRotating = true;

        if (_collider != null)
            _collider.enabled = false;

        Quaternion startRotation = map.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, angle);

        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / rotationDuration);
            map.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        map.transform.rotation = endRotation;

        isRotating = false;

        if (_collider != null)
            _collider.enabled = true;

        
        if (angle > 0)
            currentRotationState = (currentRotationState + 1) % 4;
        else
            currentRotationState = (currentRotationState + 3) % 4;

        switch (currentRotationState)
        {
            case 0: _WhereIsDown = "down"; break;
            case 1: _WhereIsDown = "right"; break;
            case 2: _WhereIsDown = "up"; break;
            case 3: _WhereIsDown = "left"; break;
        }

        OnMapRotated?.Invoke();
    }
}
