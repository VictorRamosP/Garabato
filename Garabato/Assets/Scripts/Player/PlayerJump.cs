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
    public KeyCode JumpKey = KeyCode.Space;

    private Rigidbody2D _rigidbody;
    //private CollisionDetection _collisionDetection;
    private float _lastVelocityY;
    private float _jumpStartedTime;
    private bool _jumpHeld;

    //bool IsWallSliding => _collisionDetection.IsTouchingFront;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //_collisionDetection = GetComponent<CollisionDetection>();
    }

    void Update()
    {
        if (Input.GetKeyDown(JumpKey))
        {
            JumpStarted();
            _jumpHeld = true;
        }

        if (Input.GetKeyUp(JumpKey))
        {
            JumpFinished();
            _jumpHeld = false;
        }
    }

    void FixedUpdate()
    {
        if (IsPeakReached()) TweakGravity();

        //if (IsWallSliding) SetWallSlide();
    }

    void JumpStarted()
    {
        SetGravity();
        var vel = new Vector2(_rigidbody.velocity.x, GetJumpForce());
        _rigidbody.velocity = vel;
        _jumpStartedTime = Time.time;
    }

    void JumpFinished()
    {
        float fractionOfTimePressed = 1 / Mathf.Clamp01((Time.time - _jumpStartedTime) / PressTimeToMaxJump);
        _rigidbody.gravityScale *= fractionOfTimePressed;
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
