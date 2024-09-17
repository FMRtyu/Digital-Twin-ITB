using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentController : MonoBehaviour
{
    public string kkLink;
    public RawImage[] sidikJari;
    public RawImage irisMata;
    public string DomisiliLink;
    public string SKKLink;
    public string AktaNikahLink;
    public string IjazahLink;
    public string SKPALink;

    //button
    public Button kkBTN;
    public Button DomisiliBTN;
    public Button SKKBTN;
    public Button AktaNikahBTN;
    public Button IjazahBTN;
    public Button SKPABTN;

    private void OnEnable()
    {
        //setButton
        if (!string.IsNullOrEmpty(kkLink))
        {
            kkBTN.interactable = true;
        }
        else
        {
            kkBTN.interactable = false;
        }

        if (!string.IsNullOrEmpty(DomisiliLink))
        {
            DomisiliBTN.interactable = true;
        }
        else
        {
            DomisiliBTN.interactable = false;
        }

        if (!string.IsNullOrEmpty(SKKLink))
        {
            SKKBTN.interactable = true;
        }
        else
        {
            SKKBTN.interactable = false;
        }

        if (!string.IsNullOrEmpty(AktaNikahLink))
        {
            AktaNikahBTN.interactable = true;
        }
        else
        {
            AktaNikahBTN.interactable = false;
        }

        if (!string.IsNullOrEmpty(IjazahLink))
        {
            IjazahBTN.interactable = true;
        }
        else
        {
            IjazahBTN.interactable = false;
        }

        if (!string.IsNullOrEmpty(SKPALink))
        {
            SKPABTN.interactable = true;
        }
        else
        {
            SKPABTN.interactable = false;
        }
    }
    public void KKDownloader()
    {
        if (kkLink != string.Empty)
        {
            Debug.Log("try downloading : " + kkLink);
            // Send a message to JavaScript to show the popup
#if UNITY_WEBGL && !UNITY_EDITOR
            // Check if the game is in fullscreen mode
            if (Screen.fullScreen)
            {
                // Call the JavaScript function to exit fullscreen
                Application.ExternalEval("ExitFullscreen();");
            }
#endif
            Application.OpenURL(kkLink);
        }
    }
    public void DomisiliDownloader()
    {
        if (DomisiliLink != string.Empty)
        {
            // Send a message to JavaScript to show the popup
#if UNITY_WEBGL && !UNITY_EDITOR
            // Check if the game is in fullscreen mode
            if (Screen.fullScreen)
            {
                // Call the JavaScript function to exit fullscreen
                Application.ExternalEval("ExitFullscreen();");
            }
#endif
            Application.OpenURL(DomisiliLink);
        }
    }
    public void SKKDownloader()
    {
        if (SKKLink != string.Empty)
        {
            // Send a message to JavaScript to show the popup
#if UNITY_WEBGL && !UNITY_EDITOR
            // Check if the game is in fullscreen mode
            if (Screen.fullScreen)
            {
                // Call the JavaScript function to exit fullscreen
                Application.ExternalEval("ExitFullscreen();");
            }
#endif
            Application.OpenURL(SKKLink);
        }
    }
    public void NikahDownloader()
    {
        if (AktaNikahLink != string.Empty)
        {
            // Send a message to JavaScript to show the popup
#if UNITY_WEBGL && !UNITY_EDITOR
            // Check if the game is in fullscreen mode
            if (Screen.fullScreen)
            {
                // Call the JavaScript function to exit fullscreen
                Application.ExternalEval("ExitFullscreen();");
            }
#endif
            Application.OpenURL(AktaNikahLink);
        }
    }
    public void IjazahDownloader()
    {
        if (IjazahLink != string.Empty)
        {
            // Send a message to JavaScript to show the popup
#if UNITY_WEBGL && !UNITY_EDITOR
            // Check if the game is in fullscreen mode
            if (Screen.fullScreen)
            {
                // Call the JavaScript function to exit fullscreen
                Application.ExternalEval("ExitFullscreen();");
            }
#endif
            Application.OpenURL(IjazahLink);
        }
    }
    public void AgamaDownloader()
    {
        if (SKPALink != string.Empty)
        {
            // Send a message to JavaScript to show the popup
#if UNITY_WEBGL && !UNITY_EDITOR
            // Check if the game is in fullscreen mode
            if (Screen.fullScreen)
            {
                // Call the JavaScript function to exit fullscreen
                Application.ExternalEval("ExitFullscreen();");
            }
#endif
            Application.OpenURL(SKPALink);
        }
    }
}
