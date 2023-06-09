﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;
    private Vector3 inputVector;

    // Start is called before the first frame update
    void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>(); // 나중에 inchildren으로 바꿔보자 // 바꿀필요 없다 이게더 성능 좋다.
    }

public virtual void OnDrag(PointerEventData ped)
    {
        //ped.pressEventCamer
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, null, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            //pos.y = (pos.x / bgImg.rectTransform.sizeDelta.y);
            pos.y = 0;

            //inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
            inputVector = new Vector3(pos.x * 2, 0, 0);

            inputVector = (inputVector.magnitude > 0.759f) ? inputVector.normalized * 0.759f : inputVector;

            //joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3)
            //    , inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));

            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3), 0);
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float GetHorizontalValue()
    {
        return inputVector.x;
    }

    public void ResetInputVector()
    {
        inputVector = Vector3.zero;
    }
}
