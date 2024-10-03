using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordController : MonoBehaviour
{
    private TMP_InputField passwordInputfield;

    private void Start()
    {
        passwordInputfield = this.GetComponent<TMP_InputField>();
    }
    public void SeePassword()
    {
        passwordInputfield.contentType = TMP_InputField.ContentType.Standard;
        passwordInputfield.inputType = TMP_InputField.InputType.Standard;
        passwordInputfield.ForceLabelUpdate();
    }
    public void HidePassword()
    {
        passwordInputfield.contentType = TMP_InputField.ContentType.Password;
        passwordInputfield.inputType = TMP_InputField.InputType.Password;
        passwordInputfield.ForceLabelUpdate();
    }
}
