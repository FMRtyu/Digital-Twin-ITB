using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VehicleCountController : MonoBehaviour
{
    public bool isDataTest;
    public bool isThisSelected;
    public float[] valuesOfVehicle;
    public float[] newValuesOfVehicle;
    public TextMeshProUGUI[] vehicleCountText;
    public GameObject alertUpdate;

    public string URLVideo;

    public BarChartController barChartController;
    public VideoStreamingUI[] videoStreamingUI;

    public Button CCTVButton;
    public string TagName;

    public bool cikapayang, daagBor, itbKeb, simpangDago;
    // Start is called before the first frame update

    public void OpenCCTVVideo()
    {
        Application.ExternalEval("ExitFullscreen()");
        if (cikapayang && isThisSelected)
        {
            Application.ExternalEval("OpenCikapayangVideo()");
        }
        else if (daagBor && isThisSelected)
        {
            Application.ExternalEval("OpenDagborVideo()");
        }
        else if (itbKeb && isThisSelected)
        {
            Application.ExternalEval("OpenITBVideo()");
        }
        else if (simpangDago && isThisSelected)
        {
            Application.ExternalEval("OpenSimpangDagoVideo()");
        }
    }
    void Start()
    {
        CCTVButton.onClick.AddListener(OpenCCTVVideo);
        valuesOfVehicle = new float[5];
        newValuesOfVehicle = new float[5];
        StartCoroutine(DataDummy());
        //StartCoroutine(GetDataVehicleFirst());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Deselected()
    {
        isThisSelected = false;
    }
    public void SetDataInMenu()
    {
        vehicleCountText[0].text = "Sepeda : " + valuesOfVehicle[0];
        vehicleCountText[1].text = "Bus : " + valuesOfVehicle[1];
        vehicleCountText[2].text = "Truk : " + valuesOfVehicle[2];
        vehicleCountText[3].text = "Mobil : " + valuesOfVehicle[3];
        vehicleCountText[4].text = "Motor : " + valuesOfVehicle[4];
    }
    public void SetVideo()
    {
        for (int i = 0; i < videoStreamingUI.Length; i++)
        {
            videoStreamingUI[i].videoURL = URLVideo;
            videoStreamingUI[i].updateVideo();
        }
    }
    IEnumerator GetDataVehicleFirst()
    {
        string url = "";
        if (cikapayang)
        {
            url = "http://rest-viana.gotos.id/tag/cikapayang";
        }
        else if (daagBor)
        {
            url = "http://rest-viana.gotos.id/tag/borromeus";
        }
        else if (itbKeb)
        {
            url = "http://rest-viana.gotos.id/tag/itb-bonbin";
        }
        else if (simpangDago)
        {
            url = "http://rest-viana.gotos.id/tag/djuanda";
        }

        UnityWebRequest request = UnityWebRequest.Get(url);

        request.SetRequestHeader("NO-CORS","true");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            SimpleJSON.JSONNode dataKendaraan = SimpleJSON.JSON.Parse(json);
            valuesOfVehicle[0] = dataKendaraan[0]["sepeda"];
            valuesOfVehicle[1] = dataKendaraan[0]["bus"];
            valuesOfVehicle[2] = dataKendaraan[0]["truk"];
            valuesOfVehicle[3] = dataKendaraan[0]["mobil"];
            valuesOfVehicle[4] = dataKendaraan[0]["motor"];

            barChartController.setValues(valuesOfVehicle);
            barChartController.makeBarChart();
            KeepUpdateData();
        }
    }

    public void KeepUpdateData()
    {
        StartCoroutine(DataUpdating());
    }

    IEnumerator DataUpdating()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            bool change = false;
            string url = "";
            if (cikapayang)
            {
                url = "http://rest-viana.gotos.id/tag/cikapayang";
            }
            else if (daagBor)
            {
                url = "http://rest-viana.gotos.id/tag/borromeus";
            }
            else if (itbKeb)
            {
                url = "http://rest-viana.gotos.id/tag/itb-bonbin";
            }
            else if (simpangDago)
            {
                url = "http://rest-viana.gotos.id/tag/itb-bonbin";
            }

            UnityWebRequest request = UnityWebRequest.Get(url);

            request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
            request.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
            request.SetRequestHeader("Access-Control-Allow-Methods", "Access-Control-Allow-Methods");
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode dataKendaraan = SimpleJSON.JSON.Parse(json);
                newValuesOfVehicle[0] = dataKendaraan[0]["sepeda"];
                newValuesOfVehicle[1] = dataKendaraan[0]["bus"];
                newValuesOfVehicle[2] = dataKendaraan[0]["truk"];
                newValuesOfVehicle[3] = dataKendaraan[0]["mobil"];
                newValuesOfVehicle[4] = dataKendaraan[0]["motor"];

                for (int i = 0; i < newValuesOfVehicle.Length; i++)
                {
                    if (newValuesOfVehicle[i] != valuesOfVehicle[i])
                    {
                        change = true;
                    }
                }
            }

            if (isThisSelected && change)
            {
                vehicleCountText[0].text = "Sepeda : " + valuesOfVehicle[0];
                vehicleCountText[1].text = "Bus : " + valuesOfVehicle[1];
                vehicleCountText[2].text = "Truk : " + valuesOfVehicle[2];
                vehicleCountText[3].text = "Mobil : " + valuesOfVehicle[3];
                vehicleCountText[4].text = "Motor : " + valuesOfVehicle[4];
                change = false;
                barChartController.setValues(valuesOfVehicle);
                barChartController.makeBarChart();
                alertUpdate.SetActive(true);
            }
        }
    }
    IEnumerator DataDummy()
    {
        while (true)
        {
            valuesOfVehicle[0] = Random.Range(0, 5);
            valuesOfVehicle[1] = Random.Range(0, 7);
            valuesOfVehicle[2] = Random.Range(0, 10);
            valuesOfVehicle[3] = Random.Range(0, 150);
            valuesOfVehicle[4] = Random.Range(0, 150);

            barChartController.setValues(valuesOfVehicle);
            barChartController.makeBarChart();
            if (isThisSelected)
            {
                vehicleCountText[0].text = "Sepeda : " + valuesOfVehicle[0];
                vehicleCountText[1].text = "Bus : " + valuesOfVehicle[1];
                vehicleCountText[2].text = "Truk : " + valuesOfVehicle[2];
                vehicleCountText[3].text = "Mobil : " + valuesOfVehicle[3];
                vehicleCountText[4].text = "Motor : " + valuesOfVehicle[4];
            }
            alertUpdate.SetActive(true);
            yield return new WaitForSeconds(20);
        }
    }
}
