using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private float jumpForce = 5f;
    private Transform transformOrigin;
    // Start is called before the first frame update
   // void Start()
    //{
    private Vector3 positionOrigin;
    private Vector3[] positions = new Vector3[3];
    private int positionIndex = 0;
    private float railDistance = 2f;
    private bool grounded;
    public enum State {
        Idle,
        Running,
        Dead
    };
    public State state = State.Running;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positionOrigin = transform.position;
        // Add possible horizontal positions

        transformOrigin = gameObject.transform;
        positions[0] = positionOrigin - Vector3.left*railDistance;
        positions[1] = positionOrigin;
        positions[2] = positionOrigin + Vector3.left*railDistance;
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
        // Toggle states (Debug)
        if(Input.GetKeyDown(KeyCode.T)) {
            if(state == State.Running) {
                state = State.Dead;
            } else {
                state = State.Running;
            }
        }
        if(state == State.Dead) return;
        if(state == State.Running) {
            // Jump
            if(Input.GetKeyDown("space") && grounded) {
                Jump();
            }
            // Horizontal Movement
            if(Input.GetKeyDown("left")) {
                if(positionIndex < positions.Length-1) {
                    positionIndex++;
                }
            }
            if(Input.GetKeyDown("right")) {
                if(positionIndex > 0) {
                    positionIndex--;
                }
            }
            Vector3 toPos = new Vector3(positions[positionIndex].x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, toPos, .5f);
        }
    }

    void Jump() {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }
        Debug.Log(grounded);
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Ground") {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision other) {
        if(other.collider.tag == "Ground") {
            grounded = false;
        }
    }
}
