using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class GetMethod: MonoBehaviour 
{
    InputField outputArea;
    Text txtmob;

    // Start is called before the first frame update
    void Start()
    {
        outputArea = GameObject.Find("outputArea").GetComponent<InputField>();
        GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);

    }
    void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading ..";
        string url = "https://rest-viana.gotos.id/rows";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                outputArea.text = request.error;
            }
            else
            {
                outputArea.text = "{ \"DataKendaraan\":" + request.downloadHandler.text +"}";



                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
