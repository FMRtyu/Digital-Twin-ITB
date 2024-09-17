using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class EditDataController : MonoBehaviour
{

    //biodata lengkap
    public string jenisKelamin;
    public string golonganDarah;
    public string agama;
    public string pekerjaan;
    public string statusPerkawinan;
    public string kewarganegaraan;

    //inputfield
    public TMP_InputField namaLengkapInput;
    public TMP_InputField noKKInput;
    public TMP_InputField noNIKInput;
    public TMP_InputField AlamatInput;
    public TMP_Dropdown provinsiInput;
    public TMP_Dropdown kotaKabInput;
    public TMP_Dropdown kecamatanInput;
    public TMP_Dropdown kelurahanDesaInput;
    public TMP_InputField RTInput;
    public TMP_InputField RWInput;
    public Toggle lakiLakiToggle;
    public Toggle perempuanToggle;
    public TMP_InputField tempatLahirInput;
    public TMP_InputField tanggalLahirInput;
    public TMP_Dropdown golonganDarahInput;
    public TMP_Dropdown agamaInput;
    public TMP_InputField pekerjaanInput;
    public TMP_Dropdown statusPerkawinanInput;
    public Toggle WNIToggle;
    public Toggle WNAToggle;
    public TMP_InputField pendidikanTerakhirInput;

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

    public LoadingController loading;
    public GameObject errPage;
    public GameObject MenuPage;

    public DataByNIK respondDataEdit;

    public Button simpanBTN;
    public Button kembaliBTN;

    public GameObject DonePage;

    public ScrollRect myScrollRect;
    public Scrollbar newScrollBar;
    private void Start()
    {
        IdProvinsi = new string[34];
        Namaprovinsi = new string[34];
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

    public void SetKelurahanData()
    {
        int idDropdown = kelurahanDesaInput.value - 1;
        if (idDropdown >= 0)
        {
            chosenIdKelDesa = IdKelDesa[idDropdown];
            chosenNamaKelDesa = NamaKelDesa[idDropdown];
        }
    }
    public void ResetAllDataAPI()
    {
        myScrollRect.verticalNormalizedPosition = 1f;

        simpanBTN.interactable = true;
        kembaliBTN.interactable = true;

        chosenIdProvinsi = string.Empty;
        chosenNamaProvinsi = string.Empty;
        chosenIdKotaKab = string.Empty;
        chosenNamaKotaKab = string.Empty;
        chosenIdKecamatan = string.Empty;
        chosenNamaKecamatan = string.Empty;
        chosenIdKelDesa = string.Empty;
        chosenNamaKelDesa = string.Empty;

        namaLengkapInput.text = string.Empty;
        noKKInput.interactable = false;
        noNIKInput.interactable = false;
        noKKInput.text = string.Empty;
        noNIKInput.text = string.Empty;
        RTInput.text = string.Empty;
        RWInput.text = string.Empty;
        tempatLahirInput.text = string.Empty;
        tanggalLahirInput.text = string.Empty;
        pekerjaanInput.text = string.Empty;
        pendidikanTerakhirInput.text = string.Empty;

        provinsiInput.ClearOptions();
        provinsiInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

        kotaKabInput.ClearOptions();
        kotaKabInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

        kelurahanDesaInput.ClearOptions();
        kelurahanDesaInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

        kecamatanInput.ClearOptions();
        kecamatanInput.options.Add(new TMP_Dropdown.OptionData("-- pilih --"));

        lakiLakiToggle.isOn = true;
        golonganDarahInput.value = 0;
        agamaInput.value = 0;
        statusPerkawinanInput.value = 0;
        WNIToggle.isOn = true;

        errPage.SetActive(false);
        loading.gameObject.SetActive(true);
    }

    public void GenderLaki()
    {
        jenisKelamin = "Laki - laki";
    }
    public void GenderPerempuan()
    {
        jenisKelamin = "Perempuan";
    }

    public void SetWNI()
    {
        kewarganegaraan = "WNI";
    }
    public void SetWNA()
    {
        kewarganegaraan = "WNA";
    }

    public void GetDataFromAPI(string NIK)
    {
        StartCoroutine(GetData(NIK));
    }

    IEnumerator GetData(string NIK)
    {
        loading.WasLoading();
        // UnityWebRequest request = new UnityWebRequest(apiUrl + "/auth/register", "POST");
        UnityWebRequest request = UnityWebRequest.Get("https://blockchain-itb.com/resident-identity/find?nik=" + NIK);

        //byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        //request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("token", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("access-token", PlayerPrefs.GetString("token"));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            respondDataEdit = JsonUtility.FromJson<DataByNIK>(request.downloadHandler.text);
            if (respondDataEdit.message == "Get resident by NIK success!")
            {


                //setTextOnly
                namaLengkapInput.text = respondDataEdit.data.resident.full_name;
                noKKInput.text = respondDataEdit.data.resident.family_card_number;
                noNIKInput.text = NIK;
                noKKInput.interactable = false;
                noNIKInput.interactable = false;
                RTInput.text = respondDataEdit.data.resident.address_rt;
                RWInput.text = respondDataEdit.data.resident.address_rw;
                tempatLahirInput.text = respondDataEdit.data.resident.birth_place;
                tanggalLahirInput.text = respondDataEdit.data.resident.birthday;
                pekerjaanInput.text = respondDataEdit.data.resident.job;
                pendidikanTerakhirInput.text = respondDataEdit.data.resident.last_education;
                AlamatInput.text = respondDataEdit.data.resident.address_street;

                //setWilayah
                //Provinsi
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
                            if (Namaprovinsi[i].ToLower() == respondDataEdit.data.resident.address_province.ToLower())
                            {
                                provinsiInput.value = i + 1;
                                chosenIdProvinsi = IdProvinsi[i];
                            }
                        }
                    }
                }
                //set Jenis Kelamin
                jenisKelamin = respondDataEdit.data.resident.gender;

                if (jenisKelamin == "Laki - laki")
                {
                    lakiLakiToggle.isOn = true;
                }
                else if (jenisKelamin == "Perempuan")
                {
                    perempuanToggle.isOn = true;
                }
                //set golongan darah
                for (int i = 0; i < golonganDarahInput.options.Count; i++)
                {
                    if (golonganDarahInput.options[i].text == respondDataEdit.data.resident.blood_type)
                    {
                        golonganDarahInput.value = i;
                    }
                }
                //set agama
                for (int i = 0; i < agamaInput.options.Count; i++)
                {
                    if (agamaInput.options[i].text == respondDataEdit.data.resident.religion)
                    {
                        agamaInput.value = i;
                    }
                }
                //status kawin
                for (int i = 0; i < statusPerkawinanInput.options.Count; i++)
                {
                    if (statusPerkawinanInput.options[i].text == respondDataEdit.data.resident.marital_status)
                    {
                        statusPerkawinanInput.value = i;
                    }
                }
                //kewarganegaraan
                kewarganegaraan = respondDataEdit.data.resident.nationality;

                if (kewarganegaraan == "WNI")
                {
                    WNIToggle.isOn = true;
                }
                else if (kewarganegaraan == "WNA")
                {
                    WNAToggle.isOn = true;
                }
                setKotaDataEdit();
            }
            else
            {
                errPage.SetActive(true);
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
            Debug.LogError(KotaKab.error);
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
                if (NamaKotaKab[i].ToLower() == respondDataEdit.data.resident.address_city.ToLower())
                {
                    kotaKabInput.value = i + 1;
                    chosenIdKotaKab = IdKotaKab[i];
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
            Debug.LogError(Kecamatan.error);
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
                if (NamaKecamatan[i].ToLower() == respondDataEdit.data.resident.address_sub_district.ToLower())
                {
                    kecamatanInput.value = i + 1;
                    chosenIdKecamatan = IdKecamatan[i];
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
            Debug.LogError(Kelurahan.error);
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
                if (NamaKelDesa[i].ToLower() == respondDataEdit.data.resident.address_village.ToLower())
                {
                    kelurahanDesaInput.value = i + 1;
                    chosenIdKelDesa = IdKecamatan[i];
                }
            }
            loading.DoneLoading();
            loading.gameObject.SetActive(false);
        }
    }
    public void SaveDataToAPI()
    {
        simpanBTN.interactable = false;
        kembaliBTN.interactable = false;
        StopAllCoroutines();
        StartCoroutine(sendDataToApi());
    }

    IEnumerator sendDataToApi()
    {
        WWWForm form = new WWWForm();

        form.AddField("nik", noNIKInput.text);
        form.AddField("full_name", namaLengkapInput.text);
        form.AddField("family_card_number", noKKInput.text);

        form.AddField("address_province", chosenNamaProvinsi);
        form.AddField("address_city", chosenNamaKotaKab);
        form.AddField("address_sub_district", chosenNamaKecamatan);
        form.AddField("address_village", chosenNamaKelDesa);
        form.AddField("address_street", AlamatInput.text);
        form.AddField("address_rt", RTInput.text);
        form.AddField("address_rw", RWInput.text);

        form.AddField("birth_place", tempatLahirInput.text);
        form.AddField("birthday", tanggalLahirInput.text);
        form.AddField("gender", jenisKelamin);
        form.AddField("blood_type", golonganDarahInput.options[golonganDarahInput.value].text);
        form.AddField("religion", agamaInput.options[agamaInput.value].text);
        form.AddField("marital_status", statusPerkawinanInput.options[statusPerkawinanInput.value].text);
        form.AddField("job", pekerjaanInput.text);
        form.AddField("nationality", kewarganegaraan);
        form.AddField("last_education", pendidikanTerakhirInput.text);

        UnityWebRequest request = UnityWebRequest.Post("https://blockchain-itb.com/resident-identity/update-by-nik", form);

        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("access-token", PlayerPrefs.GetString("token"));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("sesuatu terjadi");
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            ResetAllDataAPI();
            MenuPage.SetActive(true);
            MenuPage.GetComponent<GetDataFromAPIController>().RefreshDataFromAPI();
            DonePage.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
