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
                    break;
                case CollectableType.Shield:
                    EventManager.Broadcast(GameEvent.OnShield);
                    break;
            }
        }
    }
}
