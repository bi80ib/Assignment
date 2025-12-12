using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    interface IPlayerService
    {
        void AddPlayer(List<Player> players, string file);
        void UpdatePlayer(List<Player> players, string file);
        void SearchList(List<Player> players);
        void PrintList(List<Player> players);
    }
}
