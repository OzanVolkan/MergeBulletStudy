using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.isShield)
        {
            EventManager.Broadcast(GameEvent.OnFail);
            gameObject.SetActive(false);
        }
    }
}
