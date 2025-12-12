using Assignment;
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

            var loadedPlayers = JsonHelper.LoadFromFile<List<Player>>(file);
                if (loadedPlayers != null)
            {
                loadedPlayers.ForEach
                (player => players.Add(player));
                Console.WriteLine("File loaded successfully.");
                Logger.GetInstance().Log("File Loaded");

            }
            else {

                Console.WriteLine("No existing data found. Starting with an empty player list.");
                Logger.GetInstance().Log("No existing data found. Starting with an empty player list.");

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
                        playerService.PrintList(players);
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
}


       

        
    


        
       
    



    


