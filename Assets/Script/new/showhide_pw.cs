using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class showhide_pw : MonoBehaviour
{
    public TMP_InputField passwordInput;
    public Button visibilityButton;
    public Sprite buka;
    public Sprite tutup;

    private bool isPasswordVisible = false;

    void Start()
    {
        visibilityButton.onClick.AddListener(TogglePasswordVisibility);
    }

    private void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;
        if (isPasswordVisible)
        {
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            passwordInput.inputType = TMP_InputField.InputType.Standard;
            visibilityButton.image.sprite = buka;
        }
        else
        {
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            passwordInput.inputType = TMP_InputField.InputType.Password;
            visibilityButton.image.sprite = tutup;
        }
        passwordInput.ForceLabelUpdate();
    }
}