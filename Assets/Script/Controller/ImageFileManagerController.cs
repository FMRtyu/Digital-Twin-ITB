using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;

public class ImageFileManagerController : MonoBehaviour
{
    public string path;

    public RawImage pasFoto;

    public void ShowPopup()
    {
        // Send a message to JavaScript to show the popup
        Application.ExternalEval("showProfilePopup();");
    }

    public void OpenFileExplorer()
    {
#if UNITY_EDITOR
        path = EditorUtility.OpenFilePanel("Pilih Pas Foto (.png)", "", "png");
#endif
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + path);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("terjadi kesalahan...");
            /*
             * Texture2D texture = new Texture2D(1, 1);
             * if (texture.LoadImage(PasFoto))
            {
                // Assign the texture to a material or a game object's renderer
                // For example, if you have a renderer component attached to your GameObject:
                imageFromUpload.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            }
            else
            {
                Debug.LogError("Failed to load image data.");
            }*/
        }
        else
        {
            Texture tekstur = ((DownloadHandlerTexture)www.downloadHandler).texture;

            pasFoto.texture = tekstur;
        }
    }
}
