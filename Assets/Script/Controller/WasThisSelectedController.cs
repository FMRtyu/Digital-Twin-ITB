using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasThisSelectedController : MonoBehaviour
{
    public bool wasThisSelected;
    public GameObject[] selectedGameObject;
    
    public void ChangeState()
    {
        wasThisSelected = !wasThisSelected;
    }
    public void ShowHideIcon()
    {
        if (wasThisSelected)
        {
            foreach (GameObject item in selectedGameObject)
            {
                item.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject item in selectedGameObject)
            {
                item.SetActive(false);
            }
        }
    }
    public void CheckIfSelected()
    {
        if (wasThisSelected)
        {
            foreach (GameObject item in selectedGameObject)
            {
                item.SetActive(true);
            }
        }
    }
    public void HideGameobject()
    {
        if (wasThisSelected)
        {
            foreach (GameObject item in selectedGameObject)
            {
                item.SetActive(false);
            }
        }
    }
}
