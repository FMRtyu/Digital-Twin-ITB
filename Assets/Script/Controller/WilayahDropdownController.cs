using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class WilayahDropdownController : MonoBehaviour
{
    private string URL = "https://www.emsifa.com/api-wilayah-indonesia/api";
    //provinsi
    [SerializeField] private string[] IdProvinsi;
    [SerializeField] private string[] Namaprovinsi;
    [SerializeField] private string chosenIdProvinsi;
    [SerializeField] private string chosenNamaProvinsi;
    //Kota / Kabupaten
    [SerializeField] private string[] IdKotaKab;
    [SerializeField] private string[] NamaKotaKab;
    [SerializeField] private string chosenIdKotaKab;
    [SerializeField] private string chosenNamaKotaKab;
    //Kecamatan
    [SerializeField] private string[] IdKecamatan;
    [SerializeField] private string[] NamaKecamatan;
    [SerializeField] private string chosenIdKecamatan;
    [SerializeField] private string chosenNamaKecamatan;
    //Kelurahan / Desa
    [SerializeField] private string[] IdKelDesa;
    [SerializeField] private string[] NamaKelDesa;
    [SerializeField] private string chosenIdKelDesa;
    [SerializeField] private string chosenNamaKelDesa;
    private TMP_Dropdown[] dropdownAll;
    private TMP_InputField[] inputFieldAll;
    public GameManager gameManager;
    // Start is called before the first frame update
    private void Awake()
    {
        IdProvinsi = new string[34];
        Namaprovinsi = new string[34];
        dropdownAll = GetComponentsInChildren<TMP_Dropdown>();
        inputFieldAll = GetComponentsInChildren<TMP_InputField>();
        StartCoroutine(GetProvinsi());
    }
    public void GetDataKota()
    {
        int idDropdown = dropdownAll[0].value - 1;
        if (idDropdown >= 0)
        {
            chosenIdProvinsi = IdProvinsi[idDropdown];
            chosenNamaProvinsi = Namaprovinsi[idDropdown];
            StartCoroutine(GetKota());
        }
        else
        {
            dropdownAll[1].ClearOptions();
            dropdownAll[1].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            dropdownAll[2].ClearOptions();
            dropdownAll[2].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            dropdownAll[3].ClearOptions();
            dropdownAll[3].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
        }
    }
    public void GetDataKecamatan()
    {
        int idDropdown = dropdownAll[1].value - 1;
        if (idDropdown >= 0)
        {
            chosenIdKotaKab = IdKotaKab[idDropdown];
            chosenNamaKotaKab = NamaKotaKab[idDropdown];
            StartCoroutine(GetKecamatan());
        }
        else
        {
            dropdownAll[2].ClearOptions();
            dropdownAll[2].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            dropdownAll[3].ClearOptions();
            dropdownAll[3].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
        }
    }
    public void GetDataKelurahan()
    {
        int idDropdown = dropdownAll[2].value - 1;
        if (idDropdown >= 0)
        {
            chosenIdKecamatan = IdKecamatan[idDropdown];
            chosenNamaKecamatan = NamaKecamatan[idDropdown];
            StartCoroutine(GetKelurahan());
        }
        else
        {
            dropdownAll[3].ClearOptions();
            dropdownAll[3].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
        }
    }
    public void SaveData()
    {
        int idDropdown = dropdownAll[3].value - 1;
        chosenIdKelDesa = IdKelDesa[idDropdown];
        chosenNamaKelDesa = NamaKelDesa[idDropdown];
        gameManager.alamat = inputFieldAll[0].text;
        gameManager.provinsi = chosenNamaProvinsi;
        gameManager.kotaKab = chosenNamaKotaKab;
        gameManager.kecamatan = chosenNamaKecamatan;
        gameManager.kelurahanDesa = chosenNamaKelDesa;
        gameManager.RT = inputFieldAll[1].text;
        gameManager.RW = inputFieldAll[2].text;
    }
    IEnumerator GetProvinsi()
    {
        using(UnityWebRequest request = UnityWebRequest.Get(URL + "/provinces.json"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

                for (int i = 0; i < 34; i++)
                {
                    IdProvinsi[i] = stats[i]["id"];
                    Namaprovinsi[i] = stats[i]["name"];
                    
                }
                foreach (var item in Namaprovinsi)
                {
                    dropdownAll[0].options.Add(new TMP_Dropdown.OptionData(item));
                }
                
            }
        }
    }
    IEnumerator GetKota()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL + "/regencies/"+ chosenIdProvinsi + ".json"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
                int length = stats.Count;
                IdKotaKab = new string[length];
                NamaKotaKab = new string[length];

                for (int i = 0; i < length; i++)
                {
                    IdKotaKab[i] = stats[i]["id"];
                    NamaKotaKab[i] = stats[i]["name"];

                }
                dropdownAll[1].ClearOptions();
                dropdownAll[1].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
                foreach (var item in NamaKotaKab)
                {
                    
                    dropdownAll[1].options.Add(new TMP_Dropdown.OptionData(item));
                }

            }
        }
    }
    IEnumerator GetKecamatan()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL + "/districts/" + chosenIdKotaKab + ".json"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
                int length = stats.Count;
                IdKecamatan = new string[length];
                NamaKecamatan = new string[length];

                for (int i = 0; i < length; i++)
                {
                    IdKecamatan[i] = stats[i]["id"];
                    NamaKecamatan[i] = stats[i]["name"];

                }
                dropdownAll[2].ClearOptions();
                dropdownAll[2].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
                foreach (var item in NamaKecamatan)
                {

                    dropdownAll[2].options.Add(new TMP_Dropdown.OptionData(item));
                }

            }
        }
    }
    IEnumerator GetKelurahan()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL + "/villages/" + chosenIdKecamatan + ".json"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
                int length = stats.Count;
                IdKelDesa = new string[length];
                NamaKelDesa = new string[length];

                for (int i = 0; i < length; i++)
                {
                    IdKelDesa[i] = stats[i]["id"];
                    NamaKelDesa[i] = stats[i]["name"];

                }
                dropdownAll[3].ClearOptions();
                dropdownAll[3].options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
                foreach (var item in NamaKelDesa)
                {

                    dropdownAll[3].options.Add(new TMP_Dropdown.OptionData(item));
                }

            }
        }
    }

}
