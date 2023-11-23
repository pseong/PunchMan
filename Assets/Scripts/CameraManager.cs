using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //dontdestoryonload 는 최상위 부모 객체에만 쓸 수 있다....
    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;

    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private float minX = 0f;
    private float maxX = 60f;

    private Transform playerTransform;

    private float verticalSize;
    private float horizontalSize;

    Vector3 originPos;

    void Start()
    {
        playerTransform = transform.parent;
        verticalSize = Camera.main.orthographicSize * 2;
        horizontalSize = verticalSize * Camera.main.aspect;
    }

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        if (transform.position.x - horizontalSize / 2 < minX)
        {
            transform.position = new Vector3(minX + horizontalSize / 2, transform.position.y, transform.position.z);
        }
        if (transform.position.x + horizontalSize / 2 > maxX)
        {
            transform.position = new Vector3(maxX - horizontalSize / 2, transform.position.y, transform.position.z);
        }
    }

    public void Shake_()
    {
        originPos = transform.localPosition;
        StartCoroutine(Shake(0.3f, 0.05f));
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
