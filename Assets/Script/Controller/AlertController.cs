using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertController : MonoBehaviour
{
    public bool isClosing;
    public void Update()
    {
        if (gameObject.activeSelf && !isClosing)
        {
            isClosing = true;
            StartCoroutine(Closing());
        }
    }
    IEnumerator Closing()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
        isClosing = false;
    }
}
