using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Player[] players = new Player[3];
            PlayerArray a1 = new PlayerArray();
            

        }

        class Player
        {
            public int hoursPlayed { get; set; }
            public int highScore { get; set; }

            public int id { get; set; }

            public string username { get; set; }

            public Player(int hoursPlayed, int highScore, int id, string username)
            {
                this.hoursPlayed = hoursPlayed;
                this.highScore = highScore;
                this.id = id;
                this.username = username;
            }
        }

        class PlayerArray
        {

            public void addPlayer(Player player, Player[] players)
            {
                for (int i = 0; i < players.Length; i++) 
                {
                    if(players[i] != null)
                    {
                        players[i] = player;
                    }
                    else
                    {
                        Console.WriteLine("Full");
                    }
                }



            
        }
    }
}
