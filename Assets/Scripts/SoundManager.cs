using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip dropSound, winSound, failSound;
    public float pitchDecreaseAmount = 0.1f;

    private void OnEnable()
    {
        //EventManager.AddHandler(GameEvent.OnWin, new Action(OnWin));
        //EventManager.AddHandler(GameEvent.OnFail, new Action(OnFail));
    }
    private void OnDisable()
    {
        //EventManager.RemoveHandler(GameEvent.OnWin, new Action(OnWin));
        //EventManager.RemoveHandler(GameEvent.OnFail, new Action(OnFail));
    }
    private void Start()
    {
        //audioSource.pitch = 1f; // Baþlangýç ton yüksekliði
    }
    //void OnWin()
    //{
    //    audioSource.PlayOneShot(winSound);
    //}
    //void OnFail()
    //{
    //    audioSource.PlayOneShot(failSound);
    //}

}
