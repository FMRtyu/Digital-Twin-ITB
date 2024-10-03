using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SampahController : MonoBehaviour
{
    public string TitleName;
    public TextMeshProUGUI Title;

    [Header("Trash Gameobject")]
    public Window_Graph graphDot;
    public Slider[] trashVolume;
    public TextMeshProUGUI[] trashvolumeText;

    public bool isThisSelected;

    //value
    private List<int> trashValueList;
    // Start is called before the first frame update
    private void Start()
    {
        trashValueList = new List<int>() { Random.Range(0, 12), Random.Range(0, 24), Random.Range(0, 36), Random.Range(0, 48), Random.Range(0, 60), Random.Range(0, 72), Random.Range(0, 84), Random.Range(0, 100) };
        StartCoroutine(DummyData());
    }
    public void SetData()
    {
        for (int i = 0; i < trashVolume.Length; i++)
        {
            trashVolume[i].value = trashValueList[7];
            trashvolumeText[i].text = (int)trashValueList[7] + "%";
        }
        graphDot.ShowGraph(trashValueList);
    }
    IEnumerator DummyData()
    {
        while (true)
        {
            trashValueList.RemoveAt(0);
            if (trashValueList[6] >= 95)
            {
                trashValueList.Add(0);
            }
            else
            {
                if (trashValueList[6] < 50)
                {
                    trashValueList.Add(Random.Range(trashValueList[6], 60));
                }
                else
                {
                    trashValueList.Add(Random.Range(trashValueList[6], 100));
                }
            }
            trashvolumeText[1].text = trashValueList[7] + "%";
            trashVolume[1].value = trashValueList[7];
            if (isThisSelected)
            {
                for (int i = 0; i < trashVolume.Length; i++)
                {
                    trashvolumeText[i].text = trashValueList[7] + "%";
                    trashVolume[i].value = trashValueList[7];
                }
                graphDot.ShowGraph(trashValueList);
            }
            yield return new WaitForSeconds(60);
        }
    }
    public void Deselected()
    {
        isThisSelected = false;
    }
}
