using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoStreammingController : MonoBehaviour
{
    public string baseUrl = "https://pelindung.bandung.go.id:3443/video/DAHUA/";
    public VideoPlayer videoPlayer;
    public string[] tsVideoURLs;

    private int currentPart = 0;

    void Start()
    {
        StartCoroutine(DownloadAndPlayVideoParts());
    }

    IEnumerator DownloadAndPlayVideoParts()
    {
        while (currentPart < tsVideoURLs.Length)
        {
            string url = tsVideoURLs[currentPart];
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Save the downloaded .ts video part to a temporary file path.
                string tempPath = Application.persistentDataPath + $"/part{currentPart}.ts";
                System.IO.File.WriteAllBytes(tempPath, www.downloadHandler.data);

                // Load the downloaded .ts video part into the VideoPlayer.
                videoPlayer.url = tempPath;
                videoPlayer.Prepare();

                // Play the video part.
                videoPlayer.Play();

                // Wait for this part to finish playing before proceeding to the next one.
                while (videoPlayer.isPlaying)
                {
                    yield return null;
                }

                // Move on to the next part.
                currentPart++;
            }
            else
            {
                Debug.LogError("Failed to download video part: " + www.error);
                break; // Exit the loop on failure.
            }
        }
    }
}
