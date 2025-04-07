using System;
using System.Collections;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    private GameObject map;
    public float cooldownRotate = 0.5f;
    public float airSuspense = 0.5f;
    private float cooldownTimer = 0f;
    private Rigidbody2D _rigidbody;
    public string WhereIsDown;
    private int currentRotationState = 0;
    public Action OnMapRotated;

    [Header("Controles")]
    public KeyCode k_Rotatemap = KeyCode.A;
    public KeyCode k_Rotatemap2 = KeyCode.D;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");
        _rigidbody = GetComponent<Rigidbody2D>();

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

        if ((Input.GetKeyDown(k_Rotatemap) || Input.GetKeyDown(k_Rotatemap2)) && cooldownTimer <= 0f)
        {
            StartCoroutine(SuspendPlayer());

            float angle = (Input.GetKeyDown(KeyCode.V)) ? 90f : -90f;
            RotateAroundPlayer(map, transform.position, angle);
            cooldownTimer = cooldownRotate;

            if (angle > 0)
                currentRotationState = (currentRotationState + 1) % 4;
            else
                currentRotationState = (currentRotationState + 3) % 4;

            switch (currentRotationState)
            {
                case 0: WhereIsDown = "down"; break;
                case 1: WhereIsDown = "right"; break;
                case 2: WhereIsDown = "up"; break;
                case 3: WhereIsDown = "left"; break;
            }

            OnMapRotated?.Invoke();
        }
    }

    void RotateAroundPlayer(GameObject obj, Vector3 point, float angle)
    {
        Vector3 dir = obj.transform.position - point;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector3 newPos = point + rotation * dir;

        obj.transform.position = newPos;
        obj.transform.Rotate(0, 0, angle);
    }

    IEnumerator SuspendPlayer()
    {
        float originalGravity = _rigidbody.gravityScale;
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(airSuspense);

        _rigidbody.gravityScale = originalGravity;
    }
}
