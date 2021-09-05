using GranCook.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GranCook
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        public IPlayer Player { get; set; }

        void Start()
        {   
        
        }

        void Update()
        {
        
        }

        public void OnGameSelect()
        {
            PrintInputActionState("Select");

        }

        public void OnGameMovement(InputValue value)
        {
            Vector2 dir = value.Get<Vector2>();
            PrintInputActionState("Movement");

        }

        public void OnGamePause(InputValue value)
        {
            PrintInputActionState("Paused");
        }

        public void OnMenuStart(InputValue value)
        {
            PrintInputActionState("Start");
            GameManager.Instance.GameStart();
        }

        public void OnMenuDirection(InputValue value)
        {
            PrintInputActionState("Direction");

        }

        void PrintInputActionState(string action, InputValue value = null)
        {
            Debug.Log(string.Format("[Player {0}] {1}", Player.PlayerIndex + 1, action));
        }
    }
}
