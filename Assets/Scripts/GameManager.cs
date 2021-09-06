using GranCook.Interfaces;
using GranCook.Objects.Data;
using GranCook.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GranCook
{
    public class GameManager : IGameManager
    {
        public const string GAME_INPUT_SCHEME = "GameBoardActions";
        public const string MENU_INPUT_SCHEME = "MenuActions";

        public static GameManager Instance;


        public IPlayer[] Players { get; set; }
        public int PlayerCount { get; set; }
        public SettingsData Settings { get; set; }

        string savePath = Application.persistentDataPath + "/settings.json";

        public GameManager()
        {
            Players = new Player[4];
            PlayerCount = 0;

            LoadSettings();

            //SaveSettings();
        }

        PlayerInput[] GetPlayerInputs()
        {
            PlayerInput[] inputs = new PlayerInput[PlayerCount];
            for(int i = 0; i < PlayerCount; i++)
            {
                inputs[i] = Players[i].Input;
            }

            return inputs;
        }

        public void GameOver(IPlayer winner)
        {
        }

        public void GameStart()
        {
            SetInputState(true);

            // Switch every control scheme to Game controls
            for(int i = 0; i < PlayerCount; i++)
            {
                Players[i].Input.SwitchCurrentActionMap(GAME_INPUT_SCHEME);
            }

            SceneLoader.Instance.Load("Main");
            SceneLoader.Instance.OnSceneLoaded += GameStarted;
        }

        public void SetInputActionMap(string mapName)
        {
            for (int i = 0; i < PlayerCount; i++)
            {
                Players[i].Input.SwitchCurrentActionMap(mapName);
            }
        }

        public void SetInputState(bool active)
        {
            for (int i = 0; i < PlayerCount; i++)
            {
                Players[i].Input.gameObject.SetActive(active);
            }
        }

        void GameStarted()
        {
            // Instantiate the gameboard layout depending on the amount of player
            GameObject gameboardLayout = Object.Instantiate(Resources.Load("Prefabs/UIs/Main/Layouts/GameBoardLayouts" + PlayerCount, typeof(GameObject))) as GameObject;

            int i = 0;
            // Setup every gameboard with their player
            foreach(Transform layout in gameboardLayout.transform)
            {
                GameBoardBehavior gameBoard = layout.GetComponent<GameBoardBehavior>();
                IPlayer player = Players[i];
                gameBoard.Player = player;

                player.Input.gameObject.GetComponent<PlayerController>().gameBoardBehavior = gameBoard;
                i++;

                //Printing gameboard
                for(int j = 0; j < 5; j++)
                {
                    string line = "";

                    for (int k = 0; k < 5; k++)
                    {
                        line += player.GameBoard.Grid[j, k].ToString();
                    }

                    Debug.Log(line);
                }
            }

            SetInputState(true);
            SceneLoader.Instance.OnSceneLoaded -= GameStarted;
        }

        public void Pause()
        {
        }

        public void RoundLose(IPlayer loser)
        {
        }

        public void RoundWin(IPlayer winner)
        {
        }

        public void GameReset()
        {
            foreach (var p in Players)
            {
                if(p != null)
                    Object.Destroy(p.Input.gameObject);
            }

            Players = new Player[4];
            PlayerCount = 0;

            SceneLoader.Instance.Load("Intro");
        }

        public void SaveSettings()
        {
            string json = JsonUtility.ToJson(Settings);
            
            using(StreamWriter sw = new StreamWriter(savePath))
            {
                sw.Write(json);
            }
        }

        public void LoadSettings()
        {
            var settings = Object.Instantiate(Resources.Load("States/SettingsData")) as SettingsData;
            string json = "";

            if (File.Exists(savePath))
            {
                using(StreamReader sr = new StreamReader(savePath))
                {
                    json = sr.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(json))
                {
                    JsonUtility.FromJsonOverwrite(json, settings);
                }
            }


            Settings = settings;
        }
    }
}
