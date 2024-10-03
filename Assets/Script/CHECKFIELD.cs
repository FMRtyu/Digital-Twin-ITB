using UnityEngine;
using UnityEngine.UI;

public class CHECKFIELD : MonoBehaviour
{
    public Button button;
    public InputFieldCheck validateAllInputFields;

    private void Start()
    {
        button.onClick.AddListener(delegate { validateAllInputFields.ValidateInputFields(); });
    }
}
