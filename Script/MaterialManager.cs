using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    SkinnedMeshRenderer[] meshRenderers;
    List<Material> Origin = new List<Material>();
    public List<Material> outlineMats;
    bool isOutLine = false;
    bool bDamaged = false;
    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        for(int i = 0; i < meshRenderers.Length; i++) 
        {
            for (int j = 0; j < meshRenderers[i].materials.Length; j++)
            {
                Origin.Add(meshRenderers[i].materials[j]);
            }     
        }
    }

    public void SetHitMaterial() 
    {
        bDamaged = true;

    }

    public void SetOutline(bool val) 
    {
        if(isOutLine.Equals(val))
            return;
        isOutLine = val;
        if (val.Equals(true))
        {
            int index = 0;
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                if (meshRenderers[i].materials.Length > 1)
                {
                    Material[] mats = new Material[meshRenderers[i].materials.Length];
                    for (int j = 0; j < meshRenderers[i].materials.Length; j++)
                    {
                        mats[j] = outlineMats[index];
                        index++;
                    }
                    meshRenderers[i].materials = mats;
                }
                else 
                {
                    meshRenderers[i].material = outlineMats[index];
                    index++;
                }
            }
        }
        else
        {
            int index = 0;
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                if (meshRenderers[i].materials.Length > 1)
                {
                    Material[] mats = new Material[meshRenderers[i].materials.Length];
                    for (int j = 0; j < meshRenderers[i].materials.Length; j++)
                    {
                        mats[j] = Origin[index];
                        index++;
                    }
                    meshRenderers[i].materials = mats;
                }
                else
                {
                    meshRenderers[i].material = Origin[index];
                    index++;
                }
            }
        }
    }
}
