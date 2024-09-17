using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class getInputfield : MonoBehaviour
{

    public Button btnClick;

    public TMP_InputField input_us;
    public TMP_InputField input_pw;
    // Start is called before the first frame update
    private void Start()
    {
        btnClick.onClick.AddListener(GetInput);
    }

    // Update is called once per frame
    public void GetInput()
    {
        Debug.Log("Username :"+input_us.text);
        Debug.Log("Password :"+input_pw.text);
    }
}
