using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum GateType
{
    FireRate,

    Range,

    TripleShot,

    SizeUp
}
public class Gate : MonoBehaviour
{
    public GateType type;
    public TextMeshPro gateText;
    public float gateVal;

    [SerializeField] private Material[] gateMaterials;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        InvokeRepeating(nameof(MaterialSet), 1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (type == GateType.FireRate)
        {
            gateText.text = "Fire Rate\n" + gateVal;
        }
        if (type == GateType.Range)
        {
            gateText.text = "Range\n" + gateVal;
        }
    }
    
    private void MaterialSet()
    {
        if (type == GateType.FireRate || type == GateType.Range)
        {
            if (gateVal >= 0)
            {
                Material[] materials = meshRenderer.materials;
                materials[0] = gateMaterials[0];
                meshRenderer.materials = materials;
            }
            else if (gateVal < 0)
            {
                Material[] materials = meshRenderer.materials;
                materials[0] = gateMaterials[1];
                meshRenderer.materials = materials;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (type)
        {
            case GateType.FireRate:
                if (other.CompareTag("ShotBullet"))
                {
                    gateVal += 1f;
                    other.gameObject.SetActive(false);
                }
                if (other.CompareTag("Player"))
                {
                    float value = gateVal / 1000f;
                    GameManager.Instance.rate -= value;
                    gameObject.SetActive(false);
                }
                break;
            case GateType.Range:
                if (other.CompareTag("ShotBullet"))
                {
                    gateVal += 1f;
                    other.gameObject.SetActive(false);
                }
                if (other.CompareTag("Player"))
                {
                    float value = gateVal / 500f;
                    GameManager.Instance.range += value;
                    gameObject.SetActive(false);
                }
                break;
            case GateType.TripleShot:
                if (other.CompareTag("Player"))
                {
                    GameManager.Instance.isSingle = false;
                    GameManager.Instance.isTriple = true;
                }
                break;
            case GateType.SizeUp:
                if (other.CompareTag("Player"))
                {
                    GameManager.Instance.isSizeUp = true;
                }
                break;
        }
    }
}
