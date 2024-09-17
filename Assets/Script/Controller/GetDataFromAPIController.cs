using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetDataFromAPIController : MonoBehaviour
{
    //pagination
    public int totalPage;

    //prefabs
    public GameObject tablePrefab1;
    public GameObject tablePrefab2;
    public GameObject contentDataTable;

    public GameObject[] prefabsGroup;

    public DataTablePenduduk respond;

    //dataScript
    public CariByNikController data1;
    public DeleteDataController deleteData;
    public FooterController footerController;

    //gameobject halaman
    public GameObject DetailPage;
    public GameObject MenuPage;
    public GameObject HapusPage;
    public GameObject EditPage;

    public string dateFormat;
    private void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        Application.ExternalCall("toggleFullscreen");
#endif
        //Debug.Log(PlayerPrefs.GetString("token"));
        StartCoroutine(GetData());
    }
    public void RefreshDataFromAPI()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        Application.ExternalCall("toggleFullscreen");
#endif
        for (int i = 0; i <= prefabsGroup.Length - 1; i++)
        {
            GameObject objectToDestroy = prefabsGroup[i];

            Destroy(objectToDestroy);
        }
        StartCoroutine(GetData());
    }
    IEnumerator GetData()
    {
        // UnityWebRequest request = new UnityWebRequest(apiUrl + "/auth/register", "POST");
        UnityWebRequest request = UnityWebRequest.Get("https://blockchain-itb.com/residents?page=1&limit=999999");

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
            respond = JsonUtility.FromJson<DataTablePenduduk>(request.downloadHandler.text);
            totalPage = respond.pagination.total;
            footerController.totalData = respond.pagination.total;
            footerController.SetDataFooter();

            prefabsGroup = new GameObject[totalPage];
            SetDataTable();
        }
    }
    public void formatDate(string unfilterDate)
    {
        string day; 
        string month;
        string year;

        day = unfilterDate[8].ToString() + unfilterDate[9].ToString();
        month = unfilterDate[5].ToString() + unfilterDate[6].ToString();
        year = unfilterDate[0].ToString() + unfilterDate[1].ToString() + unfilterDate[2].ToString() + unfilterDate[3].ToString();

        dateFormat = day + "/" + month + "/" + year;
    }
    public void SetDataTable()
    {
        int index = 0;
        int indexTable = 0;

        while (index != totalPage)
        {
            if (indexTable == 0)
            {
                GameObject InsinstantiatedPrefabs = Instantiate(tablePrefab2);
                InsinstantiatedPrefabs.transform.SetParent(contentDataTable.transform);
                prefabsGroup[index] = InsinstantiatedPrefabs;
                prefabsGroup[index].GetComponent<SetTableDataController>().DetailPage = DetailPage;
                prefabsGroup[index].GetComponent<SetTableDataController>().MenuPage = MenuPage;
                prefabsGroup[index].GetComponent<SetTableDataController>().HapusPage = HapusPage;
                prefabsGroup[index].GetComponent<SetTableDataController>().deleteData = deleteData;
                prefabsGroup[index].GetComponent<SetTableDataController>().EditPage = EditPage;
                index++;
                indexTable++;
            }
            else if (indexTable == 1)
            {
                GameObject InsinstantiatedPrefabs = Instantiate(tablePrefab1);
                InsinstantiatedPrefabs.transform.SetParent(contentDataTable.transform);
                prefabsGroup[index] = InsinstantiatedPrefabs;
                prefabsGroup[index].GetComponent<SetTableDataController>().DetailPage = DetailPage;
                prefabsGroup[index].GetComponent<SetTableDataController>().MenuPage = MenuPage;
                prefabsGroup[index].GetComponent<SetTableDataController>().HapusPage = HapusPage;
                prefabsGroup[index].GetComponent<SetTableDataController>().deleteData = deleteData;
                prefabsGroup[index].GetComponent<SetTableDataController>().EditPage = EditPage;
                index++;
                indexTable--;
            }
        }
        int tempNo = 1;
        int userDataIndex = 0;
        int prefabsLength = prefabsGroup.Length - 1;
        for (int i = 0; i < totalPage; i++)
        {
            SetTableDataController dataDalamTable = prefabsGroup[prefabsLength].GetComponent<SetTableDataController>();

            //Kumpulan data
            string noNIK = respond.data[userDataIndex].nik;
            string namaPanjang = respond.data[userDataIndex].full_name;
            formatDate(respond.data[userDataIndex].created_at);

            //setdata
            dataDalamTable.noText.text = tempNo.ToString();
            dataDalamTable.nik = noNIK;
            dataDalamTable.NikText.text = noNIK;
            dataDalamTable.namaText.text = namaPanjang;
            dataDalamTable.tglText.text = dateFormat;

            if (respond.data[userDataIndex].verification_status == "1")
            {
                dataDalamTable.pendingIMG.SetActive(true);
            }
            else if (respond.data[userDataIndex].verification_status == "2")
            {
                dataDalamTable.verifikasiIMG.SetActive(true);
            }
            else if (respond.data[userDataIndex].verification_status == "0")
            {
                dataDalamTable.ditolakIMG.SetActive(true);

            }
            userDataIndex++;
            prefabsLength--;
            tempNo++;
        }
        for (int i = totalPage - 11; i >= 0; i--)
        {
            prefabsGroup[i].SetActive(false);
        }
    }
}
