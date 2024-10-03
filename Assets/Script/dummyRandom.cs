using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dummyRandom : MonoBehaviour
{
    public GameObject bar,bar1,bar2,bar3,bar4;
    public GameObject barr,barr1,barr2,barr3,barr4;
    public Text mot, mob, tr, bs, spd;
    public Text mot1, mob1, tr1, bs1, spd1;
    private int a,b,c,d,e;
    Vector3 vector = new Vector3(-21.1547089f, 11.44f, 0f);
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            RandomValue();
            yield return new WaitForSeconds(2f);
            bar.transform.position = new Vector3(-326.399994f, a, -469f);
            bar1.transform.position = new Vector3(-318f, b, -469f);
            bar2.transform.position = new Vector3(-334.9f, c, -469f);
            bar3.transform.position = new Vector3(-342.9f, d, -469f);
            bar4.transform.position = new Vector3(-351.5f, e, -469f);

            barr.transform.position = new Vector3(-225.65007f, e, 969.699646f);
            barr1.transform.position = new Vector3(-234.050064f, d, 969.69928f);
            barr2.transform.position = new Vector3(-217.1501f, c, 969.69928f);
            barr3.transform.position = new Vector3(-209.1501f, b, 969.69928f);
            barr4.transform.position = new Vector3(-200.55f, a, 969.69928f);

            mot.text = a.ToString();
            mob.text = b.ToString();
            tr.text = c.ToString();
            bs.text = d.ToString();
            spd.text = e.ToString();
            
            mot1.text = e.ToString();
            mob1.text = d.ToString();
            tr1.text = c.ToString();
            bs1.text = b.ToString();
            spd1.text = a.ToString();
;        }

    }

    void RandomValue()
    {
        a = Random.Range(3, 20);
        b = Random.Range(4, 19);
        c = Random.Range(3, 20);
        d = Random.Range(3, 17);
        e = Random.Range(3, 18);
        vector.y = a;
    }
}
