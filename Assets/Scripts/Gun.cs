using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Gun : MonoBehaviour
{
    public int bulletLevel;
    public bool hasBullet;

    [SerializeField] private List<GameObject> shotBullets;
    [SerializeField] private float bulletSpeed;


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
        EventManager.AddHandler(GameEvent.OnShooting, new Action<float>(OnShooting));
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
        EventManager.RemoveHandler(GameEvent.OnShooting, new Action<float>(OnShooting));
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

    public void OnShooting(float rate)
    {
        GameObject spawnBul = GetComponent<ObjectPooling>().GetPooledObject();
        spawnBul.transform.SetParent(null);
        spawnBul.transform.localScale = Vector3.one * 0.25f;
        spawnBul.transform.rotation = Quaternion.Euler(Vector3.zero);
        spawnBul.transform.position = transform.position;
        spawnBul.SetActive(true);
        Rigidbody bulRb = spawnBul.GetComponent<Rigidbody>();
        bulRb.AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DeactiveBullet(spawnBul, GameManager.Instance.range));
    }

    public void OnShotPhase()
    {
        StartCoroutine(BulletCreation());
    }

    IEnumerator BulletCreation()
    {
        yield return new WaitForSeconds(5.25f);
        if (bulletLevel > 0)
            GetComponent<ObjectPooling>().CreateBullets(shotBullets[bulletLevel - 1]);
    }

    IEnumerator DeactiveBullet(GameObject bullet, float range)
    {
        yield return new WaitForSeconds(range);
        bullet.SetActive(false);
    }
}
