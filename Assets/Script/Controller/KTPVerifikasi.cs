using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class KTPVerifikasi : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject KTPPage;
    public GameObject ktpGenerator;

    public TextMeshProUGUI provinsiText;
    public TextMeshProUGUI kotaText;
    public TextMeshProUGUI noNIKText;
    public TextMeshProUGUI namaText;
    public TextMeshProUGUI tempatTGLLahirText;
    public TextMeshProUGUI jenisKelaminText;
    public TextMeshProUGUI alamatText;
    public TextMeshProUGUI RTRWText;
    public TextMeshProUGUI kelDesaText;
    public TextMeshProUGUI KecamatanText;
    public TextMeshProUGUI AgamaText;
    public TextMeshProUGUI statusPerkawinanText;
    public TextMeshProUGUI pekerjaanText;
    public TextMeshProUGUI KewargaanText;
    public TextMeshProUGUI golonganDarahText;
    public TextMeshProUGUI PenetapanText;

    public TextMeshProUGUI UsernameText;
    public TextMeshProUGUI EmailText;

    public RawImage pasFoto;
    public RawImage tandaTanganFoto;


    [Header("Condition")]
    public bool isVerified;
    public bool isPageOpen;

    [Header("Data")]
    public string noNIK;
    public int verifikasi;
    public string usernamePlayer;
    public KTPGenerator respond;

    private void Start()
    {
        usernamePlayer = PlayerPrefs.GetString("namaPlayer");
        noNIK = PlayerPrefs.GetString("NIK");
        isPageOpen = false;
        verifikasi = PlayerPrefs.GetInt("status");
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        ktpGenerator.SetActive(false);
        UnityWebRequest request = UnityWebRequest.Get("https://blockchain-itb.com/resident-identity/get-personal-identity");

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
            respond = JsonUtility.FromJson<KTPGenerator>(request.downloadHandler.text);

            UsernameText.text = usernamePlayer;
            EmailText.text = PlayerPrefs.GetString("emailPlayer");
            verifikasi = int.Parse(respond.data.resident.result.verification_status);
            PlayerPrefs.SetInt("status", int.Parse(respond.data.resident.result.verification_status));
            if (verifikasi == 2)
            {
                ktpGenerator.SetActive(true);

                //data data

                provinsiText.text = "PROVINSI " + respond.data.resident.result.address_province.ToUpper();
                kotaText.text = respond.data.resident.result.address_city.ToUpper();
                KecamatanText.text = respond.data.resident.result.address_sub_district.ToUpper();
                kelDesaText.text = respond.data.resident.result.address_village.ToUpper();
                RTRWText.text = respond.data.resident.result.address_rt + "/" + respond.data.resident.result.address_rw;
                alamatText.text = respond.data.resident.result.address_street.ToUpper();

                noNIKText.text = noNIK;
                namaText.text = respond.data.resident.result.full_name.ToUpper();
                tempatTGLLahirText.text = respond.data.resident.result.birth_place.ToUpper();
                jenisKelaminText.text = respond.data.resident.result.gender.ToUpper();
                golonganDarahText.text = respond.data.resident.result.blood_type.ToUpper();

                AgamaText.text = respond.data.resident.result.religion.ToUpper();
                statusPerkawinanText.text = respond.data.resident.result.marital_status.ToUpper();
                pekerjaanText.text = respond.data.resident.result.job.ToUpper();
                KewargaanText.text = respond.data.resident.result.nationality.ToUpper();

                //profilePic

                UnityWebRequest ProfilePicture = UnityWebRequestTexture.GetTexture(respond.data.resident.result.profile_image);

                yield return ProfilePicture.SendWebRequest();

                if (ProfilePicture.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(ProfilePicture);

                    pasFoto.texture = downloadedTexture;
                }
                else
                {
                    Debug.LogError("Failed to download texture. Error: " + ProfilePicture.error);
                }
                //TTD
                UnityWebRequest TTD = UnityWebRequestTexture.GetTexture(respond.data.resident.result.signature_image);

                yield return TTD.SendWebRequest();

                if (TTD.result == UnityWebRequest.Result.Success)
                {
                    Texture downloadedTexture = DownloadHandlerTexture.GetContent(TTD);

                    tandaTanganFoto.texture = downloadedTexture;
                }
                formatDate(respond.data.resident.result.birthday);
                formatPenetapan(respond.data.resident.result.verified_at);
            }
            else
            {
                ktpGenerator.SetActive(false);
                StopAllCoroutines();
            }
        }
    }
    public void formatDate(string unfilterDate)
    {
        string day;
        string month;
        string year;

        day = unfilterDate[0].ToString() + unfilterDate[1].ToString();
        month = unfilterDate[3].ToString() + unfilterDate[4].ToString();
        year = unfilterDate[6].ToString() + unfilterDate[7].ToString() + unfilterDate[8].ToString() + unfilterDate[9].ToString();

        tempatTGLLahirText.text +=", " + day + "-" + month + "-" + year;
    }
    public void formatPenetapan(string unfilterDate)
    {
        string day;
        string month;
        string year;

        day = unfilterDate[5].ToString() + unfilterDate[6].ToString();
        month = unfilterDate[8].ToString() + unfilterDate[9].ToString() + unfilterDate[10].ToString();
        year = unfilterDate[12].ToString() + unfilterDate[13].ToString() + unfilterDate[14].ToString() + unfilterDate[15].ToString();

        if(month == "Jan")
        {
            month = "01";
        }else if (month == "Feb")
        {
            month = "02";
        }
        else if (month == "Mar")
        {
            month = "03";
        }
        else if (month == "Apr")
        {
            month = "04";
        }
        else if (month == "Mei")
        {
            month = "05";
        }
        else if (month == "Jun")
        {
            month = "06";
        }
        else if (month == "Jul")
        {
            month = "07";
        }
        else if (month == "Agu")
        {
            month = "08";
        }
        else if (month == "Sep")
        {
            month = "09";
        }
        else if (month == "Okt")
        {
            month = "10";
        }
        else if (month == "Nov")
        {
            month = "11";
        }
        else if (month == "Des")
        {
            month = "12";
        }

        PenetapanText.text = respond.data.resident.result.address_city.ToUpper()+ "\n" + day + "-" + month + "-" + year;
    }
    public void OpenOrClose()
    {
        isPageOpen = !isPageOpen;
        KTPPage.SetActive(isPageOpen);

        if (isPageOpen == true)
        {
            StartCoroutine(GetData());
        }
    }

}
