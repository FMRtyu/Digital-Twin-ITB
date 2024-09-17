using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown_Script : MonoBehaviour
{
    public TMP_InputField input_TANGGAL;
    public TMP_InputField input_TEMPAT;
    public TMP_InputField input_GOLDAR;
    //public TMP_InputField input_JENKEL;
    //public TMP_InputField input_STATUS;


    public TextMeshProUGUI output_GOLDAR;
    public TextMeshProUGUI output_JK;
    public TextMeshProUGUI output_STATUS;
    public TextMeshProUGUI output_TEMPAT;
    public TextMeshProUGUI output_TANGGAL;

    public void HandleInputDataJK(int val)
    {
        if (val == 0)
        {
            output_JK.text = "Pria";
        }
        if (val == 1)
        {
            output_JK.text = "Wanita";
        }
    } 
    public void HandleInputDataSTATUS(int val)
    {
        if (val == 0)
        {
            output_STATUS.text = "Menikah";
        }
        if (val == 1)
        {
            output_STATUS.text = "Belum Menikah";
        }
    }
    public void insertData()
    {
        output_GOLDAR.text = input_GOLDAR.text;
        output_TANGGAL.text = input_TANGGAL.text;
        output_TEMPAT.text = input_TEMPAT.text;
    }
}
