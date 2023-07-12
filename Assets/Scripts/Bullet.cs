using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class Bullet : MonoBehaviour
{
    public int listIndex;
    public int bulletLevel;
    public bool isDestroyable;

    private Vector3 mousePos;
    private float dragSpeed = 15f;
    private bool hasDragged, canDrag;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
    }
    private void Start()
    {
        canDrag = true;
        bulletLevel = ExtractLevelFromTag();   
    }
    private int ExtractLevelFromTag()
    {
        string tag = gameObject.tag;
        string numberString = "";

        for (int i = 0; i < tag.Length; i++)
        {
            if (char.IsDigit(tag[i]))
            {
                numberString += tag[i];
            }
        }

        int number = 0;
        int.TryParse(numberString, out number);

        return number;
    }
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        if (canDrag)
        {
            mousePos = Input.mousePosition - GetMousePos();
            hasDragged = false;
        }
    }

    private void OnMouseDrag()
    {
        if(canDrag)
        transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos), Time.deltaTime * dragSpeed);
    }
    private void OnMouseUp()
    {
        if (canDrag)
        {
            hasDragged = true;
            Replace();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Destroy(other.gameObject);
        }    
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
        canDrag = false;
        listIndex = transform.parent.GetSiblingIndex();
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        Transform targetGun = GameManager.Instance.gunList[listIndex].transform;
        transform.DOMoveZ(targetGun.position.z, 5f).SetEase(Ease.Linear);
    }
}
