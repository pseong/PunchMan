using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)32 / 17); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }

    //void OnPreCull() => GL.Clear(true, true, Color.black);
    void Awake()
    {/*
        float targetWidthAspect = 16.0f;
        float targetHeightAspect = 9.0f;

        Camera camera = GetComponent<Camera>();

        camera.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHeightAspect;

        float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
        float widthtadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

        if (heightRatio > widthRatio)
            widthtadd = 0.0f;
        else
            heightadd = 0.0f;

        camera.rect = new Rect(
            camera.rect.x + Mathf.Abs(widthtadd),
            camera.rect.y + Mathf.Abs(heightadd),
            camera.rect.width + (widthtadd * 2),
            camera.rect.height + (heightadd * 2));
    */
    }
}
