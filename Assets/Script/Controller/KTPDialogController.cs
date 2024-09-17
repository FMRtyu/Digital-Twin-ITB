using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
public class KTPDialogController : MonoBehaviour
{
    //gameobject
    public TextMeshProUGUI textDialog;
    public GameObject[] inputField;
    public GameObject klikText;
    public KTPTriggerController ktpTrigger;
    public GameObject klikDimanaSajaUI;
    public TMP_InputField namaSetTTD;
    public GameObject tddGameobject;
    public GameObject cameraMain;

    //dialog
    private DialogKTP dialogKTP;
    public float textSpeed;

    //NPC animation
    public Animator animasiNPC;

    //list dialog
    public string[] KTPDialog;

    //index & kondisi
    public int index;
    public bool isDialog;
    public bool isInputing;
    public bool isInputed;
    private bool isAllowedToClick;
    private int inputFieldIndex;

    public GameManager gameManager;

    public KTPDialogPerbaruiController ktpPerbarui;
    // Start is called before the first frame update
    void Start()
    {
        isInputed = false;
        gameManager.jenisKelamin = "Laki - laki";
        gameManager.kewarganegaraan = "WNI";
        dialogKTP = new DialogKTP();
        KTPDialog = dialogKTP.DialogListGet();

        textDialog.text = "Halo, <b>" + gameManager.namaPemain + "</b>! Selamat datang di stan Dukcapil. Silahkan tekan <b>ENTER</b> untuk melakukan pendataan identitas penduduk.";
        isDialog = false;
        isInputing = false;
        isAllowedToClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDialog && !isInputing && !isInputed && isAllowedToClick)
        {
            if (textDialog.text == KTPDialog[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textDialog.text = KTPDialog[index];
                animasiNPC.SetBool("Talking", false);
            }
        }
    }
    public void ResetDialog()
    {
        index = 0;
        inputFieldIndex = 0;
        StopAllCoroutines();
        textDialog.text = "Halo, <b>"+ gameManager.namaPemain + "!</b> Selamat datang di stan Dukcapil. Silahkan tekan <b>ENTER</b> untuk melakukan pendataan identitas penduduk.";
        isDialog = false;
        isInputing = false;
        isAllowedToClick = true;
        ktpTrigger.isInDialog = false;
        isInputed = false;
        klikDimanaSajaUI.SetActive(false);
        Application.ExternalEval("ResetAllInput();");
    }

    public void startDialog()
    {
        index = 0;
        inputFieldIndex = 0;
        klikText.SetActive(true);
        klikDimanaSajaUI.SetActive(true);
        StartCoroutine(TypeDialog());

    }

