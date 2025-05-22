using Cinemachine.Utility;
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
    public bool canMove = false;

    public CollisionDetection collisionDetection;
    /*
    [Header("Controles")]
    public KeyCode k_Jump = KeyCode.Space;
    */
    [HideInInspector] public bool shootingActive = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GameObject.FindGameObjectWithTag("Weapon");

        _weapon.SetActive(false);
        collisionDetection = GetComponent<CollisionDetection>();
    }

    void Update()
    {
        if (!canMove) return;
        if (!ChangeCam.isMapActive && canMove && GameManager.Instance.mapAnimationActivated)
        {
            Moverse();
        }

        float verticalVelocity = _rigidbody.velocity.y;

        _animator.SetFloat("verticalVelocity", verticalVelocity);
        _animator.SetBool("isGrounded", collisionDetection.IsGrounded);
        _animator.SetBool("isJumping", !collisionDetection.IsGrounded && verticalVelocity > 0.1f);
        _animator.SetBool("isFalling", !collisionDetection.IsGrounded && verticalVelocity < -0.1f);

        if (!collisionDetection.IsGrounded)
        {
            _weapon.SetActive(false);
        }

        bool isIdle = Mathf.Abs(InputManager.Instance.GetHorizontalAxis()) < 0.01f;
        bool isShootingIdle = isIdle && shootingActive;
        _animator.SetBool("isShootingIdle", isShootingIdle);
    }

    void Moverse()
    {
        float moveInput = 0;

        if (InputManager.Instance.GetMoveLeft())
            moveInput = -1f;
        else if (InputManager.Instance.GetMoveRight())
            moveInput = 1f;

        _rigidbody.velocity = new Vector3(moveInput * speed, _rigidbody.velocity.y);

        bool isRunning = Mathf.Abs(moveInput) > 0.01f;
        _animator.SetBool("IsRunning", isRunning);

        if (!shootingActive)
            _weapon.SetActive(isRunning);

        Orientacion(moveInput);
    }

    void Orientacion(float desiredDirection)
    {
        if ((mirandoDerecha && desiredDirection < 0) || (!mirandoDerecha && desiredDirection > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}
