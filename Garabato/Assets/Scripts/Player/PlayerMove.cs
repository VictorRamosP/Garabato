using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;
    public float speed;
    public float jumpForce;
    public LayerMask floorlayerMask;

    public bool mirandoDerecha = true;

    [Header("Controles")]
    public KeyCode k_Jump = KeyCode.Space;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = gameObject.GetComponent<CollisionDetection>();
    }

    void Update()
    {
        if (!ChangeCam.isMapActive) 
        { 
            Moverse();
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
}
