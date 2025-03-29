using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private string _horizontalInputAxis = "Horizontal";
    private string _verticalInputAxis = "Vertical";
    private Vector3 _desiredDirection = Vector3.zero;


    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidbody;
    public float speed;
    public float jumpForce;
    public LayerMask floorlayerMask;

    private GameObject map;
    public float cooldownRotate = 0.5f; //cooldawn para poder volver a rotar el mapa
    public float airSuspense = 0.5f; // tiempo que permanece en el aire miestra el mapa rota
    private float cooldownTimer = 0f;


    private bool mirandoDerecha = true;


    [Header("Controles")]
    public KeyCode k_space = KeyCode.Space;

    /*[Header("Animaciones")]
    private Animator _anim;
    public string anim_velocidadY = "VelocidadY";
    public string anim_run = "Run";
    public string anim_jump = "Jump";
    public string anim_OnGround = "OnGround";
    public string anim_climbing = "Climbing";*/



    void Start()
    {
        //_anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

        map = GameObject.FindGameObjectWithTag("Map");

        if (map == null)
        {
            Debug.LogError("No se ha encontrado un Mapa. pon el tag Map al mapa");
        }
    }

    void Update()
    {
        //_anim.SetFloat(anim_velocidadY, _rigidbody.velocity.y);

        Moverse();
        Salto();
        RotateMap();
    }

    void Moverse()
    {
        _desiredDirection.x = Input.GetAxis(_horizontalInputAxis);
        _desiredDirection.y = Input.GetAxis(_verticalInputAxis);

        //AnimacionRun(_desiredDirection.x);

        _rigidbody.velocity = new Vector3(_desiredDirection.x * speed, _rigidbody.velocity.y);


        Orientacion(_desiredDirection.x);
    }

    void AnimacionRun(float inputmov)
    {
        if (inputmov != 0f)
        {
            //_anim.SetBool(anim_run, true);
        }
        else
        {
            //_anim.SetBool(anim_run, false);
        }
    }

    // Cambia la orientacion hacia donde mira el personaje
    public void Orientacion(float desiredDirection)
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
        // Caja para detectar si el jugador toca con el suelo o no
        RaycastHit2D raycastbox = Physics2D.BoxCast(_boxCollider.bounds.center, size, 0f, Vector2.down, 0.2f, floorlayerMask);

        return raycastbox.collider != null;
    }

    void Salto()
    {
        if (Input.GetKeyDown(k_space) && TocandoSuelo())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //_anim.SetTrigger(anim_jump);
        }
        //_anim.SetBool(anim_OnGround, TocandoSuelo());
    }

    void RotateMap()
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
