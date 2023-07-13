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
        EventManager.AddHandler(GameEvent.OnSaveBullets, new Action(OnSaveBullets));
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnAddBullet, new Action(OnAddBullet));
        EventManager.RemoveHandler(GameEvent.OnSaveBullets, new Action(OnSaveBullets));
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


        gameData.bulletsIndexes = new List<Tuple<int, int>>();
        if (gameData.bulletsIndexes.Count < 1) return;

        for (int i = 0; i < gridSlots.Count; i++)
        {
            for (int j = 0; j < gameData.bulletsIndexes.Count; j++)
            {
                Tuple<int, int> item = gameData.bulletsIndexes[j];
                int index = item.Item1;
                int level = item.Item2;

                if (i == index)
                {
                    Transform spawnTrans = gridSlots[i];
                    GameObject spawnObj = GameManager.Instance.bulletTypes[level - 1];
                    Instantiate(spawnObj, spawnTrans.position, Quaternion.identity, spawnTrans);
                }
            }
        }
    }

    private void OnAddBullet()
    {
        if (GameManager.Instance.gameData.money >= 100f)
        {
            for (int i = 0; i < gridSlots.Count; i++)
            {
                Transform currentGrid = gridSlots[i];

                if (currentGrid.childCount == 0 && currentGrid.gameObject.activeInHierarchy)
                {
                    GameManager.Instance.gameData.Money -= 100;
                    GameObject newBullet = Instantiate(bulletObj, currentGrid.position, Quaternion.identity, currentGrid);
                    GameManager.Instance.currentBullets.Add(newBullet);
                    newBullet.transform.DOScale(Vector3.one * 4f, 0.5f).SetEase(Ease.OutBack);

                    return;
                }
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

    public void OnSaveBullets()
    {
        gameData.bulletsIndexes = new List<Tuple<int, int>>();

        gameData.bulletsIndexes.Clear();

        for (int i = 0; i < gridSlots.Count; i++)
        {
            if (gridSlots[i].transform.childCount > 0)
            {
                if (gridSlots[i].GetComponent<Bullet>())
                {
                    int level = gridSlots[i].GetChild(0).GetComponent<Bullet>().bulletLevel;

                    Tuple<int, int> tuple = Tuple.Create(i, level);
                    gameData.bulletsIndexes.Add(tuple);
                }
            }
        }
    }
}
