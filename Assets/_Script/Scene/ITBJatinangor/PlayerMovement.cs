using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace DigitalTwinITB.jatinangor
{
    public class PlayerMovement : ThirdPersonController
    {
        private JatinangorController jatinangorController;

        [Header("Camera Property")]
        [SerializeField] private GameObject _thirdPersonCamera;
        [SerializeField] private GameObject _firstPersonCamera;

        private void Awake()
        {
            init();
        }

        private void Update()
        {
            movement();
        }

        protected override void init()
        {
            base.init();

            jatinangorController = FindAnyObjectByType<JatinangorController>();
            jatinangorController.cameraState += OnCameraStateChanged;

            OnCameraStateChanged(CameraState.ThirdPerson);
        }
        protected override void movement()
        {
            if (jatinangorController.currentCameraState == CameraState.ThirdPerson)
            {
                base.movement();
            }
        }

        private void OnCameraStateChanged(CameraState newState)
        {
            switch (newState)
            {
                case CameraState.ThirdPerson:
                    _firstPersonCamera.gameObject.SetActive(false);
                    _thirdPersonCamera.gameObject.SetActive(true); 
                    break;
                    case CameraState.FirstPerson:
                    _firstPersonCamera.gameObject.SetActive(true);
                    _thirdPersonCamera.gameObject.SetActive(false);
                    break;
                    default : break;
            }
        }
    }
}
