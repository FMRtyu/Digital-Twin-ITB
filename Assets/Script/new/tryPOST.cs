using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class tryPOST : MonoBehaviour
{
    string apiUrl = "http://blockchainitb.ip-dynamic.com:3000/auth/login"; // Ganti URL API sesuai kebutuhan

     void Start()
    {
        tryPOST apiManager = GetComponent<tryPOST>();
        apiManager.SendPostRequest(apiUrl);

    }


    public void SendPostRequest(string jsonData)
    {
        StartCoroutine(PostRequest(jsonData));
    }



    IEnumerator PostRequest(string jsonData)
    {
        UnityWebRequest request = UnityWebRequest.PostWwwForm(apiUrl, jsonData);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error: " + request.error);
            Debug.Log("Error bang");
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            Debug.Log("kerenbang");

        }
    }
}

