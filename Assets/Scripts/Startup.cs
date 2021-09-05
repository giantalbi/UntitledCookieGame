using GranCook.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GranCook
{
    public class Startup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            GameManager.Instance = new GameManager();

            SceneLoader.Instance.Load("Menu");
        }
    }
}
