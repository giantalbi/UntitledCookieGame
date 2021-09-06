using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GranCook
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class MultiplayerCharacterSelectBehavior : MonoBehaviour
    {
        GameObject uiRoot;
        AudioSource audio;

        public void OnPlayerJoined(PlayerInput playerInput)
        {
            Player newPlayer = new Player(GameManager.Instance.PlayerCount, playerInput);
            PlayerController newPlayerController = playerInput.gameObject.GetComponent<PlayerController>();
            newPlayerController.Player = newPlayer;

            GameManager.Instance.Players[GameManager.Instance.PlayerCount] = newPlayer;
            GameManager.Instance.PlayerCount++;

            DontDestroyOnLoad(playerInput.gameObject);

            SwitchUI(newPlayer.PlayerIndex);
            audio.Play();

            Debug.Log(string.Format("[Player {0}] joined", GameManager.Instance.PlayerCount));
        }

        public void OnPlayerLeft(PlayerInput playerInput)
        {
            Debug.Log(playerInput);
        }

        void SwitchUI(int playerIndex)
        {
            var rootCanvas = uiRoot.transform.Find("Player" + (playerIndex + 1));
            var label = rootCanvas.Find("Label").GetComponent<TMPro.TextMeshProUGUI>();

            label.text = $"Player {playerIndex + 1} joined in!";
        } 

        void Start()
        {
            uiRoot = GameObject.Find("UI");
            audio = GetComponent<AudioSource>();
        }

        void Update()
        {
        
        }
    }
}
