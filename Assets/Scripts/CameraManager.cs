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

    Vector3 originPos;

    //private Camera theCamera;

    //void Start()
    //{
    //Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
    //theCamera = GetComponent<Camera>();
    //halfHeight = theCamera.orthographicSize;
    //halfWidth = halfHeight * Screen.width / Screen.height;
    //}
    //}

    void Update()
    {
        /*
        if (target.gameObject != null && bound != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            if (targetPosition.x + halfWidth > maxBound.x)
            {
                this.transform.position = new Vector3(maxBound.x - halfWidth, this.transform.position.y, this.transform.position.z);
            }
            else if (targetPosition.x - halfWidth < minBound.x)
            {
                this.transform.position = new Vector3(minBound.x + halfWidth, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                transform.position.Set(0, 2, -10);
            }
        }*/
    }

    public void Shake_()
    {
        originPos = transform.localPosition;
        StartCoroutine(Shake(0.3f,0.05f));
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
