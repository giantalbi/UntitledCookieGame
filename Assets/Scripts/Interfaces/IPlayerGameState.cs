using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranCook.Interfaces
{
    public interface IPlayerGameState
    {
        int CurrentScore { get; set; }
        int MaxScore { get; set; }
        int Handicap { get; set; }
        int TimerSpeed { get; set; }
        int RoundsWon { get; set; }
        int TimeLeft { get; set; }
        int MaxTime { get; set; }

        void TimerDown();
        void OnTimeUp();

    }
}
