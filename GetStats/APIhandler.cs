using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

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
            for(page=1; page<=last_page;page++)
            {
                url = "https://www.heroeslounge.gg/api/v1/teams?page=" + Convert.ToString(page);
                data = HLAPIteams.HLAPIteams.FromJson(getJson(url));
                if (data != null) TeamsPages.Add(data);                
            }            
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
            string url = "https://www.heroeslounge.gg/api/v1/teams/" + TeamID.ToString() + "/matches";
            Debug.WriteLine(url);
            Console.WriteLine("MatchID\tRound\tTeam 1\tTeam 2\tWinner");
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

                        foreach (var match in games)
                        {
                            Console.WriteLine(entry.Id.ToString() + "\t" + Convert.ToString(entry.Round) + "\t" + match.TeamOne + "\t" + match.TeamTwo + "\t" + match.Winner);
                        }
                    }
                    catch(Exception msg)
                    {
                        Console.WriteLine("Either freewin or not played yet: " + msg);
                    }
                }
            }
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
                    Console.WriteLine(ex.Message);
                }

                return data;
            }
        }

        public static void sortTeamPagesIntoTeams()
        {
            foreach(var entry in TeamsPages)
            {
                foreach(var team in entry.Teams)
                {
                    HLTeams.Add(HLAPIteam.Converter.TeamsToTeam(team));
                }
            }
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
    }
}
