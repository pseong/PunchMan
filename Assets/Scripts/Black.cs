using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : MonoBehaviour
{
    public float speed = 5f;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
