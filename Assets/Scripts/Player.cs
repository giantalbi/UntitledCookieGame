using GranCook.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GranCook
{
    public class Player : IPlayer
    {
        public Guid PlayerId { get; set; }
        public int PlayerIndex { get; set; }
        public PlayerInput Input { get; set; }
        public IGrandma Grandma { get; set; }
        public IGameBoard GameBoard { get; set; }
        public IPlayerGameState GameState { get; set; }

        public Player(int index, PlayerInput input)
        {
            PlayerId = Guid.NewGuid();
            PlayerIndex = index;
            Input = input;
        }

        public void CheckGameBoardMatch()
        {
            throw new NotImplementedException();
        }

        public void MoveCursor(Vector2 dir)
        {
            throw new NotImplementedException();
        }
    }
}
