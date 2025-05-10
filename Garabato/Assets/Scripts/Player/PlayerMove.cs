using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private GameObject _weapon;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public float speed;
    public float jumpForce;
    public LayerMask floorlayerMask;

    public bool mirandoDerecha = true;

    [Header("Controles")]
    public KeyCode k_Jump = KeyCode.Space;

    [HideInInspector] public bool shootingActive = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    void Update()
    {
        if (!ChangeCam.isMapActive) 
        { 
            Moverse();
        }
        bool isIdle = Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f;
        bool isShootingIdle = isIdle && shootingActive;
        _animator.SetBool("isShootingIdle", isShootingIdle);
    }

    void Moverse()
    {
        float moveInput = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector3(moveInput * speed, _rigidbody.velocity.y);

        bool isRunning = Mathf.Abs(moveInput) > 0.01f;
        _animator.SetBool("IsRunning", isRunning);

        if (!shootingActive)
            _weapon.SetActive(isRunning);

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
