using GranCook.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GranCook.Scenes
{
    public class IntroBehavior : MonoBehaviour
    {
        public void OnStart()
        {
            GetComponent<AudioSource>().Play();

            SceneLoader.Instance.Load("Menu");
        }
    }
}
