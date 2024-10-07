using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using static DigitalTwinITB.jatinangor.JatinangorController;

namespace DigitalTwinITB.jatinangor
{
    public class JatinangorController : MonoBehaviour
    {
        [Header("Pipe Materials")]
        [SerializeField] public Material materialColorPurple;
        [SerializeField] public Material materialColorRed;

        [Header("Situ 1 - GWT")]
        [SerializeField] private SituOneGWT _situGWTController;
        private bool _Situ1;
        private bool _Sensor;

        [Header("Enums")]
        public JatinangorLayerState currentLayerState;
        public CameraState currentCameraState;

        [Header("walk-in property")]
        [SerializeField] private GameObject _playerPrefabs;
        private GameObject _spawnedPlayer;
        [SerializeField] private Transform _pointSitu;
        [SerializeField] private Transform _pointWTP;
        [SerializeField] private Transform _pointGWT;
        [SerializeField] private Transform _pointAsrama;

        [Header("Metaverse Layer")]
        [SerializeField] private GameObject[] _threeDLayer;
        [SerializeField] private GameObject[] _digitalTwinLayer;

        //event group
        //Event Situ 1 - WTP
        public delegate void SituWTPValueChange(int SituPressure, int SensorPressure);

        public event SituWTPValueChange SituWTPValueChanged;

        //Event WTP - GWT
        public delegate void WTPGWTValueChange(int WTPPressure, int SensorPressure);

        public event WTPGWTValueChange WTPGWTValueChanged;

        //Event Metaverse Layer 
        public delegate void DigitalLayer(JatinangorLayerState newState);

        public event DigitalLayer onLayerChange;

        //Event Camera 

        public delegate void ChangeCamera(CameraState newState);

        public event ChangeCamera cameraState;

        // Start is called before the first frame update
        void Awake()
        {
            init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void init()
        {
            _situGWTController.init(this);

            SituWTPValueChanged += ChangeSituWTPValue;
            WTPGWTValueChanged += ChangeWTPGWTValue;
            onLayerChange += ChangeLayer;
            cameraState += ChangePlayerCamera;

            ChangeLayer(JatinangorLayerState.BasicLayers);

            StartCoroutine(SetData());
        }

        #region Event
        private void ChangeSituWTPValue(int SituPressure, int SensorPressure)
        {

        }

        private void ChangeWTPGWTValue(int SituPressure, int SensorPressure)
        {

        }

        private void ChangeLayer(JatinangorLayerState newState)
        {
            currentLayerState = newState;

            SetLayerVisibility(false);
            switch (currentLayerState)
            {
                case JatinangorLayerState.BasicLayers:
                    SetLayerVisibility(false);
                    break;
                case JatinangorLayerState.ThirdDimensionLayers:
                    foreach (GameObject item in _threeDLayer)
                    {
                        item.SetActive(true);
                    }
                    break;
                case JatinangorLayerState.DigitalTwinLayer:
                    foreach (GameObject item in _digitalTwinLayer)
                    {
                        item.SetActive(true);
                    }
                    break;
                case JatinangorLayerState.AugmentedLayer:
                    SetLayerVisibility(true);
                    break;
                default:
                    break;
            }
        }

        public void ChangePlayerCamera(CameraState newState)
        {
            currentCameraState = newState;
        }
        #endregion

        #region Setdata

        public void SetLayer(int newLayerLevel)
        {
            switch (newLayerLevel)
            {
                case 0:
                    onLayerChange(JatinangorLayerState.BasicLayers);
                    break;
                case 1:
                    onLayerChange(JatinangorLayerState.ThirdDimensionLayers);
                    break;
                case 2:
                    onLayerChange(JatinangorLayerState.DigitalTwinLayer);
                    break;
                case 3:
                    onLayerChange(JatinangorLayerState.AugmentedLayer);
                    break;
            }
        }
        public void SpawnPlayer(int SpawnPoint)
        {
            if (_spawnedPlayer != null)
            {
                Destroy(_spawnedPlayer);
            }
            currentCameraState = CameraState.ThirdPerson;
            switch (SpawnPoint)
            {  
                case 0:
                    _spawnedPlayer = Instantiate(_playerPrefabs, new Vector3(_pointSitu.position.x, _pointSitu.position.y, _pointSitu.position.z), Quaternion.identity);
                    break;
                case 1:
                    _spawnedPlayer = Instantiate(_playerPrefabs, new Vector3(_pointWTP.position.x, _pointWTP.position.y, _pointWTP.position.z), Quaternion.identity);
                    break;
                case 2:
                    _spawnedPlayer = Instantiate(_playerPrefabs, new Vector3(_pointGWT.position.x, _pointGWT.position.y, _pointGWT.position.z), Quaternion.identity);
                    break;
                case 3:
                    _spawnedPlayer = Instantiate(_playerPrefabs, new Vector3(_pointAsrama.position.x, _pointAsrama.position.y, _pointAsrama.position.z), Quaternion.identity);
                    break;
                    default:
                    Debug.Log("Delete Player");
                    _spawnedPlayer = null;
                    currentCameraState = CameraState.World;
                    break;
            }
        }

        //test
        private IEnumerator SetData()
        {
            while (true)
            {
                int situ = Random.Range(1, 10);
                int sensor1 = Random.Range(1, 10);

                int WTP = Random.Range(1, 10);
                int sensor2 = Random.Range(1, 10);

                yield return new WaitForSeconds(5f);
                SituWTPValueChanged(situ, sensor1);

                WTPGWTValueChanged(WTP, sensor2);
            }
        }

        private void SetLayerVisibility(bool newCondition)
        {
            foreach (GameObject item in _threeDLayer)
            {
                item.SetActive(newCondition);
            }
            foreach (GameObject item in _digitalTwinLayer)
            {
                item.SetActive(newCondition);
            }
        }
        #endregion
    }
}
