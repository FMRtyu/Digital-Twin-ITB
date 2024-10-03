using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class formControl : MonoBehaviour
{
    public GameObject tglLahir;
    public GameObject tmpLahir;
    public GameObject jk;
    public GameObject goldar;
    public GameObject status;


    public GameObject Lahir;
    public GameObject tLahir;
    public GameObject jkl;
    public GameObject golda;
    public GameObject stat;

    public string a, b, c, d, e;


    // Start is called before the first frame update
    // Update is called once per frame
   public void sendData()
    {
        a = tglLahir.GetComponent<Text>().text;
        b = tmpLahir.GetComponent<Text>().text;
        c = jk.GetComponent<Text>().text;
        d = goldar.GetComponent<Text>().text;
        e = status.GetComponent<Text>().text;

        Debug.Log(a);

        Lahir.GetComponent<Text>().text = a;
        tLahir.GetComponent<Text>().text = b;
        jkl.GetComponent<Text>().text = c;
        golda.GetComponent<Text>().text = d;
        stat.GetComponent<Text>().text = e;

    }
}
