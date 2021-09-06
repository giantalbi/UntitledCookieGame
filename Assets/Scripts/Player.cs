using GranCook.Interfaces;
using GranCook.Objects;
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
            GameBoard = new GameBoard();

            // TODO adjustable gamestate values
            GameState = new PlayerGameState(20, 0, 1, 20);
        }
    }
}
