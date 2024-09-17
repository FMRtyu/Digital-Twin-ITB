using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class Input_Pendataan : MonoBehaviour
{
    string apiUrl = "https://blockchain-itb.com";
    public string tokkens;

    public TMP_InputField inpNamaLengkap;
    public TMP_InputField inpNoKK;
    public TMP_InputField inpNik;
    public TMP_InputField inpProvinsi;
    public TMP_InputField inpAlamat;
    public TMP_InputField inpKota;
    public TMP_InputField inpKecamatan;
    public TMP_InputField inpKelurahan;
    public TMP_InputField inpRT;
    public TMP_InputField inpRW;
    public TMP_InputField inpTempat;
    public TMP_InputField inpTanggalLahir;
    public Toggle inpJenkelLaki, inpJenkelPerempuan;
    public TMP_Dropdown inpGoldar;
    public TMP_Dropdown inpAgama;
    public TMP_InputField inpPekerjaan;
    public TMP_Dropdown inpStatus;
    public Toggle inpKewarganegaraanWNA, inpKewarganegaraanWNI;
    public TMP_InputField inpPendidikanTerakhir;
    public UploadFile inpFoto ;
    public UploadFile inpKKpdf;
    public UploadFile inpIRIS;
    public UploadFile inpTTD;
    public UploadFile inpJARI;

    public TextMeshProUGUI textfoto, textKK, textIRIS, textTTD, textJARI;


    public Button InputBtn;

    string namaLengkap;
    string noKK;
    string nik;
    string provinsi;
    string alamat;
    string kota;
    string kecamatan;
    string kelurahan;
    string RT;
    string RW;
    string tempat;
    string tanggalLahir;
    public string jenkel;
    string goldar;
    string agama;
    string pekerjaan;
    string status;
    public string kewarganegaraan;
    string pendidikanTerakhir;
    byte[] foto;
    byte[] KKpdf;
    byte[] IRIS;
    byte[] TTD;
    byte[] JARI;

    byte[] fileData;

    void Start()
    {
        InputBtn.onClick.AddListener(SubmitFormResidence);
        tokkens = PlayerPrefs.GetString("token");

        // inpJenkelLaki.onValueChanged.AddListener(ToggleSelect("jenkel", "Laki-laki"));
    }
    void SubmitFormResidence()
    {
         namaLengkap = inpNamaLengkap.text;
         noKK = inpNoKK.text;
         nik = inpNik.text;
         provinsi = inpProvinsi.text;
         alamat = inpAlamat.text;
         kota = inpKota.text;
         kecamatan = inpKecamatan.text;
         kelurahan = inpKelurahan.text;
         RT = inpRT.text;
         RW = inpRW.text;
         tempat = inpTempat.text;
         tanggalLahir = inpTanggalLahir.text;
        // jenkel = inpJenkel.ActiveToggles().FirstOrDefault()?.GetComponentInChildren<Text>().text;
        // jenkel = GetSelectedToggleText(inpJenkel);
         goldar = inpGoldar.captionText.text;
         agama = inpAgama.captionText.text;
         pekerjaan = inpPekerjaan.text;
         status = inpStatus.captionText.text;
         //kewarganegaraan = inpKewarganegaraan.ActiveToggles().FirstOrDefault()?.GetComponentInChildren<Text>().text;
         Debug.Log("jenkell : "+jenkel);
         pendidikanTerakhir = inpPendidikanTerakhir.text;
         //foto = inpFoto.GetFileData();
        Debug.Log("foto : " + foto);
       //  KKpdf = inpKKpdf.text;
       //  IRIS = inpIRIS.text;
       //  TTD = inpTTD.text;
       //  JARI = inpJARI.text;

     /*   string data = "{{" +
        "\"nik\":\"" + nik + "\"," +
        "\"family_card_number\":\"" + noKK + "\"," +
        "\"full_name\":\"" + namaLengkap + "\"," +
        "\"address_province\":\"" + provinsi + "\"," +
        "\"address_street\":\"" + alamat+ "\"," +
        "\"address_city\":\"" + kota + "\"," +
        "\"address_sub_district\":\"" + kecamatan + "\"," +
        "\"address_village\":\"" + kelurahan + "\"," +
        "\"address_rt\":\"" + RT + "\"," +
        "\"address_rw\":\"" + RW+ "\"," +
        "\"birth_place\":\"" + tempat + "\"," +
        "\"birthday\":\"" + tanggalLahir + "\"," +
        "\"gender\":\"" + jenkel+ "\"," +
        "\"blood_type\":\"" + goldar + "\"," +
        "\"religion\":\"" + agama + "\"," +
        "\"job\":\"" + pekerjaan + "\"," +
        "\"marital_status\":\"" + status + "\"," +
        "\"nationality\":\"" + kewarganegaraan + "\"," +
        "\"last_education\":\"" + pendidikanTerakhir + "\"," +
        "\"profile_image\":\"" + foto + "\"," +
        "\"family_card_image\":\"" + KKpdf+ "\"," +
        "\"iris_image\":\"" + IRIS + "\"," +
        "\"signature_image\":\"" + TTD+ "\"" +
        "\"fingerprints_image\":\"" + JARI + "\"" +
        "}}";*/
        


        SendPostRequest();
    }
    public void SendPostRequest()
    {
        StartCoroutine(PostRequest());
    }


    IEnumerator PostRequest()
    {


        WWWForm form = new WWWForm();
        form.AddBinaryData("profile_image", foto);
        form.AddBinaryData("family_card_image", KKpdf);
        form.AddBinaryData("iris_image", IRIS);
        form.AddBinaryData("signature_image", TTD);

        form.AddField("family_card_number", noKK);
        form.AddField("nik", nik);
        form.AddField("full_name", "User satu");
        form.AddField("address_province", provinsi);
        form.AddField("address_street", alamat);
        form.AddField("address_city", kota);
        form.AddField("address_sub_district", kecamatan);
        form.AddField("address_village", kelurahan);
        form.AddField("address_rt", RT);
        form.AddField("address_rw", RW);
        form.AddField("birth_place", tempat);
        form.AddField("birthday", tanggalLahir);
        Debug.Log("jenkel" + jenkel);
        form.AddField("gender", jenkel);
        Debug.Log("yap masuk : " + jenkel);
        form.AddField("blood_type", goldar);
        Debug.Log("Goldar : " + goldar);
        form.AddField("religion", agama);
        Debug.Log("Agama : " + agama);
        form.AddField("job", pekerjaan);
        form.AddField("marital_status", status);
        Debug.Log("Status : " + status);
        form.AddField("nationality", kewarganegaraan);
        form.AddField("last_education", pendidikanTerakhir);
        byte[] boundary = UnityWebRequest.GenerateBoundary();

        Debug.Log("name " + namaLengkap);

        Debug.Log("boundary : " + boundary);
        
        UnityWebRequest request = UnityWebRequest.Post(apiUrl + "/resident-identity/create", form);

        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        //request.uploadHandler = (UploadHandler)new UploadHandlerRaw(form);
       // request.uploadHandler = (UploadHandler)new UploadHandlerFile(form);

        //request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("x-api-key", "d249e530-a13e-4f25-a240-53a9ad5b1e65");
        request.SetRequestHeader("token", "d249e530-a13e-4f25-a240-53a9ad5b1e65");
        request.SetRequestHeader("access-token", tokkens);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError){
            // Debug.LogError("Error: " + request.error);
            Debug.Log("cek data : " + form);
            Debug.Log("Response: " + request.downloadHandler.text);
            Debug.Log("Error");
        }
        else
        {
            modelUser respond = JsonUtility.FromJson<modelUser>(request.downloadHandler.text);
            PlayerPrefs.SetString("token", respond.accessToken);
            PlayerPrefs.SetString("avatar", respond.data.avatar);

            Debug.Log("Response: " + request.downloadHandler.text);
            Debug.Log("Berhasil kirim data");
            Debug.Log("data form : "+ form);

        }



        /* // Buat request POST ke API
         UnityWebRequest request = UnityWebRequest.Post(apiUrl + "/submit-form", formData);

         // Tunggu hingga request selesai
         yield return request.SendWebRequest();

         // Jika terjadi error pada request, tampilkan pesan error
         if (request.result != UnityWebRequest.Result.Success)
         {
             Debug.LogError("Error sending form data: " + request.error);
         }
         else
         {
             // Jika berhasil, tampilkan respons dari server
             Debug.Log("Form data submitted successfully. Server response: " + request.downloadHandler.text);
         }*/
    }

    public string GetSelectedToggleText(ToggleGroup toggleGroup)
    {
        string selectedToggleText = "";
        foreach (Toggle toggle in toggleGroup.ActiveToggles())
        {
            selectedToggleText = toggle.GetComponentInChildren<Toggle>().ToString();
        }
        return selectedToggleText;
        Debug.Log("diluar foreach : "+toggleGroup.ActiveToggles());
    }
    public void ToggleSelectGENDER(string value)
    {
        /*f (tg.Equals("jenkel"))
        {*/
            jenkel = value;
        //}

      /*  if (tg.Equals("kewarganegaraan"))
        {
            kewarganegaraan = value;
        }*/


        Debug.Log("data Selected toggle : " + value);
    }    
    public void ToggleSelectKEWARGANEGARAAN(string value)
    {
            kewarganegaraan = value;

        Debug.Log("data Selected toggle : " + value);
    }

    public void SelectImage(string Imgsaved)
    {
        Texture2D texture;
        byte[] fileData;
        string fileName;
        string fileType;
    // Memilih gambar dari galeri
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Memuat gambar ke dalam texture
                byte[] bytes = File.ReadAllBytes(path);
                texture = new Texture2D(2, 2);
                texture.LoadImage(bytes);

                // Mengubah gambar menjadi data PNG
                if (Imgsaved.Equals("foto"))
                {
                    foto = texture.EncodeToJPG();
                    textfoto.text = path;
                }

                if (Imgsaved.Equals("KKpdf"))
                {
                    KKpdf = texture.EncodeToJPG();
                    textKK.text = path;
                }

                if (Imgsaved.Equals("IRIS"))
                {
                    IRIS= texture.EncodeToJPG();
                    textIRIS.text = path;
                }

                if (Imgsaved.Equals("JARI"))
                {

                    JARI= texture.EncodeToJPG();
                    textJARI.text = path;
                }

                if (Imgsaved.Equals("TTD"))
                {
                    TTD= texture.EncodeToJPG();
                    textTTD.text = path;
                }



                fileName = Path.GetFileName(path);
                fileType = "image/jpg";

                //Debug.Log("filedata : " + foto);
                //Debug.Log("filename : " + fileName);
                //Debug.Log("filetype: " + fileType);
            }
        }, "Select an image");

        Debug.Log("Permission result: " + permission);
    }



    public void SelectPDF(string PDFsaved)
    {
        byte[] fileData;
        string fileName;
        string fileType;

        // Memilih file PDF dari galeri
        NativeGallery.Permission permission = NativeGallery.GetFileFromGallery((path) =>
        {
            if (path != null)
            {

                // Membaca file PDF sebagai byte array
                fileData = File.ReadAllBytes((string)path);

                // Mengubah nama file menjadi nama file asli
                fileName = Path.GetFileName((string)path);
                Debug.Log("path : " + path);

                // Menetapkan tipe file sebagai PDF
                fileType = "application/pdf";

                // Menyimpan file PDF ke variabel yang sesuai
                if (PDFsaved.Equals("KKpdf"))
                {
                    KKpdf = fileData;
                }

                // Debug.Log("filedata : " + fileData);
                // Debug.Log("filename : " + fileName);
                // Debug.Log("filetype: " + fileType);
            }
        }, "Select a PDF file", "application/pdf");

        Debug.Log("Permission result: " + permission);
    }


}



