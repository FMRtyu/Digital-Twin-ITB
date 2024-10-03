using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTableDataController : MonoBehaviour
{
    //data this gameobject
    public TextMeshProUGUI noText;
    public TextMeshProUGUI namaText;
    public TextMeshProUGUI tglText;
    public TextMeshProUGUI NikText;
    public GameObject pendingIMG;
    public GameObject verifikasiIMG;
    public GameObject ditolakIMG;
    public string nik;

    //gameobject halaman
    public GameObject DetailPage;
    public GameObject EditPage;
    public GameObject MenuPage;
    public GameObject HapusPage;

    //other
    public DeleteDataController deleteData;
    public EditDataController editData;
    public void DataDetail()
    {
        CariByNikController detailPaging= DetailPage.GetComponentInChildren<CariByNikController>();

        detailPaging.ResetAllText();
        DetailPage.SetActive(true);
        detailPaging.SearchByNIKWithDocumentFromString(nik);
        MenuPage.SetActive(false);
    }
    public void DataEdit()
    {
        EditDataController EditPaging = EditPage.GetComponent<EditDataController>();

        EditPage.SetActive(true);
        EditPaging.GetDataFromAPI(nik);
        MenuPage.SetActive(false);
    }
    public void hapusData()
    {
        deleteData.NoNIK = NikText.text;
        deleteData.nikText.text = "Yakin ingin menghapus data dengan NIK " + NikText.text + " ?";
        HapusPage.SetActive(true);
    }
}
