using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objRotation : MonoBehaviour
{
    public Vector3 kecepatan_putar;
    public bool berputar = false;
    // Start is called before the first frame update
    void Update()
    {
        if (berputar)
        {
            transform.Rotate(
                kecepatan_putar.x * Time.deltaTime * 10,
                    kecepatan_putar.y * Time.deltaTime * 10,
                    kecepatan_putar.z * Time.deltaTime * 10

            );
        }
    }

}