    IEnumerator TypeDialog()
    {
        foreach (char c in KTPDialog[index].ToCharArray())
        {
            textDialog.text += c;
            yield return new WaitForSeconds(textSpeed);
            if (textDialog.text == KTPDialog[index])
            {
                animasiNPC.SetBool("Talking", false);
            }
            else
            {
                animasiNPC.SetBool("Talking", true);
            }
        }
    }
    private void nextLine()
    {
        StopAllCoroutines();
        index += 1;
        Debug.Log("next line is : " + KTPDialog[index]);
        if (index < KTPDialog[index].Length - 1 && KTPDialog[index] != "break")
        {
            textDialog.text = string.Empty;
            StartCoroutine(TypeDialog());
        }else if (KTPDialog[index] == "break")
        {
            if (index == 1)
            {
                StopAllCoroutines();
                isAllowedToClick = false;
                StartCoroutine(CekStatusPendaftaran());
            }
            else if (index == 13)
            {
                StopAllCoroutines();
                ktpTrigger.IsInputingPlayer();
                isInputing = true;
#if !UNITY_EDITOR && UNITY_WEBGL
        Application.ExternalCall("toggleFullscreen");
#endif
                tddGameobject.SetActive(true);
                cameraMain.SetActive(false);
            }
            else
            {
                ktpTrigger.IsInputingPlayer();
                inputField[inputFieldIndex].SetActive(true);
                isInputing = true;
            }
            
        }
        else if (KTPDialog[index] == "back")
        {
            textDialog.text = string.Empty;
            backIntoIt();
        }
        else if (KTPDialog[index] == "end")
        {
            ResetDialog();
        }
    }
    public void backIntoIt()
    {
        StopAllCoroutines();
        index = 2;
        inputFieldIndex = 0;
        isDialog = true;
        isInputing = false;
        isAllowedToClick = true;
        StartCoroutine(TypeDialog());
    }
    public void PreviousDialog()
    {
        inputField[inputFieldIndex].SetActive(false);
        inputFieldIndex--;
        index -= 4;
        isInputing = false;
        ktpTrigger.IsNotInputing();
        nextLine();
    }
    public void IntoDialog()
    {
        inputField[inputFieldIndex].SetActive(false);
        inputFieldIndex++;
        isInputing = false;
        ktpTrigger.IsNotInputing();
        nextLine();
    }
    public void SaveNamaKKDanNIK()
    {
        TMP_InputField[] temp = inputField[inputFieldIndex].GetComponentsInChildren<TMP_InputField>();
        gameManager.NamaPanjang = temp[0].text;
        namaSetTTD.text = temp[0].text;
        namaSetTTD.interactable = false;
        gameManager.NoKK = temp[1].text;
        gameManager.NoNIK = temp[2].text;
        if (temp[2].text == string.Empty)
        {
            temp[2].placeholder.GetComponent<TextMeshProUGUI>().text = "Silahkan isi No NIK anda";
            temp[2].placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            IntoDialog();
        }
    }
    IEnumerator CekStatusPendaftaran()
    {

        using (UnityWebRequest request = UnityWebRequest.Get("https://blockchain-itb.com/user/show"))
        {
            request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
            request.SetRequestHeader("access-token", gameManager.tokenPlayer);
            //request.SetRequestHeader("Content-Type", "application/json");
            //request.SetRequestHeader("mode", "no-cors");
            //request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
            //request.SetRequestHeader("Access-Control-Expose-Headers", "Content-Length, Content-Encoding");
            //request.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time, Content-Type");
            //request.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            //request.SetRequestHeader("Access-Control-Allow-Origin", "*");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                ShowUserStatus respond = JsonUtility.FromJson<ShowUserStatus>(request.downloadHandler.text);
                if (respond.data.registration_status == "false")
                {
                    isAllowedToClick = true;
                    nextLine();
                }
                else if (respond.data.verification_status == "0" && respond.data.registration_status == "true")
                {
                    isAllowedToClick = false;
                    isInputed = true;
                    ktpPerbarui.isDitolak = true;
                    ktpPerbarui.StatusDitolak();
                }
                else if (respond.data.verification_status == "1" && respond.data.registration_status == "true")
                {
                    isAllowedToClick = false;
                    isInputed = true;
                    string temp = respond.data.updated_at;
                    if (temp == string.Empty || temp == "")
                    {
                        temp = respond.data.created_at;
                    }
                    string date = string.Empty;
                    for (int i = 5; i <= 15; i++)
                    {
                        date += temp[i];
                    }
                    ktpPerbarui.isPending = true;
                    ktpPerbarui.StatusPending(date);
                }
                else if (respond.data.verification_status == "2" && respond.data.registration_status == "true")
                {
                    isAllowedToClick = false;
                    isInputed = true;
                    string temp = respond.data.created_at;
                    string date = string.Empty;
                    for (int i = 5; i <= 15; i++)
                    {
                        date += temp[i];
                    }
                    ktpPerbarui.isVerified = true;
                    ktpPerbarui.isVerifiedcondition();
                }
            }
        }
    }
    public void revertTTD()
    {
        tddGameobject.SetActive(false);
        cameraMain.SetActive(true);
    }
    public void GenderLaki()
    {
        gameManager.jenisKelamin = "Laki - laki";
    }
    public void GenderPerempuan()
    {
        gameManager.jenisKelamin = "Perempuan";
    }
    public void SetWNI()
    {
        gameManager.kewarganegaraan = "WNI";
    }
    public void SetWNA()
    {
        gameManager.kewarganegaraan = "WNA";
    }
    public void SetDataLengkapBiodata1()
    {
        TMP_InputField[] tempInputfield = inputField[inputFieldIndex].GetComponentsInChildren<TMP_InputField>();
        TMP_Dropdown[] dropdownAll = inputField[inputFieldIndex].GetComponentsInChildren<TMP_Dropdown>();
        List<TMP_Dropdown.OptionData> golonganDarahOptions = dropdownAll[0].options;
        List<TMP_Dropdown.OptionData> agamaOptions = dropdownAll[1].options;


        gameManager.tempatLahir = tempInputfield[0].text;
        gameManager.tanggalLahir = tempInputfield[1].text;
        gameManager.pekerjaan = tempInputfield[2].text;

        int tempId = dropdownAll[0].value;
        gameManager.golonganDarah = golonganDarahOptions[tempId].text;
        tempId = dropdownAll[1].value;
        gameManager.agama = agamaOptions[tempId].text;
        
        IntoDialog();
    }
    public void SetDataLengkapBiodata2()
    {
        TMP_InputField tempInputfield = inputField[inputFieldIndex].GetComponentInChildren<TMP_InputField>();
        TMP_Dropdown dropdownStatusPerkawinan = inputField[inputFieldIndex].GetComponentInChildren<TMP_Dropdown>();
        List<TMP_Dropdown.OptionData> StatusPerkawinaOptions = dropdownStatusPerkawinan.options;


        gameManager.pendidikanTerakhir = tempInputfield.text;

        int tempId = dropdownStatusPerkawinan.value;
        gameManager.statusPerkawinan = StatusPerkawinaOptions[tempId].text;

        IntoDialog();
    }
    public void SetBiometrik()
    {
        IntoDialog();
    }
    public void SetTandaTangan()
    {
        IntoDialog();
    }

}
