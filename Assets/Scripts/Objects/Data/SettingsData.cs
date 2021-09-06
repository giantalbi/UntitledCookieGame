using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GranCook.Objects.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Gamestate/SettingsData", order = 1)]
    public class SettingsData : ScriptableObject
    {
        public bool startFullScreen = true;
        public int resWidth = 1920;
        public int resHeight = 1080;

        public PlayerProfileData[] playerProfiles;
    }
}
