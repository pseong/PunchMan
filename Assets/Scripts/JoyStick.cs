using System.Collections;
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
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, null, out pos))
        {
            pos.x -= 150;
            if (pos.x <= -75) pos.x = -75;
            if (pos.x >= 75) pos.x = 75;
            joystickImg.rectTransform.anchoredPosition = new Vector2(pos.x, 0);
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
