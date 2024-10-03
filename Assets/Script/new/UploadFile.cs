using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class UploadFile : MonoBehaviour
{
    public Texture2D texture;
    private byte[] fileData;
    private string fileName;
    private string fileType;

    public void SelectImage()
    {
        // Memilih gambar dari galeri
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Memuat gambar ke dalam texture
                byte[] bytes = File.ReadAllBytes(path);
                texture = new Texture2D(2, 2);
                texture.LoadImage(bytes);

                // Mengubah gambar menjadi data PNG
                fileData = texture.EncodeToJPG();
                fileName = Path.GetFileName(path);
                fileType = "image/jpg";
                Debug.Log("filedata : "+fileData);
                Debug.Log("filename : "+fileName);
                Debug.Log("filetype: "+fileType);
            }
        }, "Select an image");

        Debug.Log("Permission result: " + permission);
    }

    public byte[] GetFileData()
    {
        return fileData;
    }

    public string GetFileName()
    {
        return fileName;
    }

    public string GetFileType()
    {
        return fileType;
    }
}
