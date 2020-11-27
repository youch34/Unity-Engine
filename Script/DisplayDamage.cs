using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamage : MonoBehaviour
{
    float Damage;
    Vector3 worldpos;
    public float damage { set { Damage = value; } }
    void Start()
    {
    }

    public void SetPos(Vector3 pos) 
    {
        GetComponent<Text>().text = Damage.ToString();
        worldpos = pos;
        Invoke("Init", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            transform.position = Camera.main.WorldToScreenPoint(worldpos);
            worldpos += Camera.main.transform.up * Time.deltaTime;
        }
    }


    public void Init()
    {
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
    }
}
