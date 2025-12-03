using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;




namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {


            IPlayerService playerService = new PlayerList();
            List<Player> players = new List<Player>();
            string file = "Record.json";




            try
            {



                JsonHelper.LoadFromFile<List<Player>>(file).ForEach(player => players.Add(player));
                Console.WriteLine("File loaded successfully.");
                Logger.GetInstance().Log("File Loaded");

            }
            catch (FileNotFoundException)
            {
                Logger.GetInstance().Log("File not found, starting with empty player list");
                Console.WriteLine("File not found, starting with empty player list.");
                File.WriteAllText(file, "[]");
            }
            catch (JsonException)
            {
                Logger.GetInstance().Log("Error reading file, starting with empty player list");
                Console.WriteLine("Error reading file, starting with empty player list.");
                File.WriteAllText(file, "[]");
            }
            catch (Exception e)
            {
                Logger.GetInstance().Log("An error occurred: " + e.Message);
                Console.WriteLine("An error occurred: " + e.Message);
            }











            while (true)
            {
                Console.WriteLine("1 : Add Player");
                Console.WriteLine("2 : Search Player");
                Console.WriteLine("3 : Update Player");
                Console.WriteLine("4 : Show all Players");
                Console.WriteLine("5 : Exit");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice < 1 || choice > 5)
                    {
                        throw new OutofRange("Please enter between 1 and 5");
                    }

                    Console.Clear();
                    if (choice == 1)
                    {
                        playerService.AddPlayer(players, file);
                    }
                    else if (choice == 2)
                    {
                        playerService.SearchList(players);
                    }
                    else if (choice == 3)
                    {
                        playerService.UpdatePlayer(players, file);
                    }
                    else if (choice == 4)
                    {
                        playerService.PrintArray(players, file);
                    }
                    else if (choice == 5)
                    {
                        break;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter numerical value");
                }
                catch (OutofRange e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }







            }
        }


    }
        









        interface IPlayerService
        {
            void AddPlayer(List<Player> players, string file);
            void UpdatePlayer(List<Player> players, string file);
            void SearchList(List<Player> players);
            void PrintArray(List<Player> players, string file);
        }


        public abstract class User
        {
            public string id { get; set; }
            public string username { get; set; }
            public User(string id, string username)
            {
                this.id = id;
                this.username = username;
            }
            
            public User() { }

            public override string ToString()
            {
                return $"ID: {id}, Username: {username}";
            }






        }


        public class Player : User, IComparable<Player>
        {
            public int hoursPlayed { get; set; }
            public int highScore { get; set; }

            

            public Player(string id, string username, int highScore, int hoursPlayed)
            {
                this.id = id;
                this.username = username;
                this.hoursPlayed = hoursPlayed;
                this.highScore = highScore;

            }

            public Player(string id, string username)
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
                return base.ToString() + $", High Score: {highScore}, Hours Played: {hoursPlayed}";
            }


        }

        public class PremiumPlayer : Player
        {
            public string subscriptionType { get; set; }
            public PremiumPlayer(string id, string username, int highScore, int hoursPlayed, string subscriptionType)
                : base(id, username, highScore, hoursPlayed)
            {
                this.subscriptionType = subscriptionType;
            }
            public override string ToString()
            {
                return base.ToString() + $", Subscription Type: {subscriptionType}";
            }
        }

        public class PlayerList : IPlayerService
        {

            

            public void AddPlayer(List<Player> players, string file)
            {
                Console.WriteLine("Enter username");
                string username = Console.ReadLine();
                Console.WriteLine("Enter ID");
                string id = Console.ReadLine();
                players.Sort();
                int found = -1;
                foreach (Player p in players)
                {
                    if (p.id == id)
                    {
                        found = 1;
                        break;
                    }
                }
                if (found == 1)
                {
                    Console.WriteLine("Player with this ID already exists.");
                    return;
                }

                else if (found == -1)
                {
                    players.Add(new Player(id, username));
                    Logger.GetInstance().Log($"Player {id} added to list");
                }







                JsonHelper.SaveToFile(file, players);



            }

            public void UpdatePlayer(List<Player> players, string file)
            {
                Console.WriteLine("Enter ID of player to update");
                string id = Console.ReadLine();
                foreach (Player player in players)
                {
                    if (player.id == id)
                    {
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Enter new HighScore");
                                int highscore = Convert.ToInt32(Console.ReadLine());
                                player.highScore = highscore;
                                Console.WriteLine("Enter how many hours played");
                                int hoursplayed = Convert.ToInt32(Console.ReadLine());
                                player.hoursPlayed = hoursplayed;
                                if (hoursplayed < 0 || highscore < 0)
                                {
                                    throw new OutofRange("Values cannot be negative");
                                }
                                Logger.GetInstance().Log($"High Score and Hours Played Updatetd for player {id}");
                                break;
                            }
                            catch (FormatException e)
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input. Please enter numeric values for High Score and Hours Played.");

                                return;
                            }
                            catch (OutofRange e)
                            {
                                Console.Clear();
                                Console.WriteLine(e.Message);


                            }
                            catch (Exception e)
                            {
                                Console.Clear();
                                Console.WriteLine("An error occurred: " + e.Message);

                                
                            }
                            
                        }
                    }
                }
                    JsonHelper.SaveToFile(file, players);
                
            }

            public void SearchList(List<Player> players)
            {
                Console.WriteLine("Enter 1:id or 2:username");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice < 1 || choice > 2)
                    {
                        throw new OutofRange("Please enter 1 or 2");
                    }
                    if (choice == 1)
                    {
                        SearchByID(players);
                    }
                    else if (choice == 2)
                    {
                        SearchByUsername(players);
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter numerical value");
                    SearchList(players);
                    return;
                }
                catch (OutofRange e)
                {
                    Console.WriteLine(e.Message);
                    SearchList(players);
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    SearchList(players);
                    return;
                }


            }

            public void SearchByID(List<Player> players)
            {
                Console.WriteLine("Enter ID");
               

                    string checkID = Console.ReadLine();

                    Logger.GetInstance().Log($"Player {checkID} searched by ID");
                    SearchandSortHelper.MergeSortbyHighscoreDesc(players);
                if (!SearchandSortHelper.BinarySearchById(players, checkID))
                    {
                        Console.WriteLine("Not found");
                    }
                
               
            }

            

            public void SearchByUsername(List<Player> players)
            {
                Console.WriteLine("Enter username");
                string checkUsername = Console.ReadLine();
                Logger.GetInstance().Log($"Player {checkUsername} searched by username");
                SearchandSortHelper.MergeSortbyHighscoreDesc(players);
                if (!SearchandSortHelper.BinarySearchByUsername(players, checkUsername))
                {
                    Console.WriteLine("Not found");
                }


            }

            public void PrintArray(List<Player> players, string file)
            {
                SearchandSortHelper.MergeSortbyHighscoreDesc(players);

                foreach (Player player in players)
                {
                    if (player != null)
                    {
                        Console.WriteLine(player);


                    }
                }
            }




        }

    public static class JsonHelper
    {
        public static void SaveToFile<T>(string filePath, T data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }
        public static T LoadFromFile<T>(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found: " + e.Message);
                Logger.GetInstance().Log("File not found: " + e.Message);
                return default(T);
            }
            catch (JsonException e)
            {
                Console.WriteLine("Error deserializing JSON: " + e.Message);
                Logger.GetInstance().Log("Error deserializing JSON: " + e.Message);
                return default(T);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                Logger.GetInstance().Log("An error occurred: " + e.Message);
                return default(T);
            }
        }
    }
        public static class SearchandSortHelper
        {
            public static void MergeSortbyHighscoreDesc(List<Player> players)
            {
                if (players.Count <= 1)
                    return;
                int mid = players.Count / 2;
                List<Player> left = players.GetRange(0, mid);
                List<Player> right = players.GetRange(mid, players.Count - mid);
                MergeSortbyHighscoreDesc(left);
                MergeSortbyHighscoreDesc(right);
                int i = 0, j = 0, k = 0;
                while (i < left.Count && j < right.Count)
                {
                    if (left[i].highScore >= right[j].highScore)
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

            public static bool BinarySearchByUsername(List<Player> players, string username)
            {
                int left = 0;
                int right = players.Count - 1;
                while (left <= right)
                {
                    int mid = left + (right - left) / 2;
                    int cmp = string.Compare(players[mid].username, username, StringComparison.OrdinalIgnoreCase);
                    if (cmp == 0)
                    {
                        Console.WriteLine(players[mid]);
                        return true;
                    }
                    if (cmp < 0)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                return false;
            }

            public static bool BinarySearchById(List<Player> players, string Id)
            {
                int left = 0;
                int right = players.Count - 1;
                while (left <= right)
                {
                    int mid = left + (right - left) / 2;
                    int cmp = string.Compare(players[mid].id, Id, StringComparison.OrdinalIgnoreCase);
                    if (cmp == 0)
                    {
                        Console.WriteLine(players[mid]);
                        return true;
                    }
                    if (cmp < 0)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                return false;
            }
        }
        }

        public class Logger
        {
            private static Logger instance;
            private static string logFile = "log.txt";
            private Logger() { }
            public static Logger GetInstance()
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
            public void Log(string message)
            {
                string entry = ($"Log: {message},{DateTime.Now.ToString("HH,mm,ss")}");

                File.AppendAllText(logFile, entry + " \n");

            }

        }

        public class OutofRange : Exception
        {
            public OutofRange(string message) : base(message)
            {
            }
        }
    



    


