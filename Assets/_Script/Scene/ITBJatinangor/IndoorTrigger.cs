using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalTwinITB.jatinangor
{
    public class IndoorTrigger : MonoBehaviour
    {
        private JatinangorController _jatinangorController;
        // Start is called before the first frame update
        void Start()
        {
            init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void init()
        {
            _jatinangorController = FindAnyObjectByType<JatinangorController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _jatinangorController.ChangePlayerCamera(CameraState.FirstPerson);
        }
    }
}
