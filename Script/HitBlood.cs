using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HitBlood : MonoBehaviour
{
    ParticleSystem[] particles;
    Transform Pool;
    private void Start()
    {
        particles = transform.GetComponentsInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (IsStopped())
            Init();
    }
    public void Play() 
    {
        foreach (ParticleSystem p in particles)
            p.Play();
    }
    public bool IsStopped() 
    {
        foreach (ParticleSystem p in particles)
        {
            if (p.IsAlive())
                return false;
        }
        return true;
    }

    public void Init() 
    {
        transform.gameObject.SetActive(false);
        transform.position = Vector3.zero;
    }
}
