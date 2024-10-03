using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class DeleteDataController : MonoBehaviour
{
    public string NoNIK;
    public GameObject konfirmasiBerhasil;
    public GameObject konfirmasiGagal;
    public TextMeshProUGUI nikText;
    public GetDataFromAPIController APIData;

    public void deleteData()
    {
        StartCoroutine(SendData(NoNIK));
    }

    IEnumerator SendData(string nik)
    {
        WWWForm form = new WWWForm();

        form.AddField("nik", nik);

        UnityWebRequest request = UnityWebRequest.Post("https://blockchain-itb.com/resident-identity/delete", form);
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("token", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("access-token", PlayerPrefs.GetString("token"));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            modelUser response = JsonUtility.FromJson<modelUser>(request.downloadHandler.text);
            if (response.error != "true")
            {
                this.gameObject.SetActive(false);
                konfirmasiBerhasil.SetActive(true);
                APIData.RefreshDataFromAPI();
            }
            else
            {
                konfirmasiGagal.SetActive(true);
                this.gameObject.SetActive(false);
                APIData.RefreshDataFromAPI();
            }
            
        }
    }
}
