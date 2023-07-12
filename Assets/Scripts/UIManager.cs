using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    //public TextMeshProUGUI levelCounter;

    //[SerializeField] GameObject winPanel;
    //[SerializeField] GameObject failPanel;
    [SerializeField] GameObject mergePanel;
    //[SerializeField] GameObject greenBut, blueBut, redBut, hand;

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
        //InvokeRepeating("UIChecker", 0f, 0.1f);
    }

    private void UIChecker()
    {
        //levelCounter.text = "Level " + GameManager.Instance.gameData.levelCounter;
    }

    #region BUTTONS
    
    //public void NextButton()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    EventManager.Broadcast(GameEvent.OnNext);
    //    EventManager.Broadcast(GameEvent.OnSave);
    //}

    //public void RefreshButton()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    public void AddButton()
    {
        EventManager.Broadcast(GameEvent.OnAddBullet);
    }

    public void ShotButton()
    {
        EventManager.Broadcast(GameEvent.OnShotPhase);
    }
    #endregion

    public void OnShotPhase()
    {
        mergePanel.SetActive(false);
    }
}
