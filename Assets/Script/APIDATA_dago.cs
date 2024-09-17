using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class APIDATA_dago : MonoBehaviour
{
    /*    InputField outputArea;*/
    public float scaleSpeed;
    /*    private bool isScale = true;*/
    public Text mobil, motor, sepeda, truk, bus;

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
                var output = JsonUtility.ToJson(dataKendaraan.databar[2], true);

                Debug.Log("y: " + (dataKendaraan.databar[02].mobil / 2000f) * 100f);

                bar_mobil.transform.position += new Vector3(0f, dataKendaraan.databar[2].mobil / 1.5f, 0f) * 1f * Time.deltaTime;
                bar_motor.transform.Translate(new Vector3(0f, (dataKendaraan.databar[2].motor) / 1.5f, 0f) * 1f * Time.deltaTime);
                bar_truk.transform.position += new Vector3(0f, dataKendaraan.databar[2].truk / 1.5f, 0f) * 1f * Time.deltaTime;
                bar_bus.transform.position += new Vector3(0f, dataKendaraan.databar[2].bus / 1.5f, 0f) * 1f * Time.deltaTime;
                bar_sepeda.transform.position += new Vector3(0f, dataKendaraan.databar[2].sepeda / 1.5f, 0f) * 1f * Time.deltaTime;

                mobil.text = dataKendaraan.databar[2].mobil.ToString();
                motor.text = dataKendaraan.databar[2].motor.ToString();
                truk.text = dataKendaraan.databar[2].truk.ToString();
                bus.text = dataKendaraan.databar[2].bus.ToString();
                sepeda.text = dataKendaraan.databar[2].sepeda.ToString();

                Debug.Log("Jumlah mobil :" + dataKendaraan.databar[2].mobil.ToString());
                Debug.Log("Jumlah motor :" + dataKendaraan.databar[2].motor.ToString());
                Debug.Log("Jumlah truk :" + dataKendaraan.databar[2].truk.ToString());
                Debug.Log("Jumlah bus :" + dataKendaraan.databar[2].bus.ToString());
                Debug.Log("Jumlah sepeda :" + dataKendaraan.databar[2].sepeda.ToString());
            }

        }

    }
}
