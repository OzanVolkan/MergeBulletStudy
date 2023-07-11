using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class GridManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject bulletObj, gift;

    public List<Transform> gridSlots = new List<Transform>();

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnAddBullet, new Action(OnAddBullet));
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnAddBullet, new Action(OnAddBullet));
    }

    void Start()
    {
        GridControl(gameData.money);
        AddGift(gift);
    }

    private void GridControl(int moneyAmount)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (moneyAmount / 100 >= i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private void OnAddBullet()
    {
        for (int i = 0; i < gridSlots.Count; i++)
        {
            Transform currentGrid = gridSlots[i];

            if (currentGrid.childCount == 0 && currentGrid.gameObject.activeInHierarchy)
            {
                GameObject newBullet = Instantiate(bulletObj, currentGrid.position, Quaternion.identity, currentGrid);
                GameManager.Instance.currentBullets.Add(newBullet);
                newBullet.transform.DOScale(Vector3.one * 4f, 0.5f).SetEase(Ease.OutBack);

                return;
            }
        }
    }

    private void AddGift(GameObject giftObj)
    {
        List<Transform> currentSlots = new List<Transform>();

        foreach (var item in gridSlots)
        {
            if (item.gameObject.activeInHierarchy) currentSlots.Add(item);
        }

        int tempIndex = UnityEngine.Random.Range(0, currentSlots.Count);

        if (currentSlots[tempIndex].childCount == 0)
        {
            Transform giftGrid = currentSlots[tempIndex];
            GameObject newGift = Instantiate(giftObj, giftGrid.position, Quaternion.identity, giftGrid);
            newGift.transform.DOScale(Vector3.one * 4f, 0.5f).SetEase(Ease.OutBack);

            return;
        } 
        else AddGift(giftObj);
    }
}
