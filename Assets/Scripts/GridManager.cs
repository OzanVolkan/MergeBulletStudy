using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject bulletObj;

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
            if (gridSlots[i].childCount == 0)
            {
                Instantiate(bulletObj, gridSlots[i].transform.position, Quaternion.identity);
            }
        }
    }
}
