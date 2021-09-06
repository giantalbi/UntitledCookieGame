using GranCook.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GranCook
{
    public class OptionsBehavior : MonoBehaviour
    {
        public GameObject backButton;

        private void Start()
        {
            if(backButton != null)
            {
                backButton.GetComponent<Button>().onClick.AddListener(LoadMenu);
            }
        }

        void LoadMenu()
        {
            SceneLoader.Instance.Load("Menu");
        }
    }
}
