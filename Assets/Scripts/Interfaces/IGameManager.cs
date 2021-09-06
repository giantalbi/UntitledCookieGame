using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranCook.Interfaces
{
    public interface IGameManager
    {
        IPlayer[] Players { get; set; }
        int PlayerCount { get; set; }

        void RoundWin(IPlayer winner);
        void RoundLose(IPlayer loser);

        void GameStart();
        void GameOver(IPlayer winner);
        void GameReset();

        void Pause();

        void SaveSettings();
        void LoadSettings();
    }
}
