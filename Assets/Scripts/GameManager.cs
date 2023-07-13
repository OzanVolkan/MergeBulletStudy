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
    public bool isRunnig, isSingle, isTriple, isSizeUp;
    public float fireRate = 0.5f; // Ateþ hýzý (saniye cinsinden)

    private float fireTimer = 0f; // Ateþ zamanlayýcýsý
    private bool isFiring = false; // Ateþ durumu

    [Range(0f, 1f)] [SerializeField] float distance, radius;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnSave, new Action(OnSave));
        EventManager.AddHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnSave, new Action(OnSave));
        EventManager.RemoveHandler(GameEvent.OnShotPhase, new Action(OnShotPhase));
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

    IEnumerator SetScales()
    {
        yield return new WaitForSeconds(0.05f);
        foreach (var item in currentBullets)
        {
            //item.transform.localScale = Vector3.one;
        }
    }
    #region EVENTS
    public void OnShotPhase()
    {
        Transform targetTrans = gunList[2].transform;
        mergeCam.transform.DOMoveZ(targetTrans.position.z, 5f).SetEase(Ease.Linear);
        StartCoroutine(ReplaceGuns());
        StartCoroutine(SetScales());
        isSingle = true;
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
