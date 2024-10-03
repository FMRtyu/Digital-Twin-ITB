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

public class selectAvatar : MonoBehaviour
{
    string apiUrl = "http://131161127684.ip-dynamic.com:3000";
    public GameObject[] avatars;
    public Button[] avatarButtons;
    public string tokkens;
    public int avatarr;
    modelUser myrespon;

    //hidden object
    public GameObject canvSelect;

    private int selectedAvatarIndex;

    void Start()
    {
        tokkens = PlayerPrefs.GetString("token");
        //PlayerPrefs.SetString("avatar", myrespon.data.avatar);
        Debug.Log(PlayerPrefs.GetString("avatar"));

        if (PlayerPrefs.GetString("avatar") != string.Empty)
        {
            avatarr = int.Parse(PlayerPrefs.GetString("avatar")) - 1;

        }
        Debug.Log("avatar : " + avatarr);

        
        if (avatarr == null || avatarr <=0)
        {
            // Menambahkan listener untuk setiap tombol avatar
            for (int i = 0; i < avatarButtons.Length; i++)
            {
                int avatarIndex = i;
                avatarButtons[i].onClick.AddListener(() => SelectAvatar(avatarIndex));
            }

            // Menampilkan avatar pertama secara default
            SelectAvatar(selectedAvatarIndex);

            foreach (GameObject avatar in avatars)
            {
                avatar.SetActive(false);
            }
            //canvSelect.SetActive(false);
        }
        else
        {
            foreach (GameObject avatar in avatars)
            {
                avatar.SetActive(false);
            }
            SelectAvatar(avatarr);

            canvSelect.SetActive(false);
            Debug.Log("Data Avatar sudah terpilih");
        }

    }

    void SelectAvatar(int index)
    {
        // Menyembunyikan avatar sebelumnya
        if(selectedAvatarIndex > 0)
        {
         avatars[selectedAvatarIndex - 1].SetActive(false);
        }

        // Menampilkan avatar yang dipilih
        avatars[index].SetActive(true);

        // Menyimpan index avatar yang dipilih
        selectedAvatarIndex = index +1;
        
        string data = "{\"avatar\":" + selectedAvatarIndex + "}";
        SendPostRequest(data);

        Debug.Log("Select Avatar "+PlayerPrefs.GetString("token"));
        Debug.Log(index);
    }

    public void SendPostRequest(string jsonData)
    {
        StartCoroutine(PostRequest(jsonData));
    }

    IEnumerator PostRequest(string jsonData)
    {
       // WWWForm form = new WWWForm();
       // form.AddField("avatar", selectedAvatarIndex);

        UnityWebRequest request = new UnityWebRequest(apiUrl + "/user/select-avatar", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-api-key", "d249e530-a13e-4f25-a240-53a9ad5b1e65");
        request.SetRequestHeader("access-token", tokkens);
        Debug.Log("token  ke header :" + tokkens);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);

        }
    }
}
