using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DigitalTwinITB.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuCanvasController _mainMenuCanvasController;
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
            _mainMenuCanvasController.SetMainMenu(this);
        }
        #region Operation

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        #endregion
    }
}
