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
        public Player Player { get; set; }

        // Start is called before the first frame update
        void Start()
        {   
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnGameSelect()
        {

        }

        public void OnGameMovement(InputValue value)
        {
            Debug.Log(value);
        }

        public void OnGamePause()
        {
            Debug.Log("Paused");
        }

        public void OnMenuStart()
        {
            GameManager.Instance.GameStart();
        }
    }
}
