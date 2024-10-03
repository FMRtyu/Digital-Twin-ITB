using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using TMPro;

public class login : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;


    public Button loginButton;
    public GameObject pesan_error;
    public TextMeshProUGUI logg;

    private string loginUrl = "http://blockchainitb.ip-dynamic.com:3000/auth/login";
    private string authToken = "";

    void Start()
    {
        // Listener untuk tombol login
        loginButton.onClick.AddListener(Login);
    }

    void Login()
    {
        // Ambil username dan password dari input field
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Buat payload JSON
        Dictionary<string, string> payload = new Dictionary<string, string>();
        payload.Add("email", username);
        payload.Add("password", password);
        Debug.Log("baris 37: " + payload);
        logg.text = payload.ToString();

        // Buat request POST ke API login
        StartCoroutine(PostRequest(loginUrl, payload, (response) =>
        {
            // Ambil token dari response
            authToken = response["message"];
            Debug.Log("token: "+ response["message"]);

           
            
            
        }));
    }

    IEnumerator PostRequest(string url, Dictionary<string, string> payload, Action<Dictionary<string, string>> callback)
    {
        // Buat request
        UnityWebRequest request = UnityWebRequest.PostWwwForm(url,"POST");

        // Set header
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("api-key", "02e2310edc5eaf62dfe406abaf1a65af");

        // Buat JSON string dari payload
        string jsonPayload = JsonUtility.ToJson(payload);
        Debug.Log("jsonPayload " + jsonPayload);
        //logg.text = jsonPayload;

        // Convert string ke byte array
        byte[] requestBody = new UTF8Encoding().GetBytes(jsonPayload);

        // Set body request
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(requestBody);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        // Kirim request ke API
        yield return request.SendWebRequest();
        //Debug.Log("75 :" + request.SendWebRequest());

        // Cek apakah ada error
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
            //Debug.Log("request:" + loginUrl);
            pesan_error.SetActive(true);
        }
        else
        {
            Debug.Log("oke");
            // Ambil response JSON dari request
            string jsonResponse = request.downloadHandler.text;

            // Convert response JSON ke dictionary
            Dictionary<string, string> response = JsonUtility.FromJson<Dictionary<string, string>>(jsonResponse);

            // Panggil callback function dengan response dictionary
            callback(response);

        }
    }
}
