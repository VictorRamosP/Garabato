using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float speed;
    public float jumpForce;
    public LayerMask floorlayerMask;

    private BoxCollider2D _boxCollider;
    public bool mirandoDerecha = true;
    public GameObject shooting;

    [Header("Controles")]
    public KeyCode k_Jump = KeyCode.Space;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!ChangeCam.isMapActive) // Solo permite moverse si la c�mara del jugador est� activa
        {
            Moverse();
            //Salto();
            RotateShoot();
        }
    }

    void Moverse()
    {
        float moveInput = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector3(moveInput * speed, _rigidbody.velocity.y);
        Orientacion(moveInput);
    }

    void Orientacion(float desiredDirection)
    {
        if ((mirandoDerecha == true && desiredDirection < 0) || (mirandoDerecha == false && desiredDirection > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    bool TocandoSuelo()
    {
        Vector2 size = new Vector2(_boxCollider.bounds.size.x, _boxCollider.bounds.size.y);
        RaycastHit2D raycastbox = Physics2D.BoxCast(_boxCollider.bounds.center, size, 0f, Vector2.down, 0.2f, floorlayerMask);
        return raycastbox.collider != null;
    }

    void Salto()
    {
        if (Input.GetKeyDown(k_Jump) && TocandoSuelo())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void RotateShoot()
    {
        if (Input.GetKey(KeyCode.W))
        {
            shooting.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            shooting.transform.rotation = mirandoDerecha ? Quaternion.Euler(0, 0, -90) : Quaternion.Euler(0, 0, 90);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy")) {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
