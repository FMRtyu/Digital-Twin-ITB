using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using FreeDraw;

public class KTPDialogPerbaruiController : MonoBehaviour
{
    public string tokenPlayer;
    [Header("ceklist yang mau di ganti")]
    [SerializeField] public bool isAlamat;
    [SerializeField] public bool isAgama;
    [SerializeField] public bool isPekerjaan;
    [SerializeField] public bool isStatusPerkawinan;
    [SerializeField] public bool isPendidikanTerakhir;

    [Header("NPC animation")]
    public Animator animasiNPC;

    [Header("index & kondisi")]
    public int index;
    public int indexPending;
    public int indexInDialog;

    public bool isDialog;
    public bool isInputing;
    public bool isVerified;
    public bool isPending;
    public bool isDitolak;
    public bool isStarted;
    private int inputFieldIndex;

    private bool isLoading;
    [Header("gameobject")]
    public GamePerbaruiManager GMPerbarui;

    public GameObject klikText;
    public GameObject CameraMainInGame;

    public GameObject perkawinanInput;
    public GameObject agamaInput;
    public GameObject pendidikanInput;
    public GameObject pekerjaanInput;

    public GameObject kartuKeluargaInput;
    public GameObject aktaNikahCeraiInput;
    public GameObject keteranganDomisili;
    public GameObject ijazahTerbaruInput;
    public GameObject suratKeteranganKerjaInput;
    public GameObject suratKeteranganPindahAgamaInput;

    public GameObject[] inputField;
    public GameObject klikDimanaSajaUI;
    public TextMeshProUGUI textDialog;
    public KTPTriggerController ktpTrigger;
    public KTPDialogController ktpDialogController;
    public KTPVerifikasi dataKTP;
    public Toggle[] ceklistPilihan;
    public DrawingSettings drawSetting;

    [Header("text")]
    public string[] DialogPerbarui = new string[] {
    "Anda telah memiliki identitas digital yang terverifikasi. Tekan <b>ENTER</b> jika Anda ingin melakukan perubahan data atau informasi.",
    "Silahkan centang kolom identitas yang ingin Anda perbarui.",
    "break",
    "Silahkan isi alamat tinggal baru Anda sesuai dengan Kartu Keluarga",
    "break",
    "Silahkan isi kolom biodata yang ingin Anda perbarui.",
    "break",
    "Lengkapi dokumen persyaratan perubahan data identitas.",
    "break",
    "Konfirmasi perubahan data identitas Anda dan sertakan tanda tangan.",
    "break",
    "Pembaruan identitas Anda telah berhasil direkam ke dalam sistem <b>blockchain</b>. Petugas akan memverifikasi biodata dan dokumen-dokumen Anda, mohon tunggu",
    "end",
    "File gambar KK belum di upload, silahkan upload kembali",
    "break",
    };
    private string[] KTPDialogPending;
    private string[] KTPDialogDitolak;
    public float textSpeed;

    [Header("Input Field")]
    [Header("Data Diri")]
    public TMP_InputField NamaPanjangInput;

    public byte[] pasFoto;
    public byte[] kkFoto;
    public byte[] domisiliFoto;
    public byte[] SKKFoto;
    public byte[] AktaNikahFoto;
    public byte[] IjasahFoto;
    public byte[] pindahAgamaFoto;
    public byte[] tandaTangan;

    public TextMeshProUGUI profileFileText;
    public TextMeshProUGUI kkFileText;
    public TextMeshProUGUI domisiliFileText;
    public TextMeshProUGUI SKKFileText;
    public TextMeshProUGUI aktaNikahFileText;
    public TextMeshProUGUI ijasahFileText;
    public TextMeshProUGUI pindahAgamaFileText;
    public string agama;
    [Header("alamat")]
    public TMP_InputField alamatInput;
    public TMP_Dropdown provinsiInput;
    public TMP_Dropdown kotaKabInput;
    public TMP_Dropdown kecamatanInput;
    public TMP_Dropdown kelurahanDesaInput;
    public TMP_InputField RTInput;
    public TMP_InputField RWInput;

    private string provinsiBaru;
    private string kotaBaru;
    private string kecamatanBaru;
    private string kelurahanBaru;

    [Header("biodata lengkap")]
    public TMP_Dropdown agamaInputDropdown;
    public TMP_InputField pekerjaanInputField;
    public TMP_Dropdown statusPerkawinanInput;
    public string kewarganegaraanInput;
    public TMP_InputField pendidikanTerakhirInput;

