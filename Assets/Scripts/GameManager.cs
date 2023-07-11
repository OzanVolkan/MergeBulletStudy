using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : SingletonManager<GameManager>
{
    public GameData gameData;
    public List<GameObject> bulletTypes;
    public List<GameObject> gunList;
    public List<GameObject> currentBullets;

    private void OnEnable()
    {
        
        EventManager.AddHandler(GameEvent.OnSave, new Action(OnSave));
        EventManager.AddHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));

    }

    private void OnDisable()
    {
        
        EventManager.RemoveHandler(GameEvent.OnSave, new Action(OnSave));
        EventManager.RemoveHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));

    }
    private void Awake()
    {
        //OnLoad();
    }
    private void Start()
    {


    }
    private void Update()
    {
        
    }

    private void OnShotPhase()
    {
        List<GameObject> side0 = new List<GameObject>();
        List<GameObject> side1 = new List<GameObject>();
        List<GameObject> side2 = new List<GameObject>();
        List<GameObject> side3 = new List<GameObject>();
        List<GameObject> side4 = new List<GameObject>();

        for (int i = 0; i < currentBullets.Count; i++)
        {
            if (currentBullets[i].GetComponent<Bullet>().listIndex == 0)
            {
                side0.Add(currentBullets[i]);
            }
            else if (currentBullets[i].GetComponent<Bullet>().listIndex == 1)
            {
                side1.Add(currentBullets[i]);
            }
            else if (currentBullets[i].GetComponent<Bullet>().listIndex == 2)
            {
                side2.Add(currentBullets[i]);
            }
            else if (currentBullets[i].GetComponent<Bullet>().listIndex == 3)
            {
                side3.Add(currentBullets[i]);
            }
            else if (currentBullets[i].GetComponent<Bullet>().listIndex == 4)
            {
                side4.Add(currentBullets[i]);
            }
        }

        int max0 = side0[0].GetComponent<Bullet>().listIndex;
        for (int i = 1; i < side0.Count; i++)
        {
            if (side0[i].GetComponent<Bullet>().listIndex > max0)
            {
                max0 = side0[i].GetComponent<Bullet>().listIndex;
            }
        }
        foreach (var item in side0)
        {
            if (item.GetComponent<Bullet>().listIndex != max0)
            {
                item.GetComponent<Bullet>().isDestroyable = true;
            }
        }
    }

    #region EVENTS


    #endregion

    void OnSave()
    {
        SaveManager.SaveData(gameData);
    }

    void OnLoad()
    {
#if !UNITY_EDITOR
        SaveManager.LoadData(gameData);
#endif
    }
    public void OnApplicationQuit()
    {
        OnSave();
    }
    public void OnApplicationFocus(bool focus)
    {
        if (focus == false) OnSave();
    }
    public void OnApplicationPause(bool pause)
    {
        if (pause == true) OnSave();
    }
}
