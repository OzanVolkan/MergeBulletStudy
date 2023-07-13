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
        if (GameManager.Instance.isSingle)
        {
            GameObject spawnBul = GetComponent<ObjectPooling>().GetPooledObject();
            spawnBul.transform.SetParent(null);

            if(!GameManager.Instance.isSizeUp)
            spawnBul.transform.localScale = Vector3.one * 0.25f;

            else spawnBul.transform.localScale = Vector3.one * 0.5f;

            spawnBul.transform.rotation = Quaternion.Euler(Vector3.zero);
            spawnBul.transform.position = transform.position;
            spawnBul.SetActive(true);
            Rigidbody bulRb = spawnBul.GetComponent<Rigidbody>();
            bulRb.AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
            StartCoroutine(DeactiveBullet(spawnBul, GameManager.Instance.range));
        }
        else if (GameManager.Instance.isTriple)
        {
            GameObject spawnBul1 = GetComponent<ObjectPooling>().GetPooledObject();
            spawnBul1.transform.SetParent(null);

            if (!GameManager.Instance.isSizeUp)
                spawnBul1.transform.localScale = Vector3.one * 0.25f;

            else spawnBul1.transform.localScale = Vector3.one * 0.5f;

            spawnBul1.transform.rotation = Quaternion.Euler(Vector3.zero);
            spawnBul1.transform.position = transform.position;
            spawnBul1.SetActive(true);
            Rigidbody bulRb1 = spawnBul1.GetComponent<Rigidbody>();
            bulRb1.AddForce((Quaternion.Euler(0f, -15f, 0f) * Vector3.forward) * bulletSpeed, ForceMode.Impulse);
            StartCoroutine(DeactiveBullet(spawnBul1, GameManager.Instance.range));

            GameObject spawnBul2 = GetComponent<ObjectPooling>().GetPooledObject();
            spawnBul2.transform.SetParent(null);

            if (!GameManager.Instance.isSizeUp)
                spawnBul2.transform.localScale = Vector3.one * 0.25f;

            else spawnBul2.transform.localScale = Vector3.one * 0.5f;

            spawnBul2.transform.rotation = Quaternion.Euler(Vector3.zero);
            spawnBul2.transform.position = transform.position;
            spawnBul2.SetActive(true);
            Rigidbody bulRb2 = spawnBul2.GetComponent<Rigidbody>();
            bulRb2.AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
            StartCoroutine(DeactiveBullet(spawnBul2, GameManager.Instance.range));

            GameObject spawnBul3 = GetComponent<ObjectPooling>().GetPooledObject();
            spawnBul3.transform.SetParent(null);

            if (!GameManager.Instance.isSizeUp)
                spawnBul3.transform.localScale = Vector3.one * 0.25f;

            else spawnBul3.transform.localScale = Vector3.one * 0.5f;

            spawnBul3.transform.rotation = Quaternion.Euler(Vector3.zero);
            spawnBul3.transform.position = transform.position;
            spawnBul3.SetActive(true);
            Rigidbody bulRb3 = spawnBul3.GetComponent<Rigidbody>();
            bulRb3.AddForce((Quaternion.Euler(0f, 15f, 0f) * Vector3.forward) * bulletSpeed, ForceMode.Impulse);
            StartCoroutine(DeactiveBullet(spawnBul3, GameManager.Instance.range));
        }
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
