using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GranCook.Utilities
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerInputManagerDispatcher : MonoBehaviour
    {
        public void OnPlayerJoined(PlayerInput playerInput)
        {
            Player newPlayer = new Player(GameManager.Instance.PlayerCount, playerInput);
            PlayerController newPlayerController = playerInput.gameObject.GetComponent<PlayerController>();
            newPlayerController.Player = newPlayer;

            GameManager.Instance.Players[GameManager.Instance.PlayerCount] = newPlayer;
            GameManager.Instance.PlayerCount++;

            DontDestroyOnLoad(playerInput.gameObject);

            Debug.Log(string.Format("[Player {0}] joined", GameManager.Instance.PlayerCount));
        }

        public void OnPlayerLeft(PlayerInput playerInput)
        {
            Debug.Log(playerInput);
        }
    }
}
