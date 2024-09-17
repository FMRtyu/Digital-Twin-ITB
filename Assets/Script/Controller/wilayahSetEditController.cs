using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class wilayahSetEditController : MonoBehaviour
{
    //provinsi
    [SerializeField] private string[] IdProvinsi;
    [SerializeField] private string[] Namaprovinsi;
    [SerializeField] private string chosenIdProvinsi;
    [SerializeField] private int chosenIdIndexProvinsi;
    [SerializeField] private string chosenNamaProvinsi;
    //Kota / Kabupaten
    [SerializeField] private string[] IdKotaKab;
    [SerializeField] private string[] NamaKotaKab;
    [SerializeField] private string chosenIdKotaKab;
    [SerializeField] private int chosenIdIndexKotaKab;
    [SerializeField] private string chosenNamaKotaKab;
    //Kecamatan
    [SerializeField] private string[] IdKecamatan;
    [SerializeField] private string[] NamaKecamatan;
    [SerializeField] private string chosenIdKecamatan;
    [SerializeField] private int chosenIdIndexKecamatan;
    [SerializeField] private string chosenNamaKecamatan;
    //Kelurahan / Desa
    [SerializeField] private string[] IdKelDesa;
    [SerializeField] private string[] NamaKelDesa;
    [SerializeField] private string chosenIdKelDesa;
    [SerializeField] private int chosenIdIndexKelDesa;
    [SerializeField] private string chosenNamaKelDesa;

    public TMP_Dropdown provinsiInput;
    public TMP_Dropdown kotaKabInput;
    public TMP_Dropdown kecamatanInput;
    public TMP_Dropdown kelurahanDesaInput;
    public KTPDialogPerbaruiController perbaruiController;

    public Button selanjutnyaBTN;

    public bool wasActive;
    public LoadingController loadingPage;
    // Start is called before the first frame update
    void Start()
    {
        IdProvinsi = new string[34];
        Namaprovinsi = new string[34];
        wasActive = false;
    }

    private void OnEnable()
    {
        StartCoroutine(GetData());
    }

    private void OnDisable()
    {
        loadingPage.gameObject.SetActive(true);
    }

    IEnumerator GetData()
    {
        loadingPage.WasLoading();
        using (UnityWebRequest Provinsi = UnityWebRequest.Get("https://www.emsifa.com/api-wilayah-indonesia/api/provinces.json"))
        {
            yield return Provinsi.SendWebRequest();

            if (Provinsi.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(Provinsi.error);
            else
            {
                string json = Provinsi.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

                for (int i = 0; i < 34; i++)
                {
                    IdProvinsi[i] = stats[i]["id"];
                    Namaprovinsi[i] = stats[i]["name"];

                }
                provinsiInput.ClearOptions();
                provinsiInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
                foreach (var item in Namaprovinsi)
                {
                    provinsiInput.options.Add(new TMP_Dropdown.OptionData(item));
                }
                bool isSelectedPro = false;
                for (int i = 0; i < 34; i++)
                {
                    if (Namaprovinsi[i].ToLower() == perbaruiController.dataKTP.respond.data.resident.result.address_province.ToLower())
                    {
                        provinsiInput.value = i + 1;
                        chosenIdProvinsi = IdProvinsi[i];
                    }
                }
                setKotaDataEdit();
            }
        }
    }

    public void GetDataKota()
    {
        int idDropdown = provinsiInput.value - 1;
        if (idDropdown >= 0)
        {
            chosenIdProvinsi = IdProvinsi[idDropdown];
            chosenNamaProvinsi = Namaprovinsi[idDropdown];
            kotaKabInput.ClearOptions();
            kotaKabInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

            kelurahanDesaInput.ClearOptions();
            kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

            kecamatanInput.ClearOptions();
            kecamatanInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            StartCoroutine(GetKota());
        }
        else
        {
            kotaKabInput.ClearOptions();
            kotaKabInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

            kelurahanDesaInput.ClearOptions();
            kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

            kecamatanInput.ClearOptions();
            kecamatanInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
        }
    }
    public void GetDataKecamatan()
    {
        int idDropdown = kotaKabInput.value - 1;
        if (idDropdown >= 0)
        {
            chosenIdKotaKab = IdKotaKab[idDropdown];
            chosenNamaKotaKab = NamaKotaKab[idDropdown];

            kelurahanDesaInput.ClearOptions();
            kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

            kecamatanInput.ClearOptions();
            kecamatanInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            StartCoroutine(GetKecamatan());
        }
        else
        {
            kecamatanInput.ClearOptions();
            kecamatanInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            kelurahanDesaInput.ClearOptions();
            kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
        }
    }
    public void GetDataKelurahan()
    {
        int idDropdown = kecamatanInput.value - 1;
        if (idDropdown >= 0)
        {
            chosenIdKecamatan = IdKecamatan[idDropdown];
            chosenNamaKecamatan = NamaKecamatan[idDropdown];
            kelurahanDesaInput.ClearOptions();
            kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            StartCoroutine(GetKelurahan());
        }
        else
        {
            kelurahanDesaInput.ClearOptions();
            kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
        }
    }
    IEnumerator GetKota()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://www.emsifa.com/api-wilayah-indonesia/api/regencies/" + chosenIdProvinsi + ".json"))
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
                kotaKabInput.ClearOptions();
                kotaKabInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
                foreach (var item in NamaKotaKab)
                {

                    kotaKabInput.options.Add(new TMP_Dropdown.OptionData(item));
                }

            }
        }
    }
    IEnumerator GetKecamatan()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://www.emsifa.com/api-wilayah-indonesia/api/districts/" + chosenIdKotaKab + ".json"))
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
                kecamatanInput.ClearOptions();
                kecamatanInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
                foreach (var item in NamaKecamatan)
                {

                    kecamatanInput.options.Add(new TMP_Dropdown.OptionData(item));
                }

            }
        }
    }
    IEnumerator GetKelurahan()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://www.emsifa.com/api-wilayah-indonesia/api/villages/" + chosenIdKecamatan + ".json"))
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
                kelurahanDesaInput.ClearOptions();
                kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
                foreach (var item in NamaKelDesa)
                {

                    kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData(item));
                }

            }
        }
    }
    private void setKotaDataEdit()
    {
        StopAllCoroutines();
        StartCoroutine(setKotaData());
    }
    IEnumerator setKotaData()
    {
        //Kota
        string urlKota = "https://www.emsifa.com/api-wilayah-indonesia/api/regencies/" + chosenIdProvinsi + ".json";
        UnityWebRequest KotaKab = UnityWebRequest.Get(urlKota);

        yield return KotaKab.SendWebRequest();

        if (KotaKab.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(KotaKab.error);
            selanjutnyaBTN.interactable = true;
        }
        else
        {
            string json = KotaKab.downloadHandler.text;
            SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
            int length = stats.Count;
            IdKotaKab = new string[length];
            NamaKotaKab = new string[length];

            for (int i = 0; i < length; i++)
            {
                IdKotaKab[i] = stats[i]["id"];
                NamaKotaKab[i] = stats[i]["name"];

            }
            kotaKabInput.ClearOptions();
            kotaKabInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            foreach (var item in NamaKotaKab)
            {

                kotaKabInput.options.Add(new TMP_Dropdown.OptionData(item));
            }
            for (int i = 0; i < length; i++)
            {
                if (NamaKotaKab[i].ToLower() == perbaruiController.dataKTP.respond.data.resident.result.address_city.ToLower())
                {
                    kotaKabInput.value = i + 1;
                    chosenIdKotaKab = IdKotaKab[i];
                }
                else
                {
                    selanjutnyaBTN.interactable = true;
                    loadingPage.gameObject.SetActive(false);
                }
            }
            setKecamatanDataEdit();
        }
    }
    private void setKecamatanDataEdit()
    {
        StopAllCoroutines();
        StartCoroutine(setKecamatanData());
    }
    IEnumerator setKecamatanData()
    {
        //kecamatan
        string urlKecamatan = "https://www.emsifa.com/api-wilayah-indonesia/api/districts/" + chosenIdKotaKab + ".json";
        UnityWebRequest Kecamatan = UnityWebRequest.Get(urlKecamatan);
        yield return Kecamatan.SendWebRequest();

        if (Kecamatan.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(Kecamatan.error);
            selanjutnyaBTN.interactable = true;
        }
        else
        {
            string json = Kecamatan.downloadHandler.text;
            SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
            int length = stats.Count;
            IdKecamatan = new string[length];
            NamaKecamatan = new string[length];

            for (int i = 0; i < length; i++)
            {
                IdKecamatan[i] = stats[i]["id"];
                NamaKecamatan[i] = stats[i]["name"];

            }
            kecamatanInput.ClearOptions();
            kecamatanInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            foreach (var item in NamaKecamatan)
            {

                kecamatanInput.options.Add(new TMP_Dropdown.OptionData(item));
            }
            for (int i = 0; i < length; i++)
            {
                if (NamaKecamatan[i].ToLower() == perbaruiController.dataKTP.respond.data.resident.result.address_sub_district.ToLower())
                {
                    kecamatanInput.value = i + 1;
                    chosenIdKecamatan = IdKecamatan[i];
                }
                else
                {
                    selanjutnyaBTN.interactable = true;
                    loadingPage.gameObject.SetActive(false);
                }
            }
            setKelurahanDataEdit();
        }
    }
    public void setKelurahanDataEdit()
    {
        StopAllCoroutines();
        StartCoroutine(setKelurahanData());
    }
    IEnumerator setKelurahanData()
    {
        //kelurahan
        string urlKelurahan = "https://www.emsifa.com/api-wilayah-indonesia/api/villages/" + chosenIdKecamatan + ".json";
        UnityWebRequest Kelurahan = UnityWebRequest.Get(urlKelurahan);
        yield return Kelurahan.SendWebRequest();

        if (Kelurahan.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(Kelurahan.error);
            selanjutnyaBTN.interactable = true;
        }
        else
        {
            string json = Kelurahan.downloadHandler.text;
            SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);
            int length = stats.Count;
            IdKelDesa = new string[length];
            NamaKelDesa = new string[length];

            for (int i = 0; i < length; i++)
            {
                IdKelDesa[i] = stats[i]["id"];
                NamaKelDesa[i] = stats[i]["name"];

            }
            kelurahanDesaInput.ClearOptions();
            kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));
            foreach (var item in NamaKelDesa)
            {

                kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData(item));
            }
            for (int i = 0; i < length; i++)
            {
                if (NamaKelDesa[i].ToLower() == perbaruiController.dataKTP.respond.data.resident.result.address_village.ToLower())
                {
                    kelurahanDesaInput.value = i + 1;
                    chosenIdKelDesa = IdKecamatan[i];
                }
                else
                {
                    selanjutnyaBTN.interactable = true;
                    loadingPage.gameObject.SetActive(false);
                }
            }
            loadingPage.DoneLoading();
            loadingPage.gameObject.SetActive(false);
            selanjutnyaBTN.interactable = true;
        }
    }
}
