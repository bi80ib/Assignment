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

                if (File.Exists("Record.json"))
                {
                    string json = File.ReadAllText(file);
                    players = JsonSerializer.Deserialize<List<Player>>(json);
                    Console.WriteLine("File loaded successfully.");

                }
            }
            catch(Exception e)
            {
                players = new List<Player>();
            }
               
            
            







            int repeat = 0;
            while (repeat == 0)
            {
                Console.WriteLine("1 : Add Player");
                Console.WriteLine("2 : Search Player");
                Console.WriteLine("3 : Update Player");
                Console.WriteLine("4 : Show all Players");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    a1.AddPlayer(players,file);
                }
                else if (choice == 2)
                {
                    a1.SearchList(players);
                }
                else if( choice == 3)
                {
                    a1.UpdatePlayer(players, file);
                }
                else if (choice == 4)
                {
                    a1.PrintArray(players, file);
                }

            }




             


        }

        static void MergeSort(List<Player> players)
        {
            if (players.Count <= 1)
                return;
            int mid = players.Count / 2;
            List<Player> left = players.GetRange(0, mid);
            List<Player> right = players.GetRange(mid, players.Count - mid);
            MergeSort(left);
            MergeSort(right);
            Merge(players, left, right);
        }

        static void Merge(List<Player> players, List<Player> left, List<Player> right)
        {
            int i = 0, j = 0, k = 0;
            while (i < left.Count && j > right.Count)
            {
                if (left[i].highScore.CompareTo(right[j].highScore) <= 0)
                {
                    players[k++] = left[i++];
                }
                else
                {
                    players[k++] = right[j++];
                }
            }
            while (i < left.Count)
            {
                players[k++] = left[i++];
            }
            while (j < right.Count)
            {
                players[k++] = right[j++];
            }
        }









        class Player : IComparable<Player>
        {
            public int hoursPlayed { get; set; }
            public int highScore { get; set; }

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

            public Player() { }
                
            public int CompareTo(Player other)
            {
                return this.id.CompareTo(other.id);
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
                Console.WriteLine("Enter ID");
                string id = Console.ReadLine();
                players.Sort();
                int found = -1;
                foreach (Player p in players) {
                    if (p.id == id)
                    {
                        found = 1;
                        break;
                    }
                }
                if(found == 1)
                    {
                        Console.WriteLine("Player with this ID already exists.");
                        return;
                    }
                
                else if (found == -1)
                {
                    players.Add(new Player(id, username));
                }





                   

                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(players, options);
                File.WriteAllText(file, json);



            }

            public void UpdatePlayer(List<Player> players, string file)
            {
                Console.WriteLine("Enter ID of player to update");
                string id = Console.ReadLine();
                foreach (Player player in players)
                {
                    if (player.id == id)
                    {
                        Console.WriteLine("Enter new HighScore");
                        int highscore = Convert.ToInt32(Console.ReadLine());
                        player.highScore = highscore;
                        Console.WriteLine("Enter how many hours played");
                        int hoursplayed = Convert.ToInt32(Console.ReadLine());
                        player.hoursPlayed = hoursplayed;
                        break;
                    }
                }
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(players, options);
                File.WriteAllText(file, json);
            }

            public void SearchList(List<Player> players)
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
                MergeSort(players);

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
