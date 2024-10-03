using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class provinces
{
    public string id;
    public string name;
}

public class GETDATAProv : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    private provinceData[] province;

    IEnumerator Start()
    {
        // Mengambil data provinsi dari URL
        string url = "https://www.emsifa.com/api-wilayah-indonesia/api/provinces.json";
        Debug.Log("ada");
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            // Memeriksa apakah ada error dalam mengambil data
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching province data: " + www.error);
                yield break;
            }
            //dataprov respond = JsonUtility.FromJson<dataprov>(request.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
           /* province = JsonUtility.FromJson<provinceData[]>(www.downloadHandler.text);*/

            if (www.downloadHandler.text.StartsWith("[") && www.downloadHandler.text.EndsWith("]"))
            {
                // Jika data JSON adalah sebuah array, maka parse data array
                province = JsonUtility.FromJson<provinceData[]>(www.downloadHandler.text); 
            }
            else
            {
                // Jika data JSON adalah sebuah objek, maka parse objek
                provinceData province = JsonUtility.FromJson<provinceData>(www.downloadHandler.text);
            }
            Debug.Log(province);


            dropdown.ClearOptions();
            List<string> provinceNames = new List<string>();
            foreach (provinceData province in province)
            {
                provinceNames.Add(province.name);
            }
            dropdown.AddOptions(provinceNames);
        }
    }
    [System.Serializable]

    public class dataprov
    {
        public List<provinceData> array;
    }

    [System.Serializable]
    public class provinceData
    {
        public string id;
        public string name;
    }
}