using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveScenetrigger : MonoBehaviour
{
    public string namescene;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(namescene);
    }
}
