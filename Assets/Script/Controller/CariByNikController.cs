using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CariByNikController : MonoBehaviour
{
    //data
    public DataByNIK respond;
    public TMP_InputField NIKData;

    public TextMeshProUGUI namaLengkap;
    public TextMeshProUGUI noKK;
    public TextMeshProUGUI NoNIK;
    public TextMeshProUGUI alamat;
    public TextMeshProUGUI TempatTanggalLahir;
    public TextMeshProUGUI jenisKelamin;
    public TextMeshProUGUI golonganDarah;
    public TextMeshProUGUI agama;
    public TextMeshProUGUI pekerjaan;
    public TextMeshProUGUI statusPerkawinan;
    public TextMeshProUGUI kewarganegaraan;
    public TextMeshProUGUI pendidikanTerakhir;
    public TextMeshProUGUI tipePendaftaran;
    public TextMeshProUGUI tanggalPendaftaran;
    public GameObject[] statusPendaftaran;
    public RawImage profilePicture;
    public RawImage TTDPicture;
    private Texture2D profilePic;

    //gameobject
    public GameObject Detail;
    public GameObject searchBar;
    public GameObject ERROR;

    public Button BTNSearch;

    public Texture defaultSkin;

    public DocumentController documentController;
    public GameObject LoadingPage;
    public void ResetAllText()
    {
        namaLengkap.text = string.Empty;
        noKK.text = string.Empty;
        NoNIK.text = string.Empty;
        alamat.text = string.Empty;
        TempatTanggalLahir.text = string.Empty;
        jenisKelamin.text = string.Empty;
        golonganDarah.text = string.Empty;
        agama.text = string.Empty;
        pekerjaan.text = string.Empty;
        statusPerkawinan.text = string.Empty;
        kewarganegaraan.text = string.Empty;
        pendidikanTerakhir.text = string.Empty;
        tipePendaftaran.text = string.Empty;
        tanggalPendaftaran.text = string.Empty;

        documentController.kkLink = string.Empty;
        documentController.DomisiliLink = string.Empty;
        documentController.SKKLink = string.Empty;
        documentController.AktaNikahLink = string.Empty;
        documentController.IjazahLink = string.Empty;
        documentController.SKPALink = string.Empty;

        documentController.kkBTN.interactable = false;
        documentController.DomisiliBTN.interactable = false;
        documentController.SKKBTN.interactable = false;
        documentController.AktaNikahBTN.interactable = false;
        documentController.IjazahBTN.interactable = false;
        documentController.SKPABTN.interactable = false;


        statusPendaftaran[0].SetActive(false);
        statusPendaftaran[1].SetActive(false);
        statusPendaftaran[2].SetActive(false);
        profilePicture.texture = defaultSkin;
        LoadingPage.SetActive(true);
    }
    public void SearchByNIK()
    {
        StartCoroutine(GetData(NIKData.text));
    }
    public void SearchByNIKFromString(string NIKString)
    {
        StartCoroutine(GetData(NIKString));
    }
    public void SearchByNIKWithDocumentFromString(string NIKString)
    {
        StartCoroutine(GetDataWithDocument(NIKString));
    }
    //untuk detail admin
    IEnumerator GetDataWithDocument(string NIK)
    {
        LoadingPage.GetComponent<LoadingController>().WasLoading();
        // UnityWebRequest request = new UnityWebRequest(apiUrl + "/auth/register", "POST");
        UnityWebRequest request = UnityWebRequest.Get("https://blockchain-itb.com/resident-identity/find?nik=" + NIK + "&include_document=true");

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
            ERROR.SetActive(true);
        }
        else
        {
            respond = JsonUtility.FromJson<DataByNIK>(request.downloadHandler.text);
            if(respond.error == "true")
            {
                ERROR.SetActive(true);
            }
            else
            {
                namaLengkap.text = respond.data.resident.full_name;
                noKK.text = respond.data.resident.family_card_number;
                NoNIK.text = NIK;
                alamat.text = respond.data.resident.address_street
                    + " RT " + respond.data.resident.address_rt
                    + " RW " + respond.data.resident.address_rw
                    + ", " + respond.data.resident.address_village
                    + ", " + respond.data.resident.address_sub_district
                    + ", " + respond.data.resident.address_city
                    + ", " + respond.data.resident.address_province;
                TempatTanggalLahir.text = respond.data.resident.birth_place + ", " + respond.data.resident.birthday;
                jenisKelamin.text = respond.data.resident.gender;
                golonganDarah.text = respond.data.resident.blood_type;
                agama.text = respond.data.resident.religion;
                pekerjaan.text = respond.data.resident.job;
                statusPerkawinan.text = respond.data.resident.marital_status;
                kewarganegaraan.text = respond.data.resident.nationality;
                pendidikanTerakhir.text = respond.data.resident.last_education;
                tipePendaftaran.text = respond.data.resident.registration_type;
                tanggalPendaftaran.text = respond.data.resident.created_at;

                if (respond.data.resident.verification_status == "1")
                {
                    statusPendaftaran[0].SetActive(true);
                }
                else if (respond.data.resident.verification_status == "2")
                {
                    statusPendaftaran[1].SetActive(true);
                }
                else if (respond.data.resident.verification_status == "0")
                {
                    statusPendaftaran[2].SetActive(true);
                }

                //profilePic

                UnityWebRequest ProfilePicture = UnityWebRequestTexture.GetTexture(respond.data.resident.profile_image);
                
                yield return ProfilePicture.SendWebRequest();

                if (ProfilePicture.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(ProfilePicture);

                    profilePicture.texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + ProfilePicture.error);
                }
                //set link
                documentController.kkLink = respond.data.document.family_card_image;
                documentController.DomisiliLink = respond.data.document.proof_of_residence;
                documentController.SKKLink = respond.data.document.certificate_of_employment;
                documentController.AktaNikahLink = respond.data.document.marriage_certificate;
                documentController.IjazahLink = respond.data.document.education_certificate;
                documentController.SKPALink = respond.data.document.belief_change_letter;
                //set sidik jari
                //jari1
                UnityWebRequest SidikPicture1 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_r1);

                yield return SidikPicture1.SendWebRequest();

                if (SidikPicture1.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture1);

                    documentController.sidikJari[0].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture1.error);
                }

                //jari2
                UnityWebRequest SidikPicture2 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_r2);

                yield return SidikPicture2.SendWebRequest();

                if (SidikPicture2.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture2);

                    documentController.sidikJari[1].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture2.error);
                }
                //jari3
                UnityWebRequest SidikPicture3 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_r3);

                yield return SidikPicture3.SendWebRequest();

                if (SidikPicture3.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture3);

                    documentController.sidikJari[2].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture3.error);
                }
                //jari4
                UnityWebRequest SidikPicture4 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_r4);

                yield return SidikPicture4.SendWebRequest();

                if (SidikPicture4.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture4);

                    documentController.sidikJari[3].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture4.error);
                }

                //jari5
                UnityWebRequest SidikPicture5 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_r5);

                yield return SidikPicture5.SendWebRequest();

                if (ProfilePicture.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture5);

                    documentController.sidikJari[4].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture5.error);
                }

                //jari6
                UnityWebRequest SidikPicture6 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_l1);

                yield return SidikPicture6.SendWebRequest();

                if (SidikPicture6.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture6);

                    documentController.sidikJari[5].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture6.error);
                }

                //jari7
                UnityWebRequest SidikPicture7 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_l2);

                yield return SidikPicture7.SendWebRequest();

                if (SidikPicture7.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture7);

                    documentController.sidikJari[6].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture7.error);
                }

                //jari8
                UnityWebRequest SidikPicture8 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_l3);

                yield return SidikPicture8.SendWebRequest();

                if (SidikPicture8.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture8);

                    documentController.sidikJari[7].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture8.error);
                }

                //jari9
                UnityWebRequest SidikPicture9 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_l4);

                yield return SidikPicture9.SendWebRequest();

                if (SidikPicture9.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture9);

                    documentController.sidikJari[8].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture9.error);
                }

                //jari10
                UnityWebRequest SidikPicture10 = UnityWebRequestTexture.GetTexture(respond.data.document.fingerprints_image.fingerprint_l5);

                yield return SidikPicture10.SendWebRequest();

                if (SidikPicture10.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(SidikPicture10);

                    documentController.sidikJari[9].texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + SidikPicture10.error);
                }

                //set irismata
                UnityWebRequest irisMata = UnityWebRequestTexture.GetTexture(respond.data.document.iris_image);

                yield return irisMata.SendWebRequest();

                if (irisMata.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(irisMata);

                    documentController.irisMata.texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + irisMata.error);
                }

                //TTD
                UnityWebRequest TTD = UnityWebRequestTexture.GetTexture(respond.data.resident.signature_image);

                yield return TTD.SendWebRequest();

                if (TTD.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(TTD);

                    TTDPicture.texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + ProfilePicture.error);
                }
                formatDate(respond.data.resident.created_at);
                LoadingPage.GetComponent<LoadingController>().DoneLoading();
                LoadingPage.SetActive(false);

            }

        }
    }
    //untuk detail instansi
    IEnumerator GetData(string NIK)
    {
        if (LoadingPage != null)
        {
            LoadingPage.SetActive(true);
            LoadingPage.GetComponent<LoadingController>().WasLoading();
        }
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
            ERROR.SetActive(true);
            if (BTNSearch != null)
            {
                BTNSearch.interactable = true;
            }
        }
        else
        {
            if (BTNSearch != null)
            {
                BTNSearch.interactable = true;
            }
            DataByNIK respond = JsonUtility.FromJson<DataByNIK>(request.downloadHandler.text);

            if (respond.error == "true")
            {
                ERROR.SetActive(true);
                if (LoadingPage != null)
                {
                    LoadingPage.SetActive(false);
                    LoadingPage.GetComponent<LoadingController>().DoneLoading();
                }
            }
            else
            {
                namaLengkap.text = respond.data.resident.full_name;
                noKK.text = respond.data.resident.family_card_number;
                NoNIK.text = NIK;
                alamat.text = respond.data.resident.address_street
                    + " RT " + respond.data.resident.address_rt
                    + " RW " + respond.data.resident.address_rw
                    + ", " + respond.data.resident.address_village
                    + ", " + respond.data.resident.address_sub_district
                    + ", " + respond.data.resident.address_city
                    + ", " + respond.data.resident.address_province;
                TempatTanggalLahir.text = respond.data.resident.birth_place + ", " + respond.data.resident.birthday;
                jenisKelamin.text = respond.data.resident.gender;
                golonganDarah.text = respond.data.resident.blood_type;
                agama.text = respond.data.resident.religion;
                pekerjaan.text = respond.data.resident.job;
                statusPerkawinan.text = respond.data.resident.marital_status;
                kewarganegaraan.text = respond.data.resident.nationality;
                pendidikanTerakhir.text = respond.data.resident.last_education;
                tipePendaftaran.text = respond.data.resident.registration_type;
                //tanggalPendaftaran.text = respond.data.resident.created_at;

                if (respond.data.resident.verification_status == "1")
                {
                    statusPendaftaran[0].SetActive(true);
                }
                else if (respond.data.resident.verification_status == "2")
                {
                    statusPendaftaran[1].SetActive(true);
                }
                else if (respond.data.resident.verification_status == "0")
                {
                    statusPendaftaran[2].SetActive(true);
                }

                //profilePic

                UnityWebRequest ProfilePicture = UnityWebRequestTexture.GetTexture(respond.data.resident.profile_image);

                yield return ProfilePicture.SendWebRequest();

                if (ProfilePicture.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(ProfilePicture);

                    profilePicture.texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + ProfilePicture.error);
                }
                //TTD
                UnityWebRequest TTD = UnityWebRequestTexture.GetTexture(respond.data.resident.signature_image);

                yield return TTD.SendWebRequest();

                if (TTD.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(TTD);

                    TTDPicture.texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + ProfilePicture.error);
                }
                if (LoadingPage != null)
                {
                    LoadingPage.SetActive(false);
                    LoadingPage.GetComponent<LoadingController>().DoneLoading();
                }
                formatDate(respond.data.resident.created_at);
                searchBar.SetActive(false);
                Detail.SetActive(true);
            }

        }
    }
    public void formatDate(string unfilterDate)
    {
        string day;
        string month;
        string year;

        day = unfilterDate[5].ToString() + unfilterDate[6].ToString();
        month = unfilterDate[8].ToString() + unfilterDate[9].ToString() + unfilterDate[10].ToString();
        year = unfilterDate[12].ToString() + unfilterDate[13].ToString() + unfilterDate[14].ToString() + unfilterDate[15].ToString();

        tanggalPendaftaran.text = day + "/" + month + "/" + year;
    }
}
