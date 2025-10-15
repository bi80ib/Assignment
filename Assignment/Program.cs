using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

    


namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Player[] players = new Player[3];
            PlayerArray a1 = new PlayerArray();
            string file = "Record.txt";


            

            if (File.Exists(file))
            {
                Console.WriteLine("File exists");
            }

            using (StreamWriter sw = new StreamWriter(file))
            {
                
                sw.WriteLine("Hello");
                sw.Close();
            }



            int repeat = 0;
            while (repeat == 0)
            {
                Console.WriteLine("1 : Add Player");
                Console.WriteLine("2 : Search Player");
                Console.WriteLine("3 : Show all Players");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    a1.AddPlayer(players,file);
                }
                else if (choice == 2)
                {
                    a1.SearchArray(players);
                }
                else if (choice == 3)
                {
                    a1.PrintArray(players, file);
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

            public Player(int id,string username)
            {
                hoursPlayed = 0;
                highScore = 0;
                this.id = id;
                this.username = username;


            }

            public override string ToString()
            {
                return $"ID: {id}, Username: {username}, Hours Played: {hoursPlayed}, High Score: {highScore}";
            }
        }

        class PlayerArray
        {

            public void AddPlayer( Player[] players, string file)
            {
               Console.WriteLine("Enter username");
                string username = Console.ReadLine();

                for (int i = 1; i < players.Length; i++)
                {
                    if (players[i] == null)
                    {
                        players[i] = new Player(i,username);
                        using (StreamWriter sw = new StreamWriter(file))
                        {
                            sw.WriteLine(players[i]);
                            sw.WriteLine("Hello");
                            sw.Close();
                        }
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
                Console.WriteLine("Enter 1:id or 2:username");
                int choice = Convert.ToInt32(Console.ReadLine());
                if(choice == 1)
                {
                    SearchByID(players);
                }
                else if (choice == 2)
                {
                    SearchByUsername(players);
                }
                                                              
            }

            public void SearchByID(Player[] players)
            {
                Console.WriteLine("Enter ID");
                int checkID = Convert.ToInt32(Console.ReadLine());
                foreach (Player player in players)
                {
                    if (player != null && player.id == checkID)
                    {
                        Console.WriteLine(player.id);
                    }
                    else
                    {
                        Console.WriteLine("Not found");
                    }
                }
            }

            public void SearchByUsername(Player[] players)
            {
                Console.WriteLine("Enter username");
                string checkUsername = Console.ReadLine();
                foreach (Player player in players)
                {
                    if (player != null && player.username == checkUsername)
                    {
                        Console.WriteLine(player.username);
                    }
                    else
                    {
                        Console.WriteLine("Not found");
                    }
                }
            }

            public void PrintArray(Player[] players,string file)
            {
                foreach (Player player in players)
                {
                    if (player != null)
                    {
                        Console.WriteLine(player);
                        

                    }
                }
            }




        }
    }
}
