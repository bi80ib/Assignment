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
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    char[] delimiter = { ',' };
                    string[] result = line.Split(delimiter);
                  
                                 
                      

                    for(int i=0; i< players.Length; i++)
                    {
                    players[i] = new Player(result[0], result[1], Convert.ToInt32(result[2]), Convert.ToInt32(result[3]));
                        break;
                    }





                }
                

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

        static class FileHandler
        {
            public static void WriteToFile(string file, Player[] players)
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    foreach (Player player in players)
                    {
                        if (player != null)
                        {
                            sw.WriteLine($"{player.id}, {player.username},{player.highScore},{player.hoursPlayed}");
                        }
                    }
                    sw.Close();
                }
            }
        }
        class Player
        {
            public int hoursPlayed { get; private set; }
            public int highScore { get; private set; }

            public string id { get; set;}

            public string username { get; set; }

            public Player(string id, string username, int highScore, int hoursPlayed)
            {
                this.id = id;
                this.username = username;
                this.hoursPlayed = hoursPlayed;
                this.highScore = highScore;
               
            }

            public Player(string id,string username)
            {
                hoursPlayed = 0;
                highScore = 0;
                this.id = id;
                this.username = username;


            }

            public void UpdateHighScore()
            {
                Console.WriteLine("Enter new high score");
                highScore =  Convert.ToInt32(Console.ReadLine());


                

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

               FileHandler.WriteToFile( file, players);
                    
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
                    if (player != null && Convert.ToInt32(player.id) == checkID)
                    {
                        Console.WriteLine(player);
                        player.UpdateHighScore();
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
                        Console.WriteLine(player);
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
