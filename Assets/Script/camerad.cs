using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerad : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject camTop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void camm1()
    {
        cam1.SetActive(false);
        camTop.SetActive(true);
        cam2.SetActive(true);
    }
    public void camm2()
    {
        cam1.SetActive(true);
        camTop.SetActive(true);
        cam2.SetActive(false);
    }public void camTOP()
    {
        cam1.SetActive(false);
        cam2.SetActive(false);
        camTop.SetActive(true);
    }
}
