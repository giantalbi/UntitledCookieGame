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
            // Switch every control scheme to Game controls
            for(int i = 0; i < PlayerCount; i++)
            {
                Players[i].Input.SwitchCurrentActionMap(GAME_INPUT_SCHEME);
            }

            SceneLoader.Instance.Load("Main");
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
