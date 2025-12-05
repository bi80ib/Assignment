using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public abstract class User : IComparable<User>
    {
        public string id { get; set; }
        public string username { get; set; }
        public User(string id, string username)
        {
            this.id = id;
            this.username = username;
        }

        public User() { }

        public int CompareTo(User other)
        {
            return this.id.CompareTo(other.id);
        }

        public override string ToString()
        {
            return $"ID: {id}, Username: {username}";
        }
    }

        public class Player : User
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
}
