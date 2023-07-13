using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Gold,

    Shield
}
public class Collectable : MonoBehaviour
{
    public CollectableType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case CollectableType.Gold:
                    EventManager.Broadcast(GameEvent.OnGoldCollect);
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(1).transform.SetParent(null);
                    gameObject.SetActive(false);
                    break;
                case CollectableType.Shield:
                    EventManager.Broadcast(GameEvent.OnShield);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
