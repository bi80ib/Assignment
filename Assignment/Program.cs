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
            
            int repeat = 0;
            while (repeat == 0)
            {
                Console.WriteLine("1 : Add Player");
                Console.WriteLine("2 : Search Player");
                Console.WriteLine("3 : Show all Players");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    a1.AddPlayer(players);
                }
                else if (choice == 2)
                {
                    a1.SearchArray(players);
                }
                else if (choice == 3)
                {
                    a1.PrintArray(players);
                }
            }




             


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

            public void AddPlayer( Player[] players)
            {
                for (int i = 1; i < players.Length; i++)
                {
                    if (players[i] == null)
                    {
                        players[i] = new Player(i);
                        break;
                    }
                    else 
                    {
                        Console.WriteLine("Full");
                    }
                }
           
            
            }

            public void SearchArray(Player[] players)
            {
                Console.WriteLine("Enter id");
                
                int checkID = Convert.ToInt32(Console.ReadLine());
                foreach (Player player in players)
                {
                    if(player != null && player.id == checkID)
                    {
                      
                        Console.WriteLine(player.id);
                      
                    }

                    Console.WriteLine("Not found");
                   
                    
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
