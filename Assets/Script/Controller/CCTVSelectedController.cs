using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CCTVSelectedController : MonoBehaviour
{
    public RaycastHit hit;
    public LayerMask mask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.gameObject.activeSelf)
                {
                    hit.transform.gameObject.GetComponent<CCTVController>().ThisSelected();
                }
            }
        }
    }
}
