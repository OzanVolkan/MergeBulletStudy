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
    public int gateVal;

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
}
