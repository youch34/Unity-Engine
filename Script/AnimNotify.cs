using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimNotify : MonoBehaviour
{
    public GameObject Player;
    public string playerfile;
    public GameObject EM;
    public string emfile;
    public GameObject ED;
    public string edfile;
    private void Start()
    {
        AddNotify(Player,playerfile);
        AddNotify(EM, emfile);
        AddNotify(ED, edfile);

    }

    public void AddNotify(GameObject obj, string file) 
    {
        GameObject gobj = Instantiate(obj);
        gobj.GetComponent<Animator>().ReadFile(file);
        DestroyImmediate(gobj);
    }
}
