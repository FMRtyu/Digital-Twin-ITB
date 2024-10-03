using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KembaliController : MonoBehaviour
{
    public ScrollRect myScrollRect;
    public Scrollbar newScrollBar;
    public GameObject SearchTab;

    public void KembaliSearchTab()
    {
        //Change the current vertical scroll position.
        SearchTab.SetActive(true);
        myScrollRect.verticalNormalizedPosition = 1f;
    }
}
