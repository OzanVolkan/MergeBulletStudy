using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int bulletLevel;
    public bool hasBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            hasBullet = true;
            int tempLvl = other.GetComponent<Bullet>().bulletLevel;
            if (tempLvl > bulletLevel) bulletLevel = tempLvl;
            Destroy(other.gameObject);
        }
    }

    
}
