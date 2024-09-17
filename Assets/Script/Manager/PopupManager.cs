using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public void ShowProfilePopup()
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
        Application.ExternalEval("showProfilePopup();");
    }
    public void ShowMataPopup()
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
        Application.ExternalEval("showMataPopup();");
    }
    public void ShowSidikPopup()
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
        Application.ExternalEval("showSidikPopup();");
    }
    public void ShowNoKKPopup()
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
        Application.ExternalEval("showKKPopup();");
    }
    public void ShowProfileEditPopup()
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
        Application.ExternalEval("showProfileEditPopup();");
    }
    public void ShowKKEditPopup()
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
        Application.ExternalEval("showKatruKKEditPopup();");
    }
    public void ShowDomisiliEditPopup()
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
        Application.ExternalEval("showDomisiliEditPopup();");
    }
    public void ShowSKKEditPopup()
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
        Application.ExternalEval("showSKKEditPopup();");
    }
    public void ShowAktaNikahCeraiEditPopup()
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
        Application.ExternalEval("showAktaNikahCeraiEditPopup();");
    }
    public void ShowIjazahEditPopup()
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
        Application.ExternalEval("showIjazahEditPopup();");
    }
    public void ShowPindahAgamaEditPopup()
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
        Application.ExternalEval("showPindahAgamaEditPopup();");
    }
}
