using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Transform m_transform;
    private float speed = -0.05f;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = m_transform.position + Vector3.forward * speed;
    }
}
