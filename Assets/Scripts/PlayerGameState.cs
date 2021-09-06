using GranCook.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranCook
{
    public class PlayerGameState : IPlayerGameState
    {
        public int CurrentScore { get; set; }
        public int MaxScore { get; set; }
        public int Handicap { get; set; }
        public int TimerSpeed { get; set; }
        public int RoundsWon { get; set; }
        public int TimeLeft { get; set; }
        public int MaxTime { get; set; }

        public PlayerGameState(int pMaxScore, int pHandicap, int pTimerSpeed, int pMaxTime)
        {
            MaxScore = pMaxScore;
            Handicap = pHandicap;
            TimerSpeed = pTimerSpeed;
            MaxTime = pMaxTime;

            CurrentScore = Handicap;
            RoundsWon = 0;
            TimeLeft = MaxTime;
        }



        public void OnTimeUp()
        {
            throw new NotImplementedException();
        }

        public void TimerDown()
        {
            TimeLeft -= TimerSpeed;

            if(TimeLeft < 0)
            {
                OnTimeUp();
            }
        }
    }
}
