using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class scaleScript : MonoBehaviour
{
/*    InputField outputArea;*/
    public float scaleSpeed;
    /*    private bool isScale = true;*/
    public Text mobil,motor,sepeda,truk,bus;

    /*    public GameObject bar1_obj;
        public GameObject bar2_obj;
        public GameObject bar3_obj;
        public GameObject bar4_obj;
        public GameObject bar5_obj;
        public GameObject bar6_obj;
        public GameObject bar7_obj;
        public GameObject bar8_obj;
        public GameObject bar9_obj;
        public GameObject bar10_obj;*/

    /*   public float a, b, c, d, e;*/

    public GameObject bar_motor;
    public GameObject bar_mobil;
    public GameObject bar_truk;
    public GameObject bar_bus;
    public GameObject bar_sepeda;

    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(GetData());
    }
    void Start()
    {
    }
    IEnumerator GetData()
    {
        string url = "https://rest-viana.gotos.id/rows";
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            DataKendaraan dataKendaraan;

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string json = "{ \"databar\":" + request.downloadHandler.text + "}";
                dataKendaraan = JsonUtility.FromJson<DataKendaraan>(json);
                /*outputArea.text = dataKendaraan.datas.ToString();*/
                var output = JsonUtility.ToJson(dataKendaraan.databar[0], true);
/*                Debug.Log("Mobil : " + dataKendaraan.databar[0].mobil);
                Debug.Log("Motor : " + dataKendaraan.databar[0].motor);*/

                Debug.Log("y: " + (dataKendaraan.databar[0].mobil / 2000f)*100f);

                bar_mobil.transform.position += new Vector3(0f, dataKendaraan.databar[9].mobil /1.5f , 0f) * 1f * Time.deltaTime;
                bar_motor.transform.Translate(new Vector3(0f, (dataKendaraan.databar[9].motor) / 1.5f, 0f) * 1f * Time.deltaTime);
                bar_truk.transform.position += new Vector3(0f, dataKendaraan.databar[9].truk / 1.5f, 0f) * 1f * Time.deltaTime;
                bar_bus.transform.position += new Vector3(0f, dataKendaraan.databar[9].bus / 1.5f, 0f) * 1f * Time.deltaTime;
                bar_sepeda.transform.position += new Vector3(0f, dataKendaraan.databar[9].sepeda / 1.5f, 0f) * 1f * Time.deltaTime;
                
                mobil.text = dataKendaraan.databar[9].mobil.ToString();
                motor.text = dataKendaraan.databar[9].motor.ToString();
                truk.text = dataKendaraan.databar[9].truk.ToString();
                bus.text = dataKendaraan.databar[9].bus.ToString();
                sepeda.text = dataKendaraan.databar[9].sepeda.ToString();
                
                Debug.Log("Jumlah mobil :"+dataKendaraan.databar[9].mobil.ToString());
                Debug.Log("Jumlah motor :"+dataKendaraan.databar[9].motor.ToString());
                Debug.Log("Jumlah truk :"+dataKendaraan.databar[9].truk.ToString());
                Debug.Log("Jumlah bus :"+dataKendaraan.databar[9].bus.ToString());
                Debug.Log("Jumlah sepeda :"+dataKendaraan.databar[9].sepeda.ToString());
            }
            
/*            foreach (Data datak in dataKendaraan.datas)
            {
                Debug.Log("foreach :"+ datak.mobil);
            }*/
        }

        // Debug.Log(JsonConvert.DeserializeObject<DataKendaraan>(request.downloadHandler.text.ToString()));
        // Debug.Log(JsonUtility.FromJson<DataKendaraan>(request.downloadHandler.text));

    }
}
