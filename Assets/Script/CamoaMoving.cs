using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CamoaMoving : MonoBehaviour
{
    public float speedmove = 15f;
    public bool isMoveh;
    public bool isMovev;
    public bool isMoveUD;
    public GameObject came;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveh)
        {
            //  transform.position += bar1 * scaleSpeed * Time.deltaTime;
            came.transform.position += new Vector3(10f, 0f, 0f) * speedmove * Time.deltaTime;
        }

        if (isMovev)
        {
            //  transform.position += bar1 * scaleSpeed * Time.deltaTime;
            came.transform.position += new Vector3(0f, 0f, 10f) * speedmove * Time.deltaTime;
        }
        
        if (isMoveUD)
        {
            //  transform.position += bar1 * scaleSpeed * Time.deltaTime;
            came.transform.position += new Vector3(0f, 10f, 0f) * speedmove * Time.deltaTime;
        }

        

    }

    public void moveH(float speed)
    {
        isMoveh = true;
        speedmove = speed;
    } 
    public void moveV(float speed)
    {
        isMovev = true;
        speedmove = speed;
    } public void moveUD(float speed)
    {
        isMoveUD = true;
        speedmove = speed;
    }
    public void stop()
    {
        isMoveh = false;
        isMovev = false;
        isMoveUD = false;
    }
}
