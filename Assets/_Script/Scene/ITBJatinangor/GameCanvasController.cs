using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalTwinITB.jatinangor
{
    public class GameCanvasController : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private Animator _menuAnimator;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #region SetData

        public void SetMenuVisibility(bool newCondition)
        {
            _menuAnimator.SetBool("isMenuOpen", newCondition);
        }

        #endregion
    }
}
