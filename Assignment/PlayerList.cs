using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
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
            while (true)
            {

                try
                {

                    Console.WriteLine("Enter 1:id or 2:username");


                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice < 1 || choice > 2)
                    {
                        throw new OutofRange("Please enter 1 or 2");
                    }
                    if (choice == 1)
                    {
                        SearchByID(players);
                        break;
                    }
                    else if (choice == 2)
                    {
                        SearchByUsername(players);
                        break;
                    }


                }



                catch (FormatException e)
                {
                    Console.WriteLine("Please enter numerical value");

                }

                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);


                }
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

        public void PrintList(List<Player> players, string file)
        {
            SearchandSortHelper.MergeSortbyHighscoreDesc(players);

            foreach (Player player in players)
            {

                Console.WriteLine(player);



            }
        }
    }

}

    



