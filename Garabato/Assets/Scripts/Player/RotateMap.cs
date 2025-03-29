using System.Collections;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    private Rigidbody2D _rigidbody;


    private GameObject map;
    public float cooldownRotate = 0.5f; //cooldawn para poder volver a rotar el mapa
    public float airSuspense = 0.5f; // tiempo que permanece en el aire miestra el mapa rota
    private float cooldownTimer = 0f;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        map = GameObject.FindGameObjectWithTag("Map");

        if (map == null)
        {
            Debug.LogError("No se ha encontrado un Mapa. pon el tag Map al mapa");
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

            if (Input.GetKeyDown(KeyCode.V))
            {
                map.transform.Rotate(0, 0, 90);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                map.transform.Rotate(0, 0, -90);
            }

            cooldownTimer = cooldownRotate;
        }
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
