using System.Collections;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    public float JumpHeight;
    public float DistanceToMaxHeight;
    public float SpeedHorizontal;
    public float PressTimeToMaxJump;
    public float WallSlideSpeed = 1;
    public ContactFilter2D filter;
    //public KeyCode JumpKey = KeyCode.Space;
    public LayerMask floorlayerMask;
    public AudioClip jumpSound;
    public bool canJump;
    public float FallGravityMultiplier = 2.5f;
    private AudioSource _audioSource;
    private Rigidbody2D _rigidbody;
    private CollisionDetection _collisionDetection;
    private float _lastVelocityY;
    private float _jumpStartedTime;
    bool IsWallSliding => _collisionDetection.IsTouchingFront;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisionDetection = gameObject.GetComponent<CollisionDetection>();
        _audioSource = GetComponent<AudioSource>();
        canJump = true;
    }

    void Update()
    {
        if (ChangeCam.isMapActive || !canJump || !GameManager.Instance.mapAnimationActivated) return;
        if (InputManager.Instance.GetJumpDown() && _collisionDetection.IsGrounded)
        {
            JumpStarted();
            if (_audioSource && jumpSound)
            {
                _audioSource.PlayOneShot(jumpSound);
            }
        }

        if (InputManager.Instance.GetJumpUp())
        {
            JumpFinished();
        }


    }

    void FixedUpdate()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (FallGravityMultiplier - 1) * Time.fixedDeltaTime;
        }

        //if (IsWallSliding) SetWallSlide();
    }

    void JumpStarted()
    {
        SetGravity();


        float jumpDirection = InputManager.Instance.GetMoveLeft() ? -1f : InputManager.Instance.GetMoveRight() ? 1f : 0f;


        var vel = new Vector2(jumpDirection * SpeedHorizontal, GetJumpForce());
        _rigidbody.velocity = vel;


        GetComponent<PlayerMove>().jumpDirection = jumpDirection;

        _jumpStartedTime = Time.time;
    }

    void JumpFinished()
    {
        if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }

    private bool IsPeakReached()
    {

        bool reached = (_lastVelocityY * _rigidbody.velocity.y) < 0;
        _lastVelocityY = _rigidbody.velocity.y;

        return reached;
    }

    private void SetWallSlide()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,
            Mathf.Max(_rigidbody.velocity.y, -WallSlideSpeed));
    }

    private void SetGravity()
    {
        var grav = 2 * JumpHeight * (SpeedHorizontal * SpeedHorizontal) / (DistanceToMaxHeight * DistanceToMaxHeight);
        _rigidbody.gravityScale = grav / 9.81f;
    }

    private void TweakGravity()
    {
        _rigidbody.gravityScale *= 1.2f;
    }

    private float GetJumpForce()
    {
        return 2 * JumpHeight * SpeedHorizontal / DistanceToMaxHeight;
    }

    private float GetDistanceToGround()
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];
        Physics2D.Raycast(transform.position, Vector2.down, filter, hits, 10);
        return hits[0].distance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        float h = -GetDistanceToGround() + JumpHeight;
        Vector3 start = transform.position + new Vector3(-1, h, 0);
        Vector3 end = transform.position + new Vector3(1, h, 0);
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.white;
    }
}
