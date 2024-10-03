using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconColorChangerController : MonoBehaviour
{
    public Color blueColor;
    public Color greyColor;
    public Color blueColorText;
    public Color greyColorText;
    public Color BGClear;
    public Color BGGrey;
    public Image spriteRenderer;
    public Image BGTabs;
    public TextMeshProUGUI textRenderer;
    public GameObject breakLine;
    public bool isSelected;

    public bool isMain;
    void Start()
    {
        ChangeColorMenu();
    }
    public void ChangeColorMenu()
    {
        isSelected = !isSelected;
        if (!isSelected)
        {
            this.GetComponent<Button>().interactable = true;
            spriteRenderer.color = greyColor;
            textRenderer.color = greyColorText;
            BGTabs.color = BGGrey;
            breakLine.SetActive(false);
        }
        else if (isSelected)
        {
            this.GetComponent<Button>().interactable = false;
            breakLine.SetActive(true);
            spriteRenderer.color = blueColor;
            textRenderer.color = blueColorText;
            BGTabs.color = BGClear;
        }
    }

    public void SetDefault()
    {
        if (!isMain)
        {
            isSelected = false;
            this.GetComponent<Button>().interactable = true;
            spriteRenderer.color = greyColor;
            textRenderer.color = greyColorText;
            BGTabs.color = BGGrey;
            breakLine.SetActive(false);
        }
        else if (isMain)
        {
            isSelected = true;
            this.GetComponent<Button>().interactable = false;
            breakLine.SetActive(true);
            spriteRenderer.color = blueColor;
            textRenderer.color = blueColorText;
            BGTabs.color = BGClear;
        }
    }
}
