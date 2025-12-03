using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Assignment;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Globalization;


namespace AssignmentUnitTests
{
    [TestClass]
    public class TestFixture_PlayerList
    {
        [TestMethod]
        public void AddPlayer_id1_usernameJohn()
        {
            var players = new List<Player>();
            var playerList = new PlayerList();
            String file = "Record.json";

            Console.SetIn(new System.IO.StringReader("testuser\n1\n"));

            playerList.AddPlayer(players, file);
            Assert.AreEqual(1, players.Count);
            Assert.AreEqual("1", players[0].id);
            Assert.AreEqual("testuser", players[0].username);

        }

        [TestMethod]

        public void AddPlayerDuplicateID()
        {
            var players = new List<Player>();
            var playerList = new PlayerList();
            String file = "Record.json";
            players.Add(new Player { id = "1", username = "existinguser" });
            Console.SetIn(new System.IO.StringReader("newuser\n1\n"));
            playerList.AddPlayer(players, file);
            Assert.AreEqual(1, players.Count);
            Assert.AreEqual("1", players[0].id);
        }

        [TestMethod]

        public void UpdatePlayer_id1_usernameJohn()
        {
            var players = new List<Player>();
            var playerList = new PlayerList();
            String file = "Record.json";

            players.Add(new Player { id = "1", username = "John" });
            Console.SetIn(new System.IO.StringReader("45\n67\n"));
            playerList.UpdatePlayer(players, file);
            Assert.AreEqual("45", players[0].highScore);
            Assert.AreEqual("67", players[0].hoursPlayed);



        }

        [TestMethod]

        public void SearchList_OutofRange()
        {
            var players = new List<Player>();
            var playerList = new PlayerList();
            
            Console.SetIn(new System.IO.StringReader("5\n"));
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => playerList.SearchList(players));



        }

        
    }

    [TestClass]

    public class TestFixture_JsonHelper
    {
        [TestMethod]
        public void LoadFromFile_MalformedData_JsonException()
        {
            string file = "MalformedRecord.json";
            File.WriteAllText(file, "{ invalid json }");
            Assert.ThrowsException<JsonException>(() => JsonHelper.LoadFromFile<object>(file));

        }
    }
}
