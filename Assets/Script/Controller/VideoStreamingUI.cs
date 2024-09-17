using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoStreamingUI : MonoBehaviour
{
    public string videoURL;
    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    public float bufferTime; // Adjust as needed
    private float nextBufferTime;

    private bool isThis;
    public void updateVideo()
    {
        if (videoURL != null && videoURL.Length > 10)
        {
            isThis = true;
            // Set the video URL
            videoPlayer.url = videoURL;

            // Set the video texture to the RawImage
            videoPlayer.targetTexture = new RenderTexture((int)rawImage.rectTransform.sizeDelta.x, (int)rawImage.rectTransform.sizeDelta.y, 0);
            rawImage.texture = videoPlayer.targetTexture;

            // Play the video
            videoPlayer.Play();
        }
    }
    private void Update()
    {
        videoPlayer.loopPointReached += EndReached;
        /*if (Time.time >= nextBufferTime)
        {

            Debug.Log("updating");
            videoPlayer.Stop();
            videoPlayer.Play();

            nextBufferTime = Time.time + bufferTime;
        }*/
    }
    void EndReached(VideoPlayer vp)
    {
        updateVideo();
    }
}
