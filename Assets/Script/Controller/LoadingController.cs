using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingController : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    private int indexDot;
    public bool isLoading;

    private void Start()
    {
        indexDot = 0;
        isLoading = true;
    }

    IEnumerator Writing()
    {
        while (isLoading)
        {
            if (indexDot > 3)
            {
                indexDot = 0;
                loadingText.text = "Loading";
            }
            else
            {
                indexDot++;
                loadingText.text += ".";
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void DoneLoading()
    {
        isLoading = false;
        StopAllCoroutines();
    }
    public void WasLoading()
    {
        isLoading = true;
        StartCoroutine(Writing());
    }
}
