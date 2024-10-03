using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GamePerbaruiManager : MonoBehaviour
{
    public string APIUrl = "https://blockchain-itb.com";
    private string token;
    [Header("Data Diri")]
    public string statusPerkawinan;
    public string pendidikanTerakhir;
    public string agama;
    public string pekerjaan;

    [Header("alamat")]
    public string alamat;
    public string provinsi;
    public string kotaKab;
    public string kecamatan;
    public string kelurahanDesa;
    public string RT;
    public string RW;

    [Header("foto")]
    public byte[] pasFoto;
    public byte[] kkFoto;
    public byte[] domisiliFoto;
    public byte[] SKKFoto;
    public byte[] AktaNikahFoto;
    public byte[] IjasahFoto;
    public byte[] pindahAgamaFoto;
    public byte[] tandaTangan;

    [Header("GameObject")]
    public KTPDialogPerbaruiController ktpPerbarui;
    private void Start()
    {
        token = PlayerPrefs.GetString("token");
    }
    public void UpdateDataToAPI()
    {
        StartCoroutine(SendData());
    }
    IEnumerator SendData()
    {
        WWWForm formData = new WWWForm();

        if (ktpPerbarui.isAgama)
        {
            formData.AddField("religion", agama);
            Debug.Log(agama);
            Debug.Log(pindahAgamaFoto[0]);
        }
        if (ktpPerbarui.isAlamat)
        {
            formData.AddField("address_province", provinsi);
            formData.AddField("address_city", kotaKab);
            formData.AddField("address_sub_district", kecamatan);
            formData.AddField("address_village", kelurahanDesa);
            formData.AddField("address_rt", RT);
            formData.AddField("address_rw", RW);
            formData.AddField("address_street", alamat);
            Debug.Log(provinsi);
            Debug.Log(kotaKab);
            Debug.Log(kecamatan);
            Debug.Log(kelurahanDesa);
            Debug.Log(RT);
            Debug.Log(RW);
            Debug.Log(alamat);
            Debug.Log(domisiliFoto[0]);
        }
        if (ktpPerbarui.isStatusPerkawinan)
        {
            formData.AddField("marital_status", statusPerkawinan);
        }
        if (ktpPerbarui.isPendidikanTerakhir)
        {
            formData.AddField("last_education", pendidikanTerakhir);
        }
        if (ktpPerbarui.isPekerjaan)
        {
            formData.AddField("job", pekerjaan);
        }

        if (pasFoto != null)
        {
            formData.AddBinaryData("profile_image", pasFoto, "Profile_Image.jpg", "image/jpg");
            Debug.Log(pasFoto[0]);
        }
        if (ktpPerbarui.isAgama)
        {
            formData.AddBinaryData("belief_change_letter", pindahAgamaFoto, "SuratKeteranganPindahAgama.jpg", "image/jpg");
        }
        if (ktpPerbarui.isAlamat)
        {
            formData.AddBinaryData("proof_of_residence", domisiliFoto, "Domisili.jpg", "image/jpg");
        }
        if (ktpPerbarui.isStatusPerkawinan)
        {
            formData.AddBinaryData("marriage_certificate", AktaNikahFoto, "AktaNikahCerai.jpg", "image/jpg");
        }
        if (ktpPerbarui.isPendidikanTerakhir)
        {
            formData.AddBinaryData("education_certificate", IjasahFoto, "ijazah.jpg", "image/jpg");
        }
        if (ktpPerbarui.isPekerjaan)
        {
            formData.AddBinaryData("certificate_of_employment", SKKFoto, "SuratKeteranganKerja.jpg", "image/jpg");
        }

        formData.AddBinaryData("signature_image", tandaTangan, "TandaTangan.jpg", "image/jpg");
        Debug.Log(tandaTangan[0]);
        formData.AddBinaryData("family_card_image", kkFoto, "family_card_image.jpg", "image/jpg");
        Debug.Log(kkFoto[0]);

        UnityWebRequest request = UnityWebRequest.Post(APIUrl + "/resident-identity/update", formData);

        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("access-token", token);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
            ktpPerbarui.index = 13;
            ktpPerbarui.nextLine();
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            ktpPerbarui.ktpTrigger.IsNotInputing();
            ktpPerbarui.index++;
            ktpPerbarui.nextLine();
        }
    }
}
