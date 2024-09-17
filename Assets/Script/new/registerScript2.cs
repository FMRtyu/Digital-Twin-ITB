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


public class registerScript2 : MonoBehaviour
{

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("token"));
        PlayerPrefs.SetString("token", string.Empty);
    }
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

    public void register()
    {
        name = inpName.text;
        email = inpEmail.text;
        password = inpPassword.text;
        passwordConf = inpPasswordConf.text;
        PlayerPrefs.SetString("emailPlayer", email);
        string data = "{{\"full_name\":\"" + name + "\",\"email\":\"" + email + "\",\"password\":\"" + password + "\"+" + "\",\"password_confirmation\":\"" + passwordConf + "\"}";
        StartCoroutine(PostRequest());
    }

    public void SendPostRequest()
    {
        StartCoroutine(PostRequest());
    }

    IEnumerator PostRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("full_name", name); 
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("password_confirmation", passwordConf);

        //UnityWebRequest request = new UnityWebRequest(apiUrl + "/auth/register", "POST");


        //byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        //request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        //request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        using (UnityWebRequest request = UnityWebRequest.Post("https://blockchain-itb.com/auth/register", form))
        {
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
            request.SetRequestHeader("token", "02e2310edc5eaf62dfe406abaf1a65af");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                Debug.Log("Response: " + request.downloadHandler.text);
                ERROR.SetActive(true);
            }
            else
            {
                modelUser respond = JsonUtility.FromJson<modelUser>(request.downloadHandler.text);

                PlayerPrefs.SetString("token", respond.accessToken);

                PlayerPrefs.SetString("avatar", string.Empty);
                PlayerPrefs.SetString("namaPlayer", name);
                Debug.Log("Response: " + request.downloadHandler.text);
                SceneManager.LoadScene("Build-Test");
                StopAllCoroutines();

            }
        }
        //request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        
        //request.SetRequestHeader("token", "02e2310edc5eaf62dfe406abaf1a65af");

        
    }
}