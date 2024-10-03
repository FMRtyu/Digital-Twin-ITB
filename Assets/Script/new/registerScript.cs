using System.Collections;
using System.Collections.Generic;
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


public class registerScript : MonoBehaviour
{
    string apiUrl = "https://blockchain-itb.com";
    public TMP_InputField inpName;
    public TMP_InputField inpEmail;
    public TMP_InputField inpPassword;
    public TMP_InputField inpPasswordConf;
    public Button registerBtn;

    public GameObject ERROR;

    string name;
    string email;
    string password;
    string passwordConf;

    public GameObject MenuPage;
    public GameObject LoadingPage;

    public Slider LoadingSlider;
    void Start()
    {
        registerBtn.onClick.AddListener(register);
    }

    void register()
    {
        name = inpName.text;
        email = inpEmail.text;
        password = inpPassword.text;
        passwordConf = inpPasswordConf.text;
        string data = "{{\"full_name\":\"" + name + "\",\"email\":\"" + email + "\",\"password\":\"" + password + "\"+" + "\",\"password_confirmation\":\"" + passwordConf + "\"}";
        SendPostRequest(data);
    }

    public void SendPostRequest(string jsonData)
    {
        StartCoroutine(PostRequest(jsonData));
    }

    IEnumerator PostRequest(string jsonData)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", name); 
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("password_confirmation", passwordConf);

       // UnityWebRequest request = new UnityWebRequest(apiUrl + "/auth/register", "POST");
        UnityWebRequest request = UnityWebRequest.Post(apiUrl +"/auth/register", form);

        //byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        //request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("token", "02e2310edc5eaf62dfe406abaf1a65af");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            modelUser respond = JsonUtility.FromJson<modelUser>(request.downloadHandler.text);
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
            if (respond.message == "Username is required!")
            {
                ERROR.SetActive(true);
                ERROR.GetComponent<TextMeshProUGUI>().text = "Silahkan Isi Username Anda" ;
            }else if (respond.message == "Email is required!")
            {
                ERROR.SetActive(true);
                ERROR.GetComponent<TextMeshProUGUI>().text = "Silahkan Isi Email Anda";
            }
            else if (respond.message == "Password is required!")
            {
                ERROR.SetActive(true);
                ERROR.GetComponent<TextMeshProUGUI>().text = "Silahkan Isi Password Anda";
            }
            else if (respond.message == "Password confirmation does not match!")
            {
                ERROR.SetActive(true);
                ERROR.GetComponent<TextMeshProUGUI>().text = "Password Konfirmasi Tidak Sama";
            }
            else if (respond.message == "Email address is already used!")
            {
                ERROR.SetActive(true);
                ERROR.GetComponent<TextMeshProUGUI>().text = "Email Sudah Di Pakai";
            }
        }
        else
        {
            ERROR.SetActive(false);
            PlayerPrefs.SetString("emailPlayer", email);
            modelUser respond = JsonUtility.FromJson<modelUser>(request.downloadHandler.text);
            PlayerPrefs.SetString("token", respond.accessToken);
            PlayerPrefs.SetString("avatar", respond.data.avatar);
            PlayerPrefs.SetString("namaPlayer", name);
            PlayerPrefs.SetString("NIK", respond.data.digital_identity.nik);
            PlayerPrefs.SetInt("status", respond.data.digital_identity.verification_status);

            Debug.Log("Response: " + request.downloadHandler.text);
            //SceneManager.LoadScene("Build-Test");
            LoadSceneIndex();

        }
    }
    public void LoadSceneIndex()
    {
        MenuPage.SetActive(false);
        LoadingPage.SetActive(true);
        StartCoroutine(LoadTheScene());
    }

    IEnumerator LoadTheScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("index");

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            LoadingSlider.value = progressValue;
            yield return null;
        }
    }
}