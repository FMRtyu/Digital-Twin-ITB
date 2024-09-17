using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onPress : MonoBehaviour
{
    public GameObject select;
    public GameObject npcgui;
    private DialogueSystem dialogueSystem;

    void Start()
    {
        /* dialogueSystem = FindObjectOfType<DialogueSystem>();*/

    }
    private void OnTriggerEnter(Collider other)
    {
        npcgui.SetActive(true);
        Debug.Log("ada");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("asas");
            // this.gameObject.GetComponent<onPress>().enabled = true;
            // dialogueSystem.dialogueGUI.SetActive(false);
            npcgui.SetActive(false);
            select.SetActive(true);
        }
    }

  /*  public void OnTriggerStay(Collider other)
    {
        npcgui.SetActive(true);
        // this.gameObject.GetComponent<onPress>().enabled = true;
        //dialogueSystem.dialogueGUI.SetActive(true);
        //EnterRangeOfNPC();
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.I))
        {
            // this.gameObject.GetComponent<onPress>().enabled = true;
            // dialogueSystem.dialogueGUI.SetActive(false);
            npcgui.SetActive(false);
            select.SetActive(true);
        }
    }*/

    public void OnTriggerExit()
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<onPress>().enabled = false;
        select.SetActive(false);
    }

    public void EnterRangeOfNPC()
    {
        dialogueSystem.outOfRange = false;
        dialogueSystem.dialogueGUI.SetActive(true);
        if (select == true)
        {
            dialogueSystem.dialogueGUI.SetActive(false);
        }
    }
    /* public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<onPress>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.I))
        {
            this.gameObject.GetComponent<onPress>().enabled = true;
            select.SetActive(true);
            npcgui.SetActive(false);
            
        }
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<onPress>().enabled = false;
        select.SetActive(false);
    }*/

}
