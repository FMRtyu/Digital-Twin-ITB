using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//testing
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //API URL
    string apiUrl = "https://blockchain-itb.com";

    //script
    public KTPDialogController ktpDialogController;
    //gameobject
    public GameObject selectAvatarCanvas;
    public GameObject[] playerPrefabs;
    public GameObject playerSpawnPoint;

    //playerpref
    public string namaPemain;
    public string avatarPemain;
    public string tokenPlayer;

    //data diri
    public string NamaPanjang;
    public string NoKK;
    public string NoNIK;
    //alamat
    public string alamat;
    public string provinsi;
    public string kotaKab;
    public string kecamatan;
    public string kelurahanDesa;
    public string RT;
    public string RW;

    //biodata lengkap
    public string jenisKelamin;
    public string tempatLahir;
    public string tanggalLahir;
    public string golonganDarah;
    public string agama;
    public string pekerjaan;
    public string statusPerkawinan;
    public string kewarganegaraan;
    public string pendidikanTerakhir;

    //gambar
    public byte[] PasFoto;
    public TextMeshProUGUI profileFileText;
    public byte[] KartuKeluarga;
    public TextMeshProUGUI KKFileText;
    public byte[] IrisMata;
    public TextMeshProUGUI mataFileText;

    public byte[] SidikJari1;
    public byte[] SidikJari2;
    public byte[] SidikJari3;
    public byte[] SidikJari4;
    public byte[] SidikJari5;
    public byte[] SidikJari6;
    public byte[] SidikJari7;
    public byte[] SidikJari8;
    public byte[] SidikJari9;
    public byte[] SidikJari10;
    public TextMeshProUGUI sidikFileText;

    public byte[] TandaTangan;

    public GameObject FirstTimeCamera;
    private void Start()
    {
        tokenPlayer = PlayerPrefs.GetString("token");
        namaPemain = PlayerPrefs.GetString("namaPlayer");
        avatarPemain = PlayerPrefs.GetString("avatar");
        if (avatarPemain == string.Empty && tokenPlayer != string.Empty)
        {
            //tunjukin select avatar canvas
            selectAvatarCanvas.SetActive(true);
            FirstTimeCamera.SetActive(true);
        }
        else
        {
            //Cursor.visible = false;
            SpawnPlayer();
            FirstTimeCamera.SetActive(false);
        }
    }

    public void AvatarAsMen()
    {
        avatarPemain = "1";
        SendAvatarData();
    }
    public void AvatarAsWomen()
    {
        avatarPemain = "2";
        SendAvatarData();
    }

    public void SendAvatarData()
    {
        StartCoroutine(SendData());
    }

    IEnumerator SendData()
    {
        WWWForm form = new WWWForm();
        form.AddField("avatar", avatarPemain);

        UnityWebRequest request = UnityWebRequest.Post(apiUrl + "/user/select-avatar", form);

        //request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("access-token", tokenPlayer);

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("sesuatu terjadi");
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            SpawnPlayer();
        }
        

    }
    public void SetProfilePic(string base64Array)
    {
        PasFoto = System.Convert.FromBase64String(base64Array);
        profileFileText.text = "1 File chosen";

    }
    public void SetNoKKPicture(string base64Array)
    {
        KartuKeluarga = System.Convert.FromBase64String(base64Array);
        KKFileText.text = "1 File chosen";

    }
    public void SetIrisMataPicture(string base64Array)
    {
        IrisMata = System.Convert.FromBase64String(base64Array);
        mataFileText.text = "1 File chosen";

    }

    public void SetSidik1(string base64Array)
    {
        SidikJari1 = System.Convert.FromBase64String(base64Array);
    }

    public void SetSidik2(string base64Array)
    {
        SidikJari2 = System.Convert.FromBase64String(base64Array);
    }
    public void SetSidik3(string base64Array)
    {
        SidikJari3 = System.Convert.FromBase64String(base64Array);
    }
    public void SetSidik4(string base64Array)
    {
        SidikJari4 = System.Convert.FromBase64String(base64Array);
    }
    public void SetSidik5(string base64Array)
    {
        SidikJari5= System.Convert.FromBase64String(base64Array);
    }
    public void SetSidik6(string base64Array)
    {
        SidikJari6 = System.Convert.FromBase64String(base64Array);
    }
    public void SetSidik7(string base64Array)
    {
        SidikJari7 = System.Convert.FromBase64String(base64Array);
    }
    public void SetSidik8(string base64Array)
    {
        SidikJari8 = System.Convert.FromBase64String(base64Array);
    }
    public void SetSidik9(string base64Array)
    {
        SidikJari9 = System.Convert.FromBase64String(base64Array);
    }
    public void SetSidik10(string base64Array)
    {
        SidikJari10 = System.Convert.FromBase64String(base64Array);
        sidikFileText.text = "10 File chosen";
    }

    public void saveDataToDB()
    {
        ktpDialogController.tddGameobject.SetActive(false);
        ktpDialogController.cameraMain.SetActive(true);
        StartCoroutine(SendFormData());
    }
    IEnumerator SendFormData()
    {
        WWWForm form = new WWWForm();
        form.AddField("nik",NoNIK);
        form.AddField("family_card_number",NoKK);
        form.AddField("full_name",NamaPanjang);
        form.AddField("birth_place",tempatLahir);
        form.AddField("birthday",tanggalLahir);
        form.AddField("gender",jenisKelamin);
        form.AddField("blood_type",golonganDarah);
        form.AddField("address_street",alamat);
        form.AddField("address_rt",RT);
        form.AddField("address_rw",RW);
        form.AddField("address_village",kelurahanDesa);
        form.AddField("address_sub_district",kecamatan);
        form.AddField("address_city",kotaKab);
        form.AddField("address_province",provinsi);
        form.AddField("religion",agama);
        form.AddField("last_education",pendidikanTerakhir);
        form.AddField("marital_status",statusPerkawinan);
        form.AddField("job",pekerjaan);
        form.AddField("nationality",kewarganegaraan);

        form.AddBinaryData("profile_image", PasFoto, "PasFoto.jpg", "image/jpg");
        form.AddBinaryData("family_card_image", KartuKeluarga, "kk.jpg", "image/jpg");
        form.AddBinaryData("signature_image", TandaTangan, "ttd.jpg", "image/jpg");

        form.AddBinaryData("fingerprints_image", SidikJari1, "sidik1.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari2, "sidik2.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari3, "sidik3.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari4, "sidik4.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari5, "sidik5.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari6, "sidik6.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari7, "sidik7.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari8, "sidik8.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari9, "sidik9.jpg", "image/jpg");
        form.AddBinaryData("fingerprints_image", SidikJari10, "sidik10.jpg", "image/jpg");

        form.AddBinaryData("iris_image", IrisMata, "irismata.jpg", "image/jpg");


        UnityWebRequest request = UnityWebRequest.Post(apiUrl + "/resident-identity/create", form);

        //request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        request.SetRequestHeader("x-api-key", "02e2310edc5eaf62dfe406abaf1a65af");
        request.SetRequestHeader("access-token", tokenPlayer);

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("sesuatu terjadi");
            Debug.LogError("Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);

            modelUser respond = JsonUtility.FromJson<modelUser>(request.downloadHandler.text);

            if (respond.message == "Full name is required!")
            {
                ktpDialogController.KTPDialog[14] = "Nama lengkap anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Family card number is required!")
            {
                ktpDialogController.KTPDialog[14] = "No Kartu Keluarga anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Family card number length is 16 characters!")
            {
                ktpDialogController.KTPDialog[14] = "Digit No Kartu Keluarga harus 16 digit, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Birth place is required!")
            {
                ktpDialogController.KTPDialog[14] = "Tempat Lahir anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Birthday is required!")
            {
                ktpDialogController.KTPDialog[14] = "Tanggal Lahir anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Gender is required!")
            {
                ktpDialogController.KTPDialog[14] = "Jenis Kelamin anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Blood type is required!")
            {
                ktpDialogController.KTPDialog[14] = "Golongan Darah anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Address Street is required!")
            {
                ktpDialogController.KTPDialog[14] = "Alamat anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Address rt is required!")
            {
                ktpDialogController.KTPDialog[14] = "No RT anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Address rw is required!")
            {
                ktpDialogController.KTPDialog[14] = "No RW anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Address province is required!" || respond.message == "Address city is required!" || respond.message == "Address sub district is required!" || respond.message == "Address village is required!")
            {
                ktpDialogController.KTPDialog[14] = "Wilayah Alamat anda belum di isi penuh, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Religion is required!")
            {
                ktpDialogController.KTPDialog[14] = "Agama anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Marital status is required!")
            {
                ktpDialogController.KTPDialog[14] = "Status Perkawinan anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Last education is required!")
            {
                ktpDialogController.KTPDialog[14] = "Pendidikan Terakhir anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Job is required!")
            {
                ktpDialogController.KTPDialog[14] = "Pekerjaan anda belum di isi, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "profile_image file is required!")
            {
                ktpDialogController.KTPDialog[14] = "File Pas Foto anda belum di isi / error, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "family_card_image file is required!")
            {
                ktpDialogController.KTPDialog[14] = "File Foto KK anda belum di isi / error, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "signature_image file is required!")
            {
                ktpDialogController.KTPDialog[14] = "Gambar Tanda Tangan sedang error, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "fingerprints_image documents is required!")
            {
                ktpDialogController.KTPDialog[14] = "File Foto Sidik Jari anda belum di isi / error, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "some fingerprints files is missing, 10 files is required to be processed!")
            {
                ktpDialogController.KTPDialog[14] = "File Foto Sidik Jari anda Kurang, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "iris_image document is required!")
            {
                ktpDialogController.KTPDialog[14] = "File Foto Iris Mata anda belum di isi / error, silahkan isi kembali";
                ktpDialogController.IntoDialog();
            }
            else if (respond.message == "Identity with number 123456 already exists.")
            {
                ktpDialogController.KTPDialog[14] = "NIK Dengan nomor " + NoNIK + " sudah pernah di daftarkan, silahkan tunggu atau gunakan NIK Lain";
                ktpDialogController.IntoDialog();
            }
        }
        else
        {
            ktpDialogController.ktpTrigger.IsNotInputing();
            ktpDialogController.isInputing = false;

            ktpDialogController.index += 2;
            ktpDialogController.IntoDialog();
        }


    }
    private void SpawnPlayer()
    {
        StopAllCoroutines();
        if (avatarPemain == "1")
        {
            Instantiate(playerPrefabs[0], playerSpawnPoint.transform.position, Quaternion.identity);
        }
        else if (avatarPemain == "2")
        {
            Instantiate(playerPrefabs[1], playerSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
