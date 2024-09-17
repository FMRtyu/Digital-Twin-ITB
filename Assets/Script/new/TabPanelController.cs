using UnityEngine;
using UnityEngine.UI;

public class TabPanelController : MonoBehaviour
{
    public GameObject[] tabPanels; // array untuk menyimpan tab panel
    public Button[] tabButtons; // array untuk menyimpan tombol tab
    public Button nextButton; // tombol untuk pindah ke panel berikutnya
    public Button prevButton; // tombol untuk pindah ke panel sebelumnya
    public Button closeButton; // tombol untuk menutup panel
    public Button saveButton; // tombol untuk menyimpan perubahan pada panel

    private int currentPanelIndex = 0; // index panel yang sedang aktif

    private void Start()
    {
        // menambahkan listener pada tombol Next, Previous, Close, dan Save
        nextButton.onClick.AddListener(NextPanel);
        prevButton.onClick.AddListener(PrevPanel);
        closeButton.onClick.AddListener(ClosePanel);
       // saveButton.onClick.AddListener(SavePanel);

        // menampilkan panel pertama dan menyembunyikan panel lainnya
        ShowPanel(currentPanelIndex);
    }

    public void ShowPanel(int panelIndex)
    {
        // menampilkan panel yang dipilih dan menyembunyikan panel lainnya
        for (int i = 0; i < tabPanels.Length; i++)
        {
            if (i == panelIndex)
            {
                tabPanels[i].SetActive(true);
            }
            else
            {
                tabPanels[i].SetActive(false);
            }
        }

        // menandai tombol tab yang aktif dan tidak aktif
        for (int i = 0; i < tabButtons.Length; i++)
        {
            if (i == panelIndex)
            {
                tabButtons[i].interactable = false;
                tabButtons[i].GetComponent<Image>().color = Color.gray; // ubah warna tombol saat aktif
            }
            else
            {
                tabButtons[i].interactable = true;
                tabButtons[i].GetComponent<Image>().color = Color.white; // ubah warna tombol saat tidak aktif
            }
        }

        // mengatur tombol Next, Previous, Close, dan Save agar hanya muncul pada panel tertentu
        if (panelIndex == 0)
        {
            prevButton.gameObject.SetActive(false);
        }
        else if (panelIndex == tabPanels.Length - 1)
        {
            nextButton.gameObject.SetActive(false);
            saveButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(true);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            saveButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
        }

        currentPanelIndex = panelIndex;
    }

    public void NextPanel()
    {
        if (currentPanelIndex < tabPanels.Length - 1)
        {
            ShowPanel(currentPanelIndex + 1);
        }
    }

    public void PrevPanel()
    {
        if (currentPanelIndex > 0)
        {
            ShowPanel(currentPanelIndex - 1);
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void SavePanel()
    {
        // menyimpan perubahan pada panel
        Debug.Log("Panel saved!");
    }
}
