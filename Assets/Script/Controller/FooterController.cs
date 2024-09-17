using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FooterController : MonoBehaviour
{
    //text
    public TextMeshProUGUI menampilkanDariText;
    public TextMeshProUGUI IndexHalaman;
    //button
    public Button SebelumnyaBTN;
    public Button SelanjutnyaBTN;
    //data
    public int totalData;
    public int dataPerPage;
    public int totalPages;
    public int datatableIndex;
    public int currentDataShow;
    public GetDataFromAPIController getDataFromAPIController;

    public void SetDataFooter()
    {
        datatableIndex = 1;
        dataPerPage = 10;
        currentDataShow = totalData - 1;
        totalPages = totalData / dataPerPage;

        if (totalData % dataPerPage != 0)
            totalPages++;
        SebelumnyaBTN.interactable = false;
        if (totalPages == 1)
        {
            SelanjutnyaBTN.interactable = false;
        }
        else
        {
            SelanjutnyaBTN.interactable = true;
        }
        IndexHalaman.text = datatableIndex.ToString();
        menampilkanDariText.text = "Menampilkan " + datatableIndex + " dari " + totalPages;
    }

    public void DataSelanjutnya()
    {
        datatableIndex++;
        IndexHalaman.text = datatableIndex.ToString();
        //button checker
        if (datatableIndex == totalPages)
        {
            SelanjutnyaBTN.interactable = false;
        }
        if(datatableIndex != 1)
        {
            SebelumnyaBTN.interactable = true;
        }

        //set menampilkan
        menampilkanDariText.text = "Menampilkan " + datatableIndex + " dari " + totalPages;

        //hide data
        for (int i = currentDataShow; i >= currentDataShow - 9 && i != -1; i--)
        {
            getDataFromAPIController.prefabsGroup[i].SetActive(false);
        }
        currentDataShow -= 10;
        Debug.Log(currentDataShow);
        //aktifkan data
        for (int i = currentDataShow; i >= currentDataShow - 9 && i != -1; i--)
        {
            getDataFromAPIController.prefabsGroup[i].SetActive(true);
        }
    }
    public void DataSebelumnya ()
    {
        datatableIndex--;
        IndexHalaman.text = datatableIndex.ToString();
        //button checker
        if (datatableIndex != totalPages)
        {
            SelanjutnyaBTN.interactable = true;
        }
        if (datatableIndex == 1)
        {
            SebelumnyaBTN.interactable = false;
        }

        //set menampilkan
        menampilkanDariText.text = "Menampilkan " + datatableIndex + " dari " + totalPages;

        //hide data
        for (int i = currentDataShow; i >= currentDataShow - 9 && i != -1; i--)
        {
            getDataFromAPIController.prefabsGroup[i].SetActive(false);
        }
        currentDataShow += 10;
        Debug.Log(currentDataShow);

        //aktifkan data
        for (int i = currentDataShow; i >= currentDataShow - 9 && i != -1; i--)
        {
            getDataFromAPIController.prefabsGroup[i].SetActive(true);
        }
    }
}
