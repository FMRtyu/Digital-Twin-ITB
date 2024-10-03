using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldCheck : MonoBehaviour
{
    public TMP_InputField[] inputFields;

    public void ValidateInputFields()
    {
        bool allInputFieldsValid = true;

        foreach (TMP_InputField inputField in inputFields)
        {
            if (string.IsNullOrWhiteSpace(inputField.text))
            {
                allInputFieldsValid = false;
                inputField.GetComponent<Image>().color = Color.red;
                inputField.transform.Find("Text").GetComponent<Text>().text = "Mohon Lengkapi Data";
            }
            else
            {
                inputField.GetComponent<Image>().color = Color.white;
                inputField.transform.Find("Text").GetComponent<Text>().text = "";
            }
        }

        if (allInputFieldsValid)
        {
            Debug.Log("Semua input field valid!");
        }
    }
}
