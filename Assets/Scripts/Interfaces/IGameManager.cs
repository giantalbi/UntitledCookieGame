﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranCook.Interfaces
{
    public interface IGameManager
    {
        IPlayer[] Players { get; set; }

        void RoundWin(IPlayer winner);
        void RoundLose(IPlayer loser);

        void GameStart();
        void GameOver(IPlayer winner);

        void Pause();
    }
}