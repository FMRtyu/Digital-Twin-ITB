using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DummyDataKendaraan : MonoBehaviour
{
    public GameObject bar_motor;
    public GameObject bar_mobil;
    public GameObject bar_truk;
    public GameObject bar_bus;
    public GameObject bar_sepeda;
    public float a,b,c,d,e;
    public Text tmotor;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        while (true)
        {
            yield return new WaitForSeconds(5f);
            bar_motor.transform.position += new Vector3(37.3000031f, a, 0f);
            bar_mobil.transform.position += new Vector3(transform.position.x, b, transform.position.z);
            bar_truk.transform.position += new Vector3(transform.position.x, c, transform.position.z);
            bar_bus.transform.position += new Vector3(transform.position.x, d, transform.position.z);
            bar_sepeda.transform.position += new Vector3(transform.position.x, e, transform.position.z);
            /* bar_motor.transform.Translate(Vector3.up * a * Time.deltaTime);*/
            /* bar_motor.transform.position += new Vector3(0f, a, 0f);*/
            /*        tmotor = gameObject.AddComponent<Text>();
                        tmotor.text = a.ToString();*/
            RandomValue();
        }
    }
    void RandomValue()
    {
        a = Random.Range(30f, 120f);
        b = Random.Range(30f, 120f);
        c = Random.Range(30f, 120f);
        d = Random.Range(30f, 120f);
        e = Random.Range(30f, 120f);
        Debug.Log("Jml Motor :"+a+b+c+d+e);
    }
}
