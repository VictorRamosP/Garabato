using System.Collections;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private GameObject map;
    public float cooldownRotate = 0.5f;
    public float airSuspense = 0.5f;
    private float cooldownTimer = 0f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        map = GameObject.FindGameObjectWithTag("Map");

        if (map == null)
        {
            Debug.LogError("No se ha encontrado un Mapa. Pon el tag 'Map' al mapa.");
        }
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        cooldownTimer -= Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.C)) && cooldownTimer <= 0f)
        {
            StartCoroutine(SuspendPlayer());

            float angle = (Input.GetKeyDown(KeyCode.V)) ? 90f : -90f;
            RotateAroundPlayer(map, transform.position, angle);

            cooldownTimer = cooldownRotate;
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