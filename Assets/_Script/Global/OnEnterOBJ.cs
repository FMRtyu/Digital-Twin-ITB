using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterOBJ : MonoBehaviour
{
/*    public GameObject UIDIALOGNPC;
    public GameObject Select;*/

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "npc_pendataan")
        {
            Debug.Log("adadad");
        }
    }


}
