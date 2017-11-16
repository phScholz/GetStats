using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Konsole;

namespace GetStats
{
    public class APIhandler
    {
        public static List<HLAPIteams.HLAPIteams> TeamsPages = new List<HLAPIteams.HLAPIteams>();
        public static List<HLAPIteam.HLAPIteam> HLTeams = new List<HLAPIteam.HLAPIteam>();
        public static List<HLAPIteamMatches.HLAPIteamMatches> TeamMatches = new List<HLAPIteamMatches.HLAPIteamMatches>();

        public static void getTeamData()
        {
            int page = 1;
            string url = "https://www.heroeslounge.gg/api/v1/teams?page=" + Convert.ToString(page);
            Debug.WriteLine(url);
            
            var data = HLAPIteams.HLAPIteams.FromJson(getJson(url));            
            int last_page = Convert.ToInt32(data.LastPage);
            var bar = new ProgressBar(last_page);
            for (page=1; page<=last_page;page++)
            {
                bar.Refresh(page, "Downloading TeamData");
                url = "https://www.heroeslounge.gg/api/v1/teams?page=" + Convert.ToString(page);
                data = HLAPIteams.HLAPIteams.FromJson(getJson(url));
                if (data != null) TeamsPages.Add(data);                
            }
            bar.Refresh(last_page, "Download completed!");
        }

        public static void getTeamMatches(int TeamID)
        {
            string url = "https://www.heroeslounge.gg/api/v1/teams/"+TeamID.ToString()+"/matches";
            Debug.WriteLine(url);
            Console.WriteLine("MatchID" + "\t" + "Team 1\tScore");

            string text = null;
            if ((text = getJson(url)) == null)
            {
                    throw new Exception("Website does not excist: " + url);
            }

            var data = HLAPIteamMatches.HLAPIteamMatches.FromJson(getJson(url));

            foreach(var entry in data)
            {
                Console.WriteLine(entry.Pivot.MatchId + "\t" + getTeamName(TeamID) + "\t" + entry.Pivot.TeamScore.ToString());
                
            }
        }

        public static void getTeamGames(int TeamID)
        {
            Console.WriteLine("\n\t\t\t\t\tMatches");
            Console.WriteLine("\t\t\t\t\t*******");
            string url = "https://www.heroeslounge.gg/api/v1/teams/" + TeamID.ToString() + "/matches";
            Debug.WriteLine(url);
            var data = HLAPIteamMatches.HLAPIteamMatches.FromJson(getJson(url));
            
            foreach(var entry in data)
            {
                if (entry.WinnerId != null){
                    url = "https://www.heroeslounge.gg/api/v1/matches/" + entry.Id.ToString() + "/games";
                    string text = null;
                    try
                    {
                        if ((text = getJson(url)) == null)
                        {
                            throw new Exception("Website does not excist: " + url);
                        }
                        var games = HLAPIgames.HLAPIgames.FromJson(getJson(url));
                        int max_games = games.Count;
                        int i = 0;
                        foreach (var match in games)
                        {
                            Console.WriteLine("MatchID\t\tRound\t\tGame\t\tWinner");
                            Console.WriteLine(entry.Id.ToString() + "\t\t" + Convert.ToString(i) + "\t\t" + Convert.ToString(entry.Round) + "\t\t" +  match.Winner);
                            printDraft(match.Players);
                            Console.WriteLine();
                        }
                    }
                    catch(Exception msg)
                    {
                        //Console.WriteLine("Either freewin or not played yet!");
                    }
                }
            }
        }

        public static void printDraft(List<HLAPIgames.Player> Players)
        {
            List<HLAPIgames.Player> Draft = new List<HLAPIgames.Player>();

            for(int i=1; i<=10; i++)
            {
                foreach(var player in Players)
                {
                    if (player.DraftPosition == i)
                    {
                        Draft.Add(player);
                    }
                }
            }
            
            Console.WriteLine("\n\t\t\t\t\tDraft");
            Console.WriteLine("\t\t\t\t\t*****\n");
            Console.WriteLine("\t\t\t\t" + Draft.ElementAt(0).Team + "\t" + Draft.ElementAt(1).Team+"\n");
            Console.WriteLine("\t\t\t\t" + Draft.ElementAt(0).Hero + "(" + Draft.ElementAt(0).DraftPosition.ToString() + ")" + "\t" + Draft.ElementAt(1).Hero + "(" + Draft.ElementAt(1).DraftPosition.ToString() + ")");
            Console.WriteLine("\t\t\t\t" + Draft.ElementAt(3).Hero + "(" + Draft.ElementAt(3).DraftPosition.ToString() + ")" + "\t" + Draft.ElementAt(2).Hero + "(" + Draft.ElementAt(2).DraftPosition.ToString() + ")");
            Console.WriteLine("\t\t\t\t" + Draft.ElementAt(4).Hero + "(" + Draft.ElementAt(4).DraftPosition.ToString() + ")" + "\t" + Draft.ElementAt(5).Hero + "(" + Draft.ElementAt(5).DraftPosition.ToString() + ")");
            Console.WriteLine("\t\t\t\t" + Draft.ElementAt(7).Hero + "(" + Draft.ElementAt(7).DraftPosition.ToString() + ")" + "\t" + Draft.ElementAt(6).Hero + "(" + Draft.ElementAt(6).DraftPosition.ToString() + ")");
            Console.WriteLine("\t\t\t\t" + Draft.ElementAt(8).Hero + "(" + Draft.ElementAt(8).DraftPosition.ToString() + ")" + "\t" + Draft.ElementAt(9).Hero + "(" + Draft.ElementAt(9).DraftPosition.ToString() + ")");
            Console.WriteLine();
        }


        public static string getJson(string url)
        {
            using (WebClient client = new WebClient())
            {
                string data = null;
                try
                {
                    data = client.DownloadString(url);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                return data;
            }
        }

        public static void sortTeamPagesIntoTeams()
        {
            var bar = new ProgressBar(TeamsPages.Count);
            int i = 1;
            foreach(var entry in TeamsPages)
            {
                bar.Refresh(++i, "Sorting Team Data");                              
                foreach(var team in entry.Teams)
                {                    
                    HLTeams.Add(HLAPIteam.Converter.TeamsToTeam(team));
                }
            }
            bar.Refresh(TeamsPages.Count, "Sorting completed!");
        }
        
        public static List<string> getTeamID(string team)
        {
            List<string> IDs = new List<string>();
            foreach(var entry in HLTeams)
            {
                if(entry.Slug.Contains(team) || entry.Title.Contains(team) || entry.ShortDescription.Contains(team))
                {
                    IDs.Add(entry.Id.ToString());
                }
            }
            return IDs;
        }

        public static string getTeamName(int id)
        {
            foreach (var entry in HLTeams)
            {
                if (entry.Id==id)
                {
                    return entry.Title;
                }
            }
            return null;
        }

        public static void getSlothData()
        {

        }

        private static void collectTeamPlayerStats()
        {

        }

        public static void printTeamPlayerStats(int TeamID)
        {
            collectTeamPlayerStats();
        }

    }
}
