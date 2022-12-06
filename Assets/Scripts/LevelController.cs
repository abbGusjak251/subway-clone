using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Vector3 startPos;
    private float speed = -0.0025f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("ResetPosition", 0f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed;
    }

    void ResetPosition() {
        transform.position = startPos;
    }
}
