using System;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;

namespace GetStats
{
    class main
    {
        static void Main(string[] args)
        {
            if (args.Length < 4) {

                Console.WriteLine("\nConnecting to Heroes Lounge via API and downloading things!");
                APIhandler.getTeamData();
                APIhandler.sortTeamPagesIntoTeams();

                

                Console.WriteLine("\nUsage: getStats team name\n");
                string input = Console.ReadLine();
                var inputList = input.Split(null);

                

                if (inputList[1]=="team")
                {
                    getTeamStats(inputList[2]);
                }
                if (inputList[1] == "sloth")
                {
                    getSlothStats(inputList[2]);
                }
            }
            else
            {

            }

            Console.WriteLine("\n\nPress any key to ... you know what.");
            Console.ReadLine();               
        }

        static void getTeamStats(string team)
        {
            Console.WriteLine("Getting Stats for " + team);
            List<string> IDs = getTeamID(team);
            foreach(var id in IDs)
            {
                Console.WriteLine("\n");
                Console.WriteLine(APIhandler.getTeamName(Convert.ToInt32(id)));
                Console.WriteLine("\n\t\t\t\t\tMatches");
                Console.WriteLine("\t\t\t\t\t*******");
                APIhandler.getTeamGames(Convert.ToInt32(id));
            }

        }

        static void getSlothStats(string sloth)
        {
            Console.WriteLine("Getting Stats for " + sloth);
        }

        static int getSlothID(string sloth)
        {            
            return 1;
        }

        static List<String> getTeamID(string team)
        {
            var data = APIhandler.getTeamID(team);
            foreach(var i in data){
                Debug.WriteLine(i);
            }
            
            return data;
        }
        
        class Match
        {
            List<Game> games = new List<Game>();
            int enemyId { get; set; } = 0;
            int winnerId { get; set; } = 0;
            string win { get; set; } = "Win";
        }

        class Game
        {
            public string Duration { get; set; } = null;
            public List<GamePlayer> Players { get; set; } = new List<GamePlayer>();
            public string TeamOne { get; set; } = null;
            public string TeamOneFirstBan { get; set; } = null;
            public long TeamOneLevel { get; set; } = 0;
            public string TeamOneSecondBan { get; set; } = null;
            public string TeamTwo { get; set; } = null;
            public string TeamTwoFirstBan { get; set; } = null;
            public long TeamTwoLevel { get; set; } = 0;
            public string TeamTwoSecondBan { get; set; } = null;
            public string Winner { get; set; } = null;
        }

        class GamePlayer
        {
            public long Assists { get; set; } = 0;
            public long DamageTaken { get; set; } = 0;
            public long Deaths { get; set; } = 0;
            public long DraftPosition { get; set; } = 0;
            public long ExperienceContribution { get; set; } = 0;
            public long Healing { get; set; } = 0;
            public string Hero { get; set; } = null;
            public long HeroDamage { get; set; } = 0;
            public long Kills { get; set; } = 0;
            public string Name { get; set; } = null;
            public long SiegeDamage { get; set; } = 0;
            public string Team { get; set; } = null;
        }
    }    
}
