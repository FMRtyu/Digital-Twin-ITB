using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraControl;
using TMPro;

public class CCTVController : MonoBehaviour
{
    public GameObject[] virtualCamera;
    public GameObject[] PlayerUI;

    public CameraMotion RTSCamera;

    public string SPName;
    public TextMeshProUGUI SPText;
    public VehicleCountController vehicleCountController;
    public PieChartController pieChartController;

    public WasThisSelectedController[] wasThisSelectedController;
    public SampahController sampahController;

    //value
    private bool isOnce;
    private bool isTwice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void deselected()
    {
        isOnce = false;
        foreach (WasThisSelectedController item in wasThisSelectedController)
        {
            item.CheckIfSelected();
        }
        RTSCamera.isCCTVSelected = false;

        virtualCamera[0].SetActive(true);
        virtualCamera[1].SetActive(false);

        PlayerUI[0].SetActive(true);
        PlayerUI[1].SetActive(false);

        if (vehicleCountController != null)
        {
            vehicleCountController.barChartController.gameObject.SetActive(false);
        }
        if (sampahController != null)
        {
            sampahController.trashVolume[1].gameObject.SetActive(false);
            sampahController.trashvolumeText[1].gameObject.SetActive(false);
        }
    }
    public void ThisSelected()
    {

        SPText.text = SPName;
        if (!isOnce && !isTwice)
        {
            PlayerUI[0].SetActive(false);
            PlayerUI[1].SetActive(true);
            if (vehicleCountController != null)
            {
                vehicleCountController.barChartController.gameObject.SetActive(true);
                vehicleCountController.barChartController.makeBarChart();
                vehicleCountController.barChartController.ShowChart();
                pieChartController.vehicleCountController = vehicleCountController;
                vehicleCountController.isThisSelected = true;
                vehicleCountController.SetVideo();
                vehicleCountController.SetDataInMenu();
            }
            if (sampahController != null)
            {
                sampahController.trashVolume[1].gameObject.SetActive(true);
                sampahController.trashvolumeText[1].gameObject.SetActive(true);

                sampahController.isThisSelected = true;
                sampahController.SetData();
            }
            isOnce = true;
        }
        else if (isOnce && !isTwice)
        {

            RTSCamera.isCCTVSelected = true;
            virtualCamera[0].SetActive(false);
            virtualCamera[1].SetActive(true);
            foreach (WasThisSelectedController item in wasThisSelectedController)
            {
                item.HideGameobject();
            }
            isOnce = false;
        }
    }
}
