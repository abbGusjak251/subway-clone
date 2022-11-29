using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private float jumpForce = 5f;
    private Transform transformOrigin;
    // Start is called before the first frame update
    void Start()
    {
        transformOrigin = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            Jump();
        }
        if(Input.GetKeyDown("left")) {
            transform.position = transformOrigin.position + Vector3.left*2f;
            Debug.Log(transformOrigin.position);
        }
        if(Input.GetKeyDown("right")) {
            transform.position = transformOrigin.position - Vector3.left*2f;
            Debug.Log(transformOrigin.position);
        }
    }

    void Jump() {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }
}
