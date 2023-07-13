using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinishBox : MonoBehaviour
{
    public TextMeshPro boxCountText;
    public int boxValue;
                
    [SerializeField] GameObject coin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShotBullet"))
        {
            if (boxValue > 0)
            {
                boxValue -= 1;
                boxValue = Mathf.Clamp(boxValue, 0, 25000);
                other.gameObject.SetActive(false);
            }
            else if (boxValue <= 0)
            {
                coin.transform.SetParent(null);
                coin.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }

        }
        if (other.CompareTag("Player") && boxValue > 0)
        {
            EventManager.Broadcast(GameEvent.OnWin);
        }
    }

    private void Update()
    {
        boxCountText.text = boxValue.ToString();
    }
}
