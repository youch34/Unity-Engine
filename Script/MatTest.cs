using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatTest : MonoBehaviour
{
    public Material[] materials;
    public Material Origin;
    MeshRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        Origin = render.material;
        render.material = materials[0];
        StartCoroutine("Change");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Change() 
    {
        yield return new WaitForSeconds(1.0f);
        render.material = materials[1];
        yield return new WaitForSeconds(1.0f);
        render.material = Origin;
        yield return new WaitForSeconds(1.0f);
        render.material = materials[0];
        StartCoroutine("Change");
    }
}
