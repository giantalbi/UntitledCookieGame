using GranCook.Interfaces;
using GranCook.Utilities;
using System.Collections;
using System.Collections.Generic;
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

        public GameManager()
        {
            Players = new Player[4];
            PlayerCount = 0;
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

            // Setup every gameboard with their player


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

    }
}
