using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Gift : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedOnObject())
            {
                int ranInd = Random.Range(0, GameManager.Instance.bulletTypes.Count);
                GameObject randBullet = Instantiate(GameManager.Instance.bulletTypes[ranInd], transform.position, Quaternion.identity, transform.parent);
                GameManager.Instance.currentBullets.Add(randBullet);
                randBullet.transform.DOScale(Vector3.one * 4f, 0.5f).SetEase(Ease.OutBack);
                Destroy(gameObject);
            }
        }
    }

    private bool IsClickedOnObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;

            }
        }

        return false;
    }
}
