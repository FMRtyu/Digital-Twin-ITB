using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace DigitalTwinITB.jatinangor
{
    public class SituOneGWT : MonoBehaviour
    {
        public Station network { get; private set; }

        [Header("Property")]
        [SerializeField] private SplineContainer _splineSituOneWTP;
        [SerializeField] private SplineContainer _splineWTPGWT;
        public JatinangorController _jatinangorController { private get; set; }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void init(JatinangorController _jatinangorController)
        {

            this._jatinangorController = _jatinangorController;
            network = Station.SituToWTP;

            _jatinangorController.SituWTPValueChanged += OnSituWTPPressureChanged;
            _jatinangorController.WTPGWTValueChanged += OnWTPGWTPressureChanged;
        }

        private void OnDestroy()
        {
            _jatinangorController.SituWTPValueChanged -= OnSituWTPPressureChanged;
            _jatinangorController.WTPGWTValueChanged -= OnWTPGWTPressureChanged;
        }

        private void OnSituWTPPressureChanged(int SituPressure, int SensorPressure)
        {
            if (SituPressure <= 5 && SensorPressure <= 5)
            {
                Debug.Log("changing Red");
                ChangePipeColor(_splineWTPGWT, _jatinangorController.materialColorRed);
            }
            else
            {
                Debug.Log("changing Purple");
                ChangePipeColor(_splineWTPGWT, _jatinangorController.materialColorPurple);
            }
        }

        private void OnWTPGWTPressureChanged(int WTPPressure, int SensorPressure)
        {
            if (WTPPressure <= 5 && SensorPressure <= 5)
            {
                Debug.Log("changing Red");
                ChangePipeColor(_splineSituOneWTP, _jatinangorController.materialColorRed);
            }
            else
            {
                Debug.Log("changing Purple");
                ChangePipeColor(_splineSituOneWTP, _jatinangorController.materialColorPurple);
            }
        }

        private void ChangePipeColor(SplineContainer splineContainer, Material newMaterial)
        {
            Material[] materials = splineContainer.GetComponent<MeshRenderer>().materials;

            materials[0] = newMaterial;

            splineContainer.GetComponent<MeshRenderer>().materials = materials;

        }
    }
}
