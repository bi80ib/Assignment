using System;
using System.Collections.Generic;
using System.Deployment.Internal;
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
            Player p1 = new Player(2);
            a1.addPlayer(p1, players);
            a1.PrintArray(players);
            


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

            public Player(int id)
            {
                hoursPlayed = 0;
                highScore = 0;
                this.id = id;
                username = "";


            }
        }

        class PlayerArray
        {

            public void addPlayer(Player player, Player[] players)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i] == null)
                    {
                        players[i] = player;
                        break;
                    }
                    else 
                    {
                        Console.WriteLine("Full");
                    }
                }
           
            
            }

            public void PrintArray(Player[] players)
            {
                foreach (Player player in players)
                {
                    if (player != null)
                    {
                        Console.WriteLine(player.id);
                    }
                }
            }


        }
    }
}
