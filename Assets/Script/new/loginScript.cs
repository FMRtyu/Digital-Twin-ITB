using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class loginScript : MonoBehaviour
{
    string apiUrl = "https://blockchain-itb.com";
    public TMP_InputField inpEmail;
    public TMP_InputField inpPassword;
    public Button loginBtn;
    public GameObject ERROR;

    //check API
    public GameObject APINotAvalaible;

    string email ;
    string password;

    public GameObject MenuPage;
    public GameObject LoadingPage;

    public Slider LoadingSlider;

    void Start()
    {
        loginBtn.onClick.AddListener(login);
        if (PlayerPrefs.GetString("emailPlayer") != string.Empty)
        {
            inpEmail.text = PlayerPrefs.GetString("emailPlayer");
        }
    }

    void login()
    {
        email = inpEmail.text;
        password = inpPassword.text;
        string data = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\"}";
        SendPostRequest(data);

    }

    private void Awake()
    {
        StartCoroutine(CheckAPI());
    }

    IEnumerator CheckAPI()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            APINotAvalaible.SetActive(true);
        }
        else
        {
            APINotAvalaible.SetActive(false);
        }
    }
    public void SendPostRequest(string jsonData)
    {
        StartCoroutine(PostRequest(jsonData));
    }

    IEnumerator PostRequest(string jsonData)
    {
        UnityWebRequest request = new UnityWebRequest(apiUrl + "/auth/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        //request.SetRequestHeader("mode", "no-cors");
        request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
        request.SetRequestHeader("Access-Control-Expose-Headers", "Content-Length, Content-Encoding");
        request.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time, Content-Type");
        request.SetRequestHeader("Access-Control-Allow-Methods","GET, POST, OPTIONS");
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");


        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
            ERROR.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("emailPlayer", email);
            ERROR.SetActive(false);

            modelUser respond = JsonUtility.FromJson<modelUser>(request.downloadHandler.text);

            PlayerPrefs.SetString("token", respond.accessToken);
            PlayerPrefs.SetString("avatar", respond.data.avatar);
            PlayerPrefs.SetString("namaPlayer", respond.data.username);
            PlayerPrefs.SetString("NIK", respond.data.digital_identity.nik);
            PlayerPrefs.SetInt("status", respond.data.digital_identity.verification_status);

            if (respond.data.role == "admin")
            {
                SceneManager.LoadScene("AdminScene");
            }
            else if (respond.data.role == "instansi")
            {
                SceneManager.LoadScene("InstansiScene");
            }
            else
            {
                LoadSceneIndex();
                LoadingPage.SetActive(true);
                //SceneManager.LoadScene("index");
            }
        }
    }

    public void LoadSceneIndex()
    {
        MenuPage.SetActive(false);
        StartCoroutine(LoadTheScene());
    }

    IEnumerator LoadTheScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("index");
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            Debug.Log(progressValue);
            LoadingSlider.value = progressValue;
            if (progressValue >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}