    private int lengthKota;
    private int lengthKecamatan;
    private int lengthKelurahan;
    public void StatusPending(string date)
    {
        indexPending = 0;
        KTPDialogPending = new string[2];
        KTPDialogPending[0] = "Anda telah melakukan pendaftaran identitas digital pada tanggal " + date + ". Saat ini pendaftaran identitas digital Anda masih dalam status <b>menunggu verifikasi</b>.Mohon kesediaan Anda untuk menunggu, petugas akan segera memproses permohonan pendaftaran identitas digital Anda.";
        textDialog.text = string.Empty;
        StartCoroutine(TypeDialogPending());
    }
    IEnumerator TypeDialogPending()
    {
        foreach (char c in KTPDialogPending[indexPending].ToCharArray())
        {
            textDialog.text += c;
            yield return new WaitForSeconds(textSpeed);
            if (textDialog.text == KTPDialogPending[indexPending])
            {
                animasiNPC.SetBool("Talking", false);
                StopAllCoroutines();
            }
            else
            {
                animasiNPC.SetBool("Talking", true);
            }
        }
    }
    public void StatusDitolak()
    {
        indexPending = 0;
        KTPDialogDitolak = new string[2];
        KTPDialogDitolak[0] = "Pendaftaran identitas digital Anda <b>tidak dapat diverifikasi</b> karena ketidaksesuaian informasi dengan dokumen pendukung. Tekan <b>ENTER</b> jika Anda ingin melakukan pendaftaran ulang.";
        textDialog.text = string.Empty;
        StartCoroutine(TypeDialogDitolak());
    }
    IEnumerator TypeDialogDitolak()
    {
        foreach (char c in KTPDialogDitolak[indexPending].ToCharArray())
        {
            textDialog.text += c;
            yield return new WaitForSeconds(textSpeed);
            if (textDialog.text == KTPDialogDitolak[indexPending])
            {
                animasiNPC.SetBool("Talking", false);
                StopAllCoroutines();
            }
            else
            {
                animasiNPC.SetBool("Talking", true);
            }
        }
    }
    void Start()
    {
        tokenPlayer = PlayerPrefs.GetString("token");

        lengthKota = 0;
        lengthKecamatan = 0;
        lengthKelurahan = 0;

        indexInDialog = 0;
        indexPending = 0;

        //centang
        isAlamat = false;
        isAgama = false;
        isPekerjaan = false;
        isStatusPerkawinan = false;
        isPendidikanTerakhir = false;

        isDialog = false;
        isInputing = false;
        isVerified = false;
        isPending = false;
        isStarted = false;
    }
    void Update()
    {
        //kondisi pending
        if (Input.GetMouseButtonDown(0) && isPending)
        {
            StopAllCoroutines();
            textDialog.text = KTPDialogPending[index];
            animasiNPC.SetBool("Talking", false);
        }
        //kondisi ditolak
        if (Input.GetMouseButtonDown(0) && isDitolak)
        {
            StopAllCoroutines();
            textDialog.text = KTPDialogDitolak[index];
            animasiNPC.SetBool("Talking", false);
        }
        if (Input.GetKeyDown(KeyCode.Return) && isDitolak)
        {
            if (textDialog.text == KTPDialogDitolak[index])
            {
                StopAllCoroutines();
                ktpDialogController.IntoDialog();
            }
        }
        //kondisi awaledit
        if (Input.GetKeyDown(KeyCode.Return) && isVerified && !isDialog)
        {
            isDialog = true;
            index += 1;
            SetInputFieldFromAPI();
            nextLine();
        }
        //kondisi verified
        if (Input.GetMouseButtonDown(0) && isDialog && !isInputing && isVerified && !isLoading)
        {
            if (textDialog.text == DialogPerbarui[index])
            {
                index += 1;
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textDialog.text = DialogPerbarui[index];
                animasiNPC.SetBool("Talking", false);
            }
        }
    }
    public void SetInputFieldFromAPI()
    {
        NamaPanjangInput.text = dataKTP.respond.data.resident.result.full_name;
        alamatInput.text = dataKTP.respond.data.resident.result.address_street;
        RTInput.text = dataKTP.respond.data.resident.result.address_rt;
        RWInput.text = dataKTP.respond.data.resident.result.address_rw;
        pendidikanInput.GetComponentInChildren<TMP_InputField>().text = dataKTP.respond.data.resident.result.last_education;
        pekerjaanInputField.text = dataKTP.respond.data.resident.result.job;


        //status
        for (int i = 0; i < statusPerkawinanInput.options.Count; i++)
        {
            if (statusPerkawinanInput.options[i].text.ToLower() == dataKTP.respond.data.resident.result.marital_status.ToLower())
            {
                statusPerkawinanInput.value = i;
            }
        }
        //agama
        agama = dataKTP.respond.data.resident.result.religion;

        for (int i = 0; i < agamaInputDropdown.options.Count; i++)
        {
            if (agamaInputDropdown.options[i].text == agama)
            {
                agamaInputDropdown.value = i;
            }
        }

        nextLine();
    }
    public void isVerifiedcondition()
    {
        StopAllCoroutines();
        textDialog.text = DialogPerbarui[index];
    }
    public void ResetDialog()
    {
        index = 0;
        indexInDialog = 0;
        indexPending = 0;
        inputFieldIndex = 0;

        isAlamat = false;
        isAgama = false;
        isPekerjaan = false;
        isStatusPerkawinan = false;
        isPendidikanTerakhir = false;

        isDialog = false;
        isInputing = false;
        isPending = false;
        isVerified = false;
        isStarted = false;

        profileFileText.text = "No File Chosen";
        kkFileText.text = "No File Chosen";
        pindahAgamaFileText.text = "No File Chosen";
        SKKFileText.text = "No File Chosen";
        domisiliFileText.text = "No File Chosen";
        aktaNikahFileText.text = "No File Chosen";
        ijasahFileText.text = "No File Chosen";

        pekerjaanInputField.text = string.Empty;
        pendidikanTerakhirInput.text = string.Empty;


        for (int i = 0; i < ceklistPilihan.Length; i++)
        {
            ceklistPilihan[i].isOn = false;
        }
        Application.ExternalEval("ResetAllInput();");
    }
    public void IntoDialog()
    {
        inputField[inputFieldIndex].SetActive(false);
        inputFieldIndex++;
        ktpTrigger.IsNotInputing();
        isInputing = false;
        index++;
        nextLine();
    }
    public void nextLine()
    {
        StopAllCoroutines();
        if (DialogPerbarui[index] == "end")
        {
            return;
        }
        if (index < DialogPerbarui[index].Length - 1 && DialogPerbarui[index] != "break")
        {
            textDialog.text = string.Empty;
            if (index == 3)
            {
                //SetDataWilayah();
                //mix
                if (isAgama)
                {
                    agamaInput.SetActive(true);
                    suratKeteranganPindahAgamaInput.SetActive(true);
                }
                else
                {
                    agamaInput.SetActive(false);
                    suratKeteranganPindahAgamaInput.SetActive(false);
                }
                if (isStatusPerkawinan)
                {
                    aktaNikahCeraiInput.SetActive(true);
                    perkawinanInput.SetActive(true);
                }
                else
                {
                    aktaNikahCeraiInput.SetActive(false);
                    perkawinanInput.SetActive(false);
                }
                if (isPendidikanTerakhir)
                {
                    ijazahTerbaruInput.SetActive(true);
                    pendidikanInput.SetActive(true);
                }
                else
                {
                    ijazahTerbaruInput.SetActive(false);
                    pendidikanInput.SetActive(false);
                }
                if (isPekerjaan)
                {
                    pekerjaanInput.SetActive(true);
                    suratKeteranganKerjaInput.SetActive(true);
                }
                else
                {
                    pekerjaanInput.SetActive(false);
                    suratKeteranganKerjaInput.SetActive(false);
                }
                if (isAlamat)
                {
                    keteranganDomisili.SetActive(true);
                    StartCoroutine(TypeDialog());
                }
                else if (isAgama || isPekerjaan || isStatusPerkawinan)
                {
                    index = 5;
                    inputFieldIndex = 2;
                    nextLine();
                }
                else
                {
                    textDialog.text = DialogPerbarui[0];
                    ResetDialog();
                    isVerified = true;
                }
            }
            else
            {
                StartCoroutine(TypeDialog());
            }
        }
        else if (DialogPerbarui[index] == "break")
        {
            if (index == 10)
            {
#if !UNITY_EDITOR && UNITY_WEBGL
        Application.ExternalCall("toggleFullscreen");
#endif
                CameraMainInGame.SetActive(false);
                ktpTrigger.IsInputingPlayer();
                inputField[inputFieldIndex].SetActive(true);
                isInputing = true;
            }
            else if (index == 14)
            {
                index = 7;
                inputFieldIndex = 3;
                TypeDialog();
            }
            else
            {
                ktpTrigger.IsInputingPlayer();
                inputField[inputFieldIndex].SetActive(true);
                isInputing = true;
            }
        }
    }
    IEnumerator TypeDialog()
    {
        foreach (char c in DialogPerbarui[index].ToCharArray())
        {
            textDialog.text += c;
            yield return new WaitForSeconds(textSpeed);
            if (textDialog.text == DialogPerbarui[index])
            {
                animasiNPC.SetBool("Talking", false);
            }
            else
            {
                animasiNPC.SetBool("Talking", true);
            }
        }
    }
    public void PreviousDialog()
    {
        isInputing = false;
        ktpTrigger.IsNotInputing();
        Debug.Log(index);
        if (index == 10)
        {
            inputField[inputFieldIndex].SetActive(false);
            CameraMainInGame.SetActive(true);
            ktpTrigger.IsInputingPlayer();
            inputFieldIndex--;
            index -= 3;
        }
        else if (index == 6 && !isAlamat)
        {
            inputField[inputFieldIndex].SetActive(false);
            index = 1;
            inputFieldIndex = 0;
        }
        else
        {
            inputField[inputFieldIndex].SetActive(false);
            inputFieldIndex--;
            index -= 3;
        }
        nextLine();
    }
    public void SetProfilePic(string base64Array)
    {
        pasFoto = System.Convert.FromBase64String(base64Array);
        profileFileText.text = "1 File chosen";

    }
    public void SetKKPic(string base64Array)
    {
        kkFoto = System.Convert.FromBase64String(base64Array);
        kkFileText.text = "1 File chosen";

    }
    public void SetDomisiliPic(string base64Array)
    {
        domisiliFoto = System.Convert.FromBase64String(base64Array);
        domisiliFileText.text = "1 File chosen";
    }
    public void SetSKKPic(string base64Array)
    {
        SKKFoto = System.Convert.FromBase64String(base64Array);
        SKKFileText.text = "1 File chosen";

    }
    public void SetAktaNikahCeraiPic(string base64Array)
    {
        AktaNikahFoto = System.Convert.FromBase64String(base64Array);
        aktaNikahFileText.text = "1 File chosen";
    }
    public void SetIjasahPic(string base64Array)
    {
        IjasahFoto = System.Convert.FromBase64String(base64Array);
        ijasahFileText.text = "1 File chosen";
    }
    public void SetPindahAgamaPic(string base64Array)
    {
        pindahAgamaFoto = System.Convert.FromBase64String(base64Array);
        pindahAgamaFileText.text = "1 File chosen";
    }
    public void setAlamat()
    {
        isAlamat = !isAlamat;
    }
    public void setAgama()
    {
        isAgama = !isAgama;
    }
    public void setPekerjaan()
    {
        isPekerjaan = !isPekerjaan;
    }
    public void setStatusPerkawinan()
    {
        isStatusPerkawinan = !isStatusPerkawinan;
    }
    public void setPendidikanTerakhir()
    {
        isPendidikanTerakhir = !isPendidikanTerakhir;
    }
    public void saveDataToAPI()
    {
        StopAllCoroutines();

        inputField[inputFieldIndex].SetActive(false);
        CameraMainInGame.SetActive(true);

        GMPerbarui.kkFoto = kkFoto;
        if (pasFoto != null || pasFoto.Length > 0)
        {
            GMPerbarui.pasFoto = pasFoto;
        }
        if (isAlamat)
        {
            GMPerbarui.provinsi = provinsiInput.options[provinsiInput.value].text;
            GMPerbarui.kotaKab = kotaKabInput.options[kotaKabInput.value].text;
            GMPerbarui.kecamatan = kecamatanInput.options[kecamatanInput.value].text;
            GMPerbarui.kelurahanDesa = kelurahanDesaInput.options[kelurahanDesaInput.value].text;
            GMPerbarui.RT = RTInput.text;
            GMPerbarui.RW = RWInput.text;
            GMPerbarui.alamat = alamatInput.text;
            GMPerbarui.domisiliFoto = domisiliFoto;
        }
        if (isAgama)
        {
            GMPerbarui.agama = agamaInputDropdown.options[agamaInputDropdown.value].text;
            GMPerbarui.pindahAgamaFoto = pindahAgamaFoto;
        }
        if (isStatusPerkawinan)
        {
            GMPerbarui.statusPerkawinan = statusPerkawinanInput.options[statusPerkawinanInput.value].text;
            GMPerbarui.AktaNikahFoto = AktaNikahFoto;
        }
        if (isPendidikanTerakhir)
        {
            GMPerbarui.pendidikanTerakhir = pendidikanTerakhirInput.text;
            GMPerbarui.IjasahFoto = IjasahFoto;
        }
        if (isPekerjaan)
        {
            GMPerbarui.SKKFoto = SKKFoto;
            GMPerbarui.pekerjaan = pekerjaanInputField.text;
        }
        GMPerbarui.UpdateDataToAPI();
    }
}
