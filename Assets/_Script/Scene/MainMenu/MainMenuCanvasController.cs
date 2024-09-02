using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalTwinITB.MainMenu
{
    public class MainMenuCanvasController : MonoBehaviour
    {
        [Header("Fade")]
        [SerializeField] private CanvasGroup _fadeObject;
        [SerializeField] private float _fadeTime;

        [Header("ButtonGroup")]
        [SerializeField] private CanvasGroup _initialButtonGroup;
        [SerializeField] private CanvasGroup _digitalTwinButtonGroup;

        protected MainMenuController _mainMenuController;

        public void SetMainMenu(MainMenuController _mainMenuController)
        {
            this._mainMenuController = _mainMenuController;
        }
        // Start is called before the first frame update
        void Start()
        {
            FadeOut();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Operation

        public void ChangeScene(string sceneName)
        {
            FadeInChangeScene(sceneName);
        }

        public void ChangeToDigitalTwin()
        {
            LeanTween.alphaCanvas(_initialButtonGroup, to: 0, 0.5f).setOnComplete(() =>
            {
                _initialButtonGroup.blocksRaycasts = false;
                _initialButtonGroup.interactable = false;

                LeanTween.alphaCanvas(_digitalTwinButtonGroup, to: 1, 0.5f).setOnComplete(() =>
                {
                    _digitalTwinButtonGroup.blocksRaycasts = true;
                    _digitalTwinButtonGroup.interactable = true;
                });
            });
        }
        #endregion

        #region Fade

        private void FadeOut()
        {
            LeanTween.alphaCanvas(_fadeObject, to: 0, _fadeTime).setOnComplete(() =>
            {
                _fadeObject.blocksRaycasts = false;
                _fadeObject.interactable = false;
            });
        }

        private void FadeInChangeScene(string sceneName)
        {
            _fadeObject.blocksRaycasts = true;
            _fadeObject.interactable = true;

            LeanTween.alphaCanvas(_fadeObject, to: 1, _fadeTime).setOnComplete(() =>
            {
                _mainMenuController.ChangeScene(sceneName);
            });
        }

        #endregion
    }
}
