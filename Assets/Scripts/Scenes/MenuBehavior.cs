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
        public GameObject multiPlayerButton;

        public void LoadMultiPlayer()
        {
            SceneLoader.Instance.Load("MultiplayerCharacterSelect");
        }

        private void Start()
        {
            if(multiPlayerButton != null)
            {
                multiPlayerButton.GetComponent<Button>().onClick.AddListener(LoadMultiPlayer);
            }
        }
    }
}
