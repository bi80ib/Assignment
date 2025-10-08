using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {



        }

        class Player
        {
            public int hoursPlayed { get; set; }
            public int highScore { get; set; }

            public Player(int hoursPlayed, int highScore)
            {
                this.hoursPlayed = hoursPlayed;
                this.highScore = highScore;
            }
        }
    }
}
