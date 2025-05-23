using System.Collections;
using UnityEngine;

public class EraserController : MonoBehaviour
{
    public float speed = 2f;
    public string[] erasableTags;
    public float eraseDelay = 0.5f;        

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detectar jugador
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Jugador tocado por la goma");

            // Ejecutar muerte como en CollisionDead
            CollisionDead deathScript = collision.GetComponent<CollisionDead>();
            if (deathScript != null && deathScript.isActive)
            {
                deathScript.SendMessage("OnTriggerEnter2D", GetComponent<Collider2D>(), SendMessageOptions.DontRequireReceiver);
            }

            return;
        }

        // Detectar objetos con tag "erasable"
        foreach (string tag in erasableTags)
        {
            if (collision.CompareTag(tag))
            {
                StartCoroutine(EraseObject(collision.gameObject));
                break;
            }
        }
    }

    IEnumerator EraseObject(GameObject obj)
    {
        yield return new WaitForSeconds(eraseDelay);
        obj.SetActive(false);
    }
}