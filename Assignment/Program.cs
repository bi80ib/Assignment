using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;




namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Player> players = new List<Player>();
            PlayerList a1 = new PlayerList();
            string file = "Record.json";





            try
            {
                if (File.Exists(file))
                {
                    string json = File.ReadAllText(file);
                    players = JsonSerializer.Deserialize<List<Player>>(json);
                   
                }
               
            }
            catch (Exception ex)
            {
               
                players = new List<Player>();
            }


            foreach (Player p in players)
            {
                Console.WriteLine(p);
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

            public Player(string username)
            {
                hoursPlayed = 0;
                highScore = 0;
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

         class PlayerList
        {


            public void AddPlayer(List<Player> players, string file)
            {
               Console.WriteLine("Enter username");
                string username = Console.ReadLine();

                players.Add(new Player(username));

                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(players, options);
                File.WriteAllText(file, json);



            }

            public void SearchArray(List<Player> players)
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

            public void SearchByID(List<Player> players)
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

            public void SearchByUsername(List<Player> players)
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

            public void PrintArray(List<Player> players, string file)
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
