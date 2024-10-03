using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class VerifikasiController : MonoBehaviour
{
    public CariByNikController NikController;
    public GetDataFromAPIController APIController;
    public GameObject BerhasilPage;
    public int currentIndex;
    //gameobject
    public GameObject AlasanGroup;
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI NoNIK;
    private void Start()
    {
        currentIndex = 1;
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }
    private void OnDropdownValueChanged(int index)
    {
        if (index == 0)
        {
            currentIndex = 1;
            AlasanGroup.SetActive(false);
}
        else if (index == 1)
        {
            currentIndex = 2;
            AlasanGroup.SetActive(false);
        }
        else if (index == 2)
        {
            currentIndex = 0;
            AlasanGroup.SetActive(true);
        }
    }

    public void VerifikasiRequest()
    {
        if (currentIndex != 1)
        {
            StartCoroutine(SendVerifikasiRequest());
        }
    }

    IEnumerator SendVerifikasiRequest()
    {
        string alasan = AlasanGroup.GetComponentInChildren<TMP_InputField>().text;
        WWWForm form = new WWWForm();
        form.AddField("nik", NoNIK.text);
        form.AddField("status_verifikasi", currentIndex);
        form.AddField("rejected_reason", alasan);

        UnityWebRequest request = UnityWebRequest.Post("https://blockchain-itb.com/resident-identity/verify", form);

        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("access-token", PlayerPrefs.GetString("token"));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("sesuatu terjadi");
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            NikController.ResetAllText();
            AlasanGroup.SetActive(false);
            APIController.gameObject.SetActive(true);
            BerhasilPage.SetActive(true);
            APIController.RefreshDataFromAPI();
        }
    }
}
