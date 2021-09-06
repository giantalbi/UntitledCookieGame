using GranCook.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GranCook.Scenes
{
    public class MenuBehavior : MonoBehaviour
    {
        public GameObject singlePlayerButton;
        public GameObject multiPlayerButton;
        public GameObject optionsButton;
        public GameObject applyButton;


        void LoadMultiPlayer()
        {
            SceneLoader.Instance.Load("MultiplayerCharacterSelect");
        }

        void LoadOptions()
        {
            SceneLoader.Instance.Load("Options");
        }

        private void Start()
        {
            if(singlePlayerButton != null)
            {

            }

            if(multiPlayerButton != null)
            {
                multiPlayerButton.GetComponent<Button>().onClick.AddListener(LoadMultiPlayer);
            }

            if (optionsButton != null)
            {
                optionsButton.GetComponent<Button>().onClick.AddListener(LoadOptions);
            }
        }
    }
}
