using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopController : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 positionOrigin;
    private Vector3[] positions = new Vector3[3];
    private int positionIndex = 0;
    private float railDistance = 2f;
    public enum State {
        After,
        Running,
        Dead
    };
    public State state = State.Running;
    // Start is called before the first frame update
    void Start()
    {
        positionOrigin = transform.position;
        // Add possible horizontal positions
        positions[0] = positionOrigin - Vector3.left*railDistance;
        positions[1] = positionOrigin;
        positions[2] = positionOrigin + Vector3.left*railDistance;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
