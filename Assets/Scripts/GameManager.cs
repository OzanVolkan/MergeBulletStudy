using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : SingletonManager<GameManager>
{
    public GameData gameData;
    
    private void OnEnable()
    {
        
        EventManager.AddHandler(GameEvent.OnSave, new Action(OnSave));
    }

    private void OnDisable()
    {
        
        EventManager.RemoveHandler(GameEvent.OnSave, new Action(OnSave));
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
