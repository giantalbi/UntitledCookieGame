using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GranCook.Interfaces
{
    public interface IPlayer
    {
        public Guid PlayerId { get; set; }
        public IGrandma Grandma { get; set; }
        public IGameBoard GameBoard { get; set; }
        public IPlayerGameState GameState { get; set; }

        void MoveCursor(Vector2 dir);
        void CheckGameBoardMatch();
    }
}
