using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LayerController : MonoBehaviour
{
    [Header("LayerOption")]
    public Button[] layerButton;
    public GameObject[] layerBorder;
    public GameObject[] layerGameobject;
    [Header("BarChart")]
    public GameObject[] barChartGameobject;
    [Header("3DGameobject")]
    public GameObject stateGroup;
    public Image threeDObject;
    public Image metaverseObject;
    public WasThisSelectedController[] iconController;
    [Header("DigitalTwin")]
    public GameObject iconGroup;
    public GameObject[] cctvBTN;
    public GameObject[] sampahBTN;
    [Header("Augment")]
    public GameObject[] doughnutChart;

    //value
    private bool is3d;
    private bool isMetaverse;
    private bool isDigitalTwin;
    private bool isAugmented;
    // Start is called before the first frame update
    private void Start()
    {
        layerButton[0].onClick.AddListener(BasicLayer);
        layerButton[1].onClick.AddListener(TigaDLayer);
        layerButton[2].onClick.AddListener(DigitalTwinLayer);
        layerButton[3].onClick.AddListener(AugmentedLayer);

        threeDObject.color = Color.gray;
        metaverseObject.color = Color.gray;
        threeDObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        metaverseObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        stateGroup.SetActive(false);
    }

    private void BasicLayer()
    {
        layerBorder[0].SetActive(true);
        layerBorder[1].SetActive(false);
        layerBorder[2].SetActive(false);
        layerBorder[3].SetActive(false);

        threeDObject.color = Color.gray;
        metaverseObject.color = Color.gray;
        threeDObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        metaverseObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        stateGroup.SetActive(false);
        isDigitalTwin = false;
        is3d = false;
        for (int i = 1; i < layerGameobject.Length; i++)
        {
            layerGameobject[i].SetActive(false);
        }
        for (int i = 0; i < barChartGameobject.Length; i++)
        {
            barChartGameobject[i].GetComponent<BarChartController>().CloseChart();
        }
        foreach (WasThisSelectedController item in iconController)
        {
            item.wasThisSelected = false;
            item.ShowHideIcon();
        }
        for (int i = 0; i < doughnutChart.Length; i++)
        {
            doughnutChart[i].SetActive(false);
        }
        cctvBTN[0].SetActive(true);
        cctvBTN[1].SetActive(false);

        sampahBTN[0].SetActive(true);
        sampahBTN[1].SetActive(false);

        iconGroup.SetActive(false);
    }
    private void TigaDLayer()
    {
        if (isAugmented)
        {
            layerBorder[3].SetActive(false);
            isAugmented = false;
        }
        layerBorder[0].SetActive(false);
        layerBorder[1].SetActive(true);
        stateGroup.SetActive(true);
        if (!is3d)
        {
            threeDObject.color = Color.white;
            threeDObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            metaverseObject.color = Color.gray;
            metaverseObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            is3d = true;
            layerGameobject[1].SetActive(true);
            layerGameobject[2].SetActive(true);
            layerGameobject[3].SetActive(false);
        }
        else
        {
            threeDObject.color = Color.gray;
            threeDObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            metaverseObject.color = Color.white;
            metaverseObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            is3d = false;
            layerGameobject[1].SetActive(true);
            layerGameobject[2].SetActive(false);
            layerGameobject[3].SetActive(true);
        }
    }
    private void DigitalTwinLayer()
    {
        if (isAugmented)
        {
            layerBorder[3].SetActive(false);
            isAugmented = false;
        }
        for (int i = 0; i < doughnutChart.Length; i++)
        {
            doughnutChart[i].SetActive(false);
        }
        layerBorder[0].SetActive(false);
        if (!isDigitalTwin)
        {
            iconGroup.SetActive(true);
            isDigitalTwin = true;
            layerBorder[2].SetActive(true);
        }
        else
        {
            for (int i = 0; i < barChartGameobject.Length; i++)
            {
                barChartGameobject[i].GetComponent<BarChartController>().CloseChart();
            }
            iconGroup.SetActive(false);
            isDigitalTwin = false;
            layerBorder[2].SetActive(false);
            foreach (WasThisSelectedController item in iconController)
            {
                item.wasThisSelected = false;
                item.ShowHideIcon();
            }
            cctvBTN[0].SetActive(true);
            cctvBTN[1].SetActive(false);

            sampahBTN[0].SetActive(true);
            sampahBTN[1].SetActive(false);
        }
    }
    private void AugmentedLayer()
    {
        layerBorder[0].SetActive(false);
        if (!isAugmented)
        {
            for (int i = 1; i < layerBorder.Length; i++)
            {
                layerBorder[i].SetActive(true);
            }
            if (!isDigitalTwin)
            {
                DigitalTwinLayer();
            }
            if (!is3d && (!layerGameobject[2].activeSelf || !layerGameobject[3].activeSelf))
            {
                is3d = true;
                TigaDLayer();
            }
            foreach (WasThisSelectedController item in iconController)
            {
                item.wasThisSelected = true;
                item.ShowHideIcon();
            }
            for (int i = 0; i < barChartGameobject.Length; i++)
            {
                barChartGameobject[i].SetActive(true);
                barChartGameobject[i].GetComponent<BarChartController>().ShowChart();
            }
            for (int i = 0; i < doughnutChart.Length; i++)
            {
                doughnutChart[i].SetActive(true);
            }
            cctvBTN[0].SetActive(false);
            cctvBTN[1].SetActive(true);

            sampahBTN[0].SetActive(false);
            sampahBTN[1].SetActive(true);

            isAugmented = true;
        }
        else
        {
            isAugmented = false;
            BasicLayer();
        }
    }
}
