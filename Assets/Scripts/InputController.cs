using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Transform playerTrans;
    [SerializeField] private float movementSensitivity, leftMovementLimit, rightMovementLimit;
    [SerializeField] private float movementSpeed;

    private bool canMove;
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    private void Update()
    {
        if (canMove)
        {
            playerTrans.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.isRunnig)
        {
            canMove = true;
            Vector3 tempPosition = playerTrans.position;
            tempPosition.x = Mathf.Clamp(tempPosition.x + (eventData.delta.x / movementSensitivity), leftMovementLimit, rightMovementLimit);
            playerTrans.position = tempPosition;
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameManager.Instance.isRunnig) canMove = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(GameManager.Instance.isRunnig) canMove = false;
    }
}
