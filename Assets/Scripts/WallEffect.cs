using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffect : MonoBehaviour
{
    Rigidbody2D wallRigidbody;
    bool cricri;
    // Start is called before the first frame update
    void Start()
    {
        wallRigidbody = GetComponent<Rigidbody2D>();

        wallRigidbody.AddForce(new Vector2(Random.Range(100f, 300f), Random.Range(500f, 1500f)));
        wallRigidbody.AddForceAtPosition(new Vector3(Random.Range(-5f, -10f), 0), new Vector2(10f, -5f));
        Destroy(gameObject, 3);
    }
}
