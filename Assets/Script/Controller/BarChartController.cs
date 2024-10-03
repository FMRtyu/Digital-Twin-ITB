using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BarChartController : MonoBehaviour
{
    public GameObject[] cubePrefab;
    public GameObject textMeshProPrefab;
    GameObject[] cube;
    public float barWidth;
    public float barSpacing;
    public float minHeight;
    public float maxHeight;

    public float[] dataValues;

    private float[] cubeHeight;
    private bool isShowed;
    public bool isOnProggress;
    public void setValues(float[] newOne)
    {
        dataValues = newOne;
    }
    public void makeBarChart()
    {
        cube = new GameObject[dataValues.Length];
        cubeHeight = new float[dataValues.Length];
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < dataValues.Length; i++)
        {
            // Calculate the position based on the parent's position, rotation, and cube position
            Vector3 position = transform.position +
                              transform.rotation * new Vector3(i * (barWidth + barSpacing), 0.0f, 0.0f);

            cube[i] = Instantiate(cubePrefab[i], position, Quaternion.identity, transform);

            cubeHeight[i] = Mathf.Max(dataValues[i] * 50 / maxHeight, minHeight / maxHeight); // Ensure bars are at least minHeight
            //cube[i].transform.localScale = new Vector3(barWidth, normalizedHeight, barWidth);

            Vector3 textPosition = position + new Vector3(0.0f, cubeHeight[i] + 10, 0.0f);
            GameObject textMeshProObject = Instantiate(textMeshProPrefab, textPosition, Quaternion.identity, transform);
            TextMeshProUGUI textMeshPro = textMeshProObject.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = dataValues[i].ToString(); // Display data value as text
        }
        if (isShowed)
        {
            for (int i = 0; i < dataValues.Length; i++)
            {
                LeanTween.scale(cube[i], new Vector3(barWidth, cubeHeight[i], barWidth), 1f);
            }
        }
    }
    public void ShowChart()
    {
        isShowed = true;
        isOnProggress = true;
        for (int i = 0; i < dataValues.Length; i++)
        {
            LeanTween.scale(cube[i], new Vector3(barWidth, cubeHeight[i], barWidth), 1f);
        }
        isOnProggress = false;
    }
    public void CloseChart()
    {
        isShowed = false;
        isOnProggress = true;
        for (int i = 0; i < dataValues.Length; i++)
        {
            LeanTween.scale(cube[i], new Vector3(barWidth, 1, barWidth), 1f).setOnComplete(() => {
                for (int j = 0; j < cube.Length; j++)
                {
                    this.gameObject.SetActive(false);
                }
            });
        }
        isOnProggress = false;
    }
}
