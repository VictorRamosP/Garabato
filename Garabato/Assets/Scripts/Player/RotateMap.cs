using System;
using System.Collections;
using System.Data;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    private GameObject map;
    public float cooldownRotate = 0.5f;
    private float cooldownTimer = 0f;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private string _WhereIsDown;
    private int currentRotationState = 0;
    public Action OnMapRotated;

    [Header("Controles")]
    public KeyCode k_Rotatemap = KeyCode.D;
    public KeyCode k_Rotatemap2 = KeyCode.A;

    [Header("Rotaci�n suave")]
    public float rotationDuration = 0.5f;
    private bool isRotating = false;
    public bool IsRotating => isRotating; // <- Propiedad p�blica

    public string WhereIsDown { get { return _WhereIsDown; } }

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        if (map == null)
        {
            Debug.LogError("No se ha encontrado un Mapa. Pon el tag 'Map' al mapa.");
        }

        if (map != null)
        {
            Debug.Log("El Map de RotateMap es: " + map.name);
        }
        else
        {
            Debug.LogError("Map no está asignado en RotateMap.");
        }
    }

    void Update()
    {
        if (ChangeCam.isMapActive)
        {
            transform.SetParent(map.transform);
            Rotate();
        }
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    void Rotate()
    {
        cooldownTimer -= Time.deltaTime;
        float originalGravity = _rigidbody.gravityScale;

        if ((Input.GetKeyDown(k_Rotatemap) || Input.GetKeyDown(k_Rotatemap2)) && cooldownTimer <= 0f && !isRotating)
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;

            float angle = (Input.GetKeyDown(k_Rotatemap2)) ? 90f : -90f;
            StartCoroutine(SmoothRotate(angle));
            cooldownTimer = cooldownRotate;
        }

        _rigidbody.gravityScale = originalGravity;
    }

    void RotateAroundPlayer(GameObject obj, Vector3 point, float angle)
    {
        Vector3 dir = obj.transform.position - point;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector3 newPos = point + rotation * dir;

        obj.transform.position = newPos;
        obj.transform.Rotate(0, 0, angle);
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
            float t = Mathf.SmoothStep(0, 1, elapsed / rotationDuration); // ← más suave aún
            map.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        map.transform.rotation = endRotation;

        if (_collider != null)
        {
            _collider.enabled = true;
        }

        isRotating = false;
        currentRotationState = (angle > 0) ? (currentRotationState + 1) % 4 : (currentRotationState + 3) % 4;

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
