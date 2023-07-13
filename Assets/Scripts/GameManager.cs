using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class GameManager : SingletonManager<GameManager>
{
    public GameData gameData;
    public GameObject mergeCam, runnerCam;
    public List<GameObject> bulletTypes;
    public List<GameObject> gunList;
    public List<GameObject> currentBullets;
    public float rate, range;
    public bool isRunnig, isSingle, isTriple, isSizeUp, isShield;
    public float fireRate = 0.5f; // Ateþ hýzý (saniye cinsinden)

    private float missileSpeed = 7.5f, shieldTime = 2.5f;
    private float fireTimer = 0f; // Ateþ zamanlayýcýsý
    private bool isFiring = false; // Ateþ durumu

    [Range(0f, 1f)] [SerializeField] float distance, radius;
    [SerializeField] GameObject missilePrefab;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnSave, new Action(OnSave));
        EventManager.AddHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
        EventManager.AddHandler(GameEvent.OnShield, new Action(OnShield));
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnSave, new Action(OnSave));
        EventManager.RemoveHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
        EventManager.RemoveHandler(GameEvent.OnShield, new Action(OnShield));
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
        if (isRunnig)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isFiring = true;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                isFiring = false;
            }

            // Ateþ hýzýný kontrol etme
            if (isFiring)
            {
                fireTimer += Time.deltaTime;

                if (fireTimer >= fireRate)
                {
                    Fire();
                    fireTimer = 0f;
                }
            }
            else
            {
                fireTimer = 0f;
            }
        }

    }
    private void Fire()
    {
        EventManager.Broadcast(GameEvent.OnShooting, rate);
    }

    IEnumerator ReplaceGuns()
    {
        yield return new WaitForSeconds(5.25f);
        for (int i = 0; i < gunList.Count; i++)
        {
            if (!gunList[i].GetComponent<Gun>().hasBullet)
            {
                GameObject tempGun = gunList[i];
                Destroy(gunList[i]);
            }
        }
        yield return new WaitForSeconds(0.1f);
        GameObject[] currentGuns = GameObject.FindGameObjectsWithTag("Gun");
        yield return new WaitForSeconds(0.1f);
        ReplaceGuns(currentGuns);
        mergeCam.SetActive(false);
        runnerCam.SetActive(true);
        StartCoroutine(MissileLaunch());
    }
    public void ReplaceGuns(GameObject[] guns)
    {
        if (guns.Length > 0)
        {
            for (int i = 0; i < guns.Length; i++)
            {
                float x = distance * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
                float z = distance * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

                Vector3 newPos = new Vector3(x, 0.8335806f, z);

                guns[i].transform.DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);
            }
        }
        isRunnig = true;
    }
    IEnumerator MissileLaunch()
    {
        Transform spawnTrans = GameObject.Find("FinishLine").transform;
        Transform targetTrans = GameObject.Find("StartLine").transform;
        float randX = targetTrans.position.x + UnityEngine.Random.Range(-1f, 1f);
        float randY = targetTrans.position.y + UnityEngine.Random.Range(0.15f, 0.4f);
        Vector3 spawnPos = new Vector3(randX, randY, spawnTrans.position.z);
        GameObject newMissile = Instantiate(missilePrefab, spawnPos, Quaternion.identity);
        newMissile.transform.DOMoveZ(targetTrans.position.z, missileSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(newMissile);
        });
        float randomInterval = UnityEngine.Random.Range(3f, 6f);

        yield return new WaitForSeconds(randomInterval);
        StartCoroutine(MissileLaunch());
    }

    IEnumerator ShieldProtection()
    {
        isShield = true;
        yield return new WaitForSeconds(shieldTime);
        isShield = false;
    }

    #region EVENTS
    public void OnShotPhase()
    {
        Transform targetTrans = gunList[2].transform;
        mergeCam.transform.DOMoveZ(targetTrans.position.z, 5f).SetEase(Ease.Linear);
        StartCoroutine(ReplaceGuns());
        isSingle = true;
    }

    public void OnShield()
    {
        StartCoroutine(ShieldProtection());
    }

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
