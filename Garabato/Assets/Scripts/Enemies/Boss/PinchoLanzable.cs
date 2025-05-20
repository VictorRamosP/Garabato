using UnityEngine;

public class PinchoLanzable : MonoBehaviour
{
    [HideInInspector] public float speed;
    private bool isFalling = false;
    private float timer = 0;
    public void Launch()
    {
        isFalling = true;
    }

    void Update()
    {
        if (isFalling)
        {
            timer += Time.deltaTime;
            transform.position += (Vector3)(transform.up * speed * Time.deltaTime);
        }

        if (timer > 10f)
        {
            Destroy(this);
        }
    }
}

