using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D _rb;
    private float gravityScale;
    void Start()
    {
        gravityScale =  _rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isMapActive) {
            _rb.gravityScale = 0;
        }else {
            _rb.gravityScale = gravityScale;
        }
    }
}
