using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment
{
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

                File.WriteAllText("Record.Json", "[]");
                return default(T);

            }
            catch (JsonException e)
            {
                Console.WriteLine("Error deserializing JSON: " + e.Message);
                Logger.GetInstance().Log("Error deserializing JSON: " + e.Message);
                File.WriteAllText("Record.Json", "[]");
                return default(T);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                Logger.GetInstance().Log("An error occurred: " + e.Message);
                File.WriteAllText("Record.Json", "[]");
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
