using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChartController : MonoBehaviour
{
    public Image[] imagePieChart;
    public float[] values;

    public VehicleCountController vehicleCountController;
    // Start is called before the first frame update
    void Start()
    {
        SetValues(values);
    }

    // Update is called once per frame
    void Update()
    {
        values = vehicleCountController.valuesOfVehicle;
        if (gameObject.activeSelf)
        {
            SetValues(values);
        }
    }

    public void SetValues(float[] valueToSet)
    {
        float totalValues = 0;

        for (int i = 0; i < valueToSet.Length; i++)
        {
            totalValues += FindPercentage(valueToSet, i);
            imagePieChart[i].fillAmount = totalValues;
        }
    }

    public float FindPercentage(float[] valueToSet, int index)
    {
        float TotalAmount = 0;

        for (int i = 0; i < valueToSet.Length; i++)
        {
            TotalAmount += valueToSet[i];
        }
        return valueToSet[index] / TotalAmount;
    }
}
