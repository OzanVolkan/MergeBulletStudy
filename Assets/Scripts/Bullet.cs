using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private float dragSpeed = 15f;
    private bool hasDragged;

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePos = Input.mousePosition - GetMousePos();
        hasDragged = false;
    }

    private void OnMouseDrag()
    {
        transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos), Time.deltaTime * dragSpeed);
    }
    private void OnMouseUp()
    {
        hasDragged = true;
        UnsuitablePlace();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Grid") && other.transform.childCount == 0 && hasDragged)
        {
            print("Grid");
            transform.SetParent(other.transform);
            transform.DOLocalMove(Vector3.zero, 0.5f);
        }
        if (other.tag == tag && hasDragged)
        {
            print("Merge");
        }
    }

    private void UnsuitablePlace()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag != "Grid")
            {
                hasDragged = false;
                transform.DOLocalMove(Vector3.zero, 0.5f);
            }
        }
    }
}
