using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using StarterAssets;

public class KTPTriggerController : MonoBehaviour
{
    public GameObject canvasDialogMakeInput;
    private TextMeshProUGUI textDialog;
    public KTPDialogController dialogController;
    public KTPDialogPerbaruiController dialogPerbaruiController;
    public GameObject PlayerGameobject;
    private CinemachineFreeLook cameraThirdPerson;

    public bool isInDialog;
    // Start is called before the first frame update
    void Start()
    {
        isInDialog = false;
        textDialog = canvasDialogMakeInput.GetComponentInChildren<TextMeshProUGUI>();
        dialogController = canvasDialogMakeInput.GetComponentInChildren<KTPDialogController>();
    }

    private void OnTriggerStay(Collider other)
    {
        canvasDialogMakeInput.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Return) && !isInDialog)
        {
            isInDialog = true;
            PlayerGameobject = other.gameObject;
            textDialog.text = string.Empty;
            dialogController.isDialog = true;
            dialogController.startDialog();
            cameraThirdPerson = other.GetComponentInChildren<CinemachineFreeLook>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isInDialog = false;
        canvasDialogMakeInput.SetActive(false);
        PlayerGameobject = null;
        dialogController.ResetDialog();
        dialogPerbaruiController.ResetDialog();
    }

    public void IsInputingPlayer()
    {
        PlayerGameobject.GetComponent<ThirdPersonController>().enabled = false;
        PlayerGameobject.GetComponent<ThirdPersonController>().isInputing = true;
        PlayerGameobject.GetComponent<CharacterController>().enabled = false;
    }
    public void IsNotInputing()
    {
        PlayerGameobject.GetComponent<ThirdPersonController>().enabled = true;
        PlayerGameobject.GetComponent<ThirdPersonController>().isInputing = false;
        PlayerGameobject.GetComponent<CharacterController>().enabled = true;
    }
}
