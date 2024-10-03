using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchBarController : MonoBehaviour
{
    private TMP_InputField inputField;
    private string searchNik;

    //other
    public FooterController dataFooter;
    public GetDataFromAPIController tableData;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    public void resetNIKSearchBar()
    {
        inputField.text = string.Empty;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (searchNik.Length > 0 && tableData.prefabsGroup.Length > 0)
            {
                for (int i = 0; i <= tableData.prefabsGroup.Length - 1; i++)
                {
                    if (tableData.prefabsGroup[i].GetComponent<SetTableDataController>().nik != searchNik)
                    {
                        Debug.Log(searchNik + " : " + tableData.prefabsGroup[i].GetComponent<SetTableDataController>().nik);
                        tableData.prefabsGroup[i].SetActive(false);
                    }
                }
            }
        }
    }
    private void OnInputFieldValueChanged(string newValue)
    {
        if (tableData.prefabsGroup.Length > 0)
        {
            searchNik = newValue;
        }

        if (newValue.Length == 0)
        {
            int temp = (dataFooter.datatableIndex * 10) - 1;
            for (int i = temp - 9; i < temp && i != tableData.respond.pagination.total; i++)
            {
                tableData.prefabsGroup[i].SetActive(true);
            }
        }
    }
}
