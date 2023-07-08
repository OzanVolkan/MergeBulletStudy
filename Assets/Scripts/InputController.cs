using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (canDrag)
        //{
        //    isMoving = true;
        //    Vector3 tempPosition = playerTransform.localPosition;
        //    tempPosition.x = Mathf.Clamp(tempPosition.x + (eventData.delta.x / movementSensitivity), leftMovementLimit, rightMovementLimit);
        //    playerTransform.localPosition = tempPosition;
        //}
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
