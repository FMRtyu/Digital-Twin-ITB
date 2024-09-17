using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleSelected : MonoBehaviour
{
    [SerializeField] private Toggle toggle1, toggle2;
    Input_Pendataan input_Pendataan;
    
    public void ToggleSelect(string tg, string value)
    {
        if (tg.Equals("jenkel"))
        {
            input_Pendataan.jenkel = value;
        }

        if (tg.Equals("kewarganegaraan"))
        {
            input_Pendataan.kewarganegaraan = value;
        }


        Debug.Log("data Selected toggle : " + value);
    }



}
