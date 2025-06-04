using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public StateMachine stateMachine;

    public PlayerJumper playerJumper;
    public PlayerMove playerMove;
    public PlayerShoot playerShoot;

    public Rigidbody2D rb;
    public Animator animator;
    private bool isDead;
    void Start()
    {
        playerJumper = GetComponent<PlayerJumper>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState(this));
    }
    void Update()
    {
        if (!GameManager.Instance.canPlayerGetHurt)
            GetComponent<Collider2D>().enabled = false;
        else
            GetComponent<Collider2D>().enabled = true;

        if (GameManager.Instance.isMapActive)
        {
            stateMachine.ChangeState(new MapState(this));
        }
        else if (GameManager.Instance.canPlayerMove && (InputManager.Instance.GetMoveLeft() || InputManager.Instance.GetMoveRight()))
        {
            stateMachine.ChangeState(new RunningState(this));
        }
        else
        {
            stateMachine.ChangeState(new IdleState(this));
        }

        stateMachine.OnUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead || GameManager.Instance.isMapActive)
            return;

        if ((collision.CompareTag("Dead") || collision.CompareTag("Spikes") || collision.CompareTag("Enemy")) && !GameManager.Instance.isMapActive)
        {
            Morir();
        }
    }
    
    private void Morir()
    {
        isDead = true;

        if (weapon != null)
            weapon.SetActive(false);

        animator.SetTrigger("Die");

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        GameManager.Instance.setPlayerConstraints(false);

        StartCoroutine(ReloadAfterDelay(1f));
    }

    private IEnumerator ReloadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
