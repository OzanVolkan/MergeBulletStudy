using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class Bullet : MonoBehaviour
{
    public int listIndex;
    public int verticalIndex;
    public bool isDestroyable;

    private Vector3 mousePos;
    private float dragSpeed = 15f;
    private bool hasDragged;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
    }
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
        Replace();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == tag && hasDragged)
        {
            MergeBullets(other);
        }
    }

    private void Replace()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 8))
        {
            if (hit.transform.tag != "Grid")
            {
                hasDragged = false;
                transform.DOLocalMove(Vector3.zero, 0.5f);
            }
            else if (hit.transform.CompareTag("Grid") && hit.transform.childCount == 0 && hasDragged)
            {
                transform.SetParent(hit.transform);
                transform.DOLocalMove(Vector3.zero, 0.5f);
            }
            
            else transform.DOLocalMove(Vector3.zero, 0.15f);
        }
    }

    private void MergeBullets(Collider other)
    {
        for (int i = 0; i < GameManager.Instance.bulletTypes.Count; i++)
        {
            if (i <= 2)
            {
                var currentType = GameManager.Instance.bulletTypes[i];
                var nextType = GameManager.Instance.bulletTypes[i + 1];

                if (currentType.tag == tag)
                {
                    tag = "Untagged";
                    GameObject newBullet = Instantiate(nextType, other.transform.position, Quaternion.identity, other.transform.parent);
                    GameManager.Instance.currentBullets.Remove(gameObject);
                    GameManager.Instance.currentBullets.Remove(other.gameObject);
                    GameManager.Instance.currentBullets.Add(newBullet);
                    newBullet.transform.localScale = Vector3.one * 4f;
                    newBullet.transform.DOLocalMove(Vector3.zero, 0.15f);

                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void OnShotPhase()
    {
        listIndex = transform.parent.GetSiblingIndex();
        verticalIndex = transform.parent.transform.parent.GetSiblingIndex();
        transform.SetParent(null);
        Transform targetGun = GameManager.Instance.gunList[listIndex].transform;
        transform.DOMoveZ(targetGun.position.z, 5f);

    }
}
