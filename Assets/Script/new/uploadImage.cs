using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class uploadImage : MonoBehaviour
{

    // URL endpoint API yang akan digunakan untuk mengupload gambar
    public string url = "http://example.com/upload_image";

    // Method untuk mengupload gambar
    public IEnumerator UploadImage(byte[] imageBytes)
    {

        // Buat form data dengan jenis konten multipart/form-data
        WWWForm formData = new WWWForm();
        formData.AddField("image", "Image Upload");
        formData.AddBinaryData("file", imageBytes, "image.png", "image/png");

        // Buat request ke API dengan metode POST dan form data sebagai payload
        using (UnityWebRequest www = UnityWebRequest.Post(url, formData))
        {
            // Tunggu hingga selesai mengirim request
            yield return www.SendWebRequest();

            // Cek jika terdapat error pada request
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Upload failed: " + www.error);
            }
            else
            {
                Debug.Log("Upload successful!");
            }
        }
    }
}
