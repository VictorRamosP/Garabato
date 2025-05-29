using Cinemachine.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public ParticleSystem walkParticles;

    public CollisionDetection collisionDetection;

    [HideInInspector] public bool shootingActive = false;


    private bool isDead = false;


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
        /*if (isRunning && collisionDetection.IsGrounded)
        {
            if (!walkParticles.isPlaying)
                walkParticles.Play();
        }
        else
        {
            if (walkParticles.isPlaying)
                walkParticles.Stop();
        }*/
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead || ChangeCam.isMapActive)
            return;

        if ((collision.CompareTag("Dead") || collision.CompareTag("Spikes")) && !ChangeCam.isMapActive)
        {
            speed = 0;
            Morir();
        }
    }

    private void Morir()
    {
        isDead = true;

        if (_weapon != null)
            _weapon.SetActive(false);

        _animator.SetTrigger("Die");

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;

        // Desactiva otros scripts menos este
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
                script.enabled = false;
        }

        StartCoroutine(ReloadAfterDelay(1f));
    }

    private IEnumerator ReloadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}