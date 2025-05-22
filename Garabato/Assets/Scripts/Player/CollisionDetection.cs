using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField]
    private LayerMask WhatIsPlatform;

    [SerializeField]
    private Transform GroundCheckPoint;
    [SerializeField]
    private Transform FrontCheckPoint;
    [SerializeField]
    private Transform RoofCheckPoint;

    public Transform CurrentPlatform;

    private float _checkRadius = 0.15f;
    private bool _wasGrounded;

    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded || _isPlatformGround; } }

    [SerializeField]
    private bool _isTouchingFront;
    public bool IsTouchingFront { get { return _isTouchingFront; } }

    [SerializeField]
    private bool _isPlatformGround;
    public bool IsPlatForm { get { return _isPlatformGround; } }

    [SerializeField]
    private bool _isTouchingRoof;
    public bool IsTouchingRoof { get { return _isTouchingRoof; } }

    [SerializeField]
    private float _distanceToGround;
    public float DistanceToGround { get { return _distanceToGround; } }

    [SerializeField]
    private float _groundAngle;
    public float GroundAngle { get { return _groundAngle; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPoint.position, _checkRadius);
        Gizmos.DrawWireSphere(FrontCheckPoint.position, _checkRadius);
        Gizmos.color = Color.white;
    }

    void FixedUpdate()
    {
        CheckCollisions();
        CheckDistanceToGround();
    }

    private void CheckCollisions()
    {
        CheckGrounded();
        CheckPlatformed();
        CheckFront();
        //CheckRoof(); 
    }

    private void CheckFront()
    {
        var colliders = Physics2D.OverlapCircleAll(FrontCheckPoint.position, _checkRadius, WhatIsGround);

        _isTouchingFront = (colliders.Length > 0);
    }

    private void CheckRoof()
    {
        var colliders = Physics2D.OverlapCircleAll(RoofCheckPoint.position, _checkRadius, WhatIsGround);

        _isTouchingRoof = (colliders.Length > 0);
    }

    private void CheckGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position, _checkRadius, WhatIsGround);

        _isGrounded =  (colliders.Length > 0);
    }

    private void CheckPlatformed()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position, _checkRadius, WhatIsPlatform);

        _isPlatformGround = (colliders.Length > 0);
        if (_isPlatformGround) CurrentPlatform = colliders[0].transform;
    }

    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckPoint.position, Vector2.down, 100, WhatIsGround);

        _distanceToGround = hit.distance;
        _groundAngle = Vector2.Angle(hit.normal,new Vector2(1,0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.collider.CompareTag("Enemy")) {
            SceneManager.LoadScene("Gameplay");
        }*/
    }
}
