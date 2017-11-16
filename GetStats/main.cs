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
            if (args.Length < 4)
            {

                Console.WriteLine("\nConnecting to Heroes Lounge via API and downloading things!");
                APIhandler.getTeamData();
                APIhandler.sortTeamPagesIntoTeams();

                do
                {
                    Console.WriteLine("\nSelect: 1) Team 2) Sloth:\n");
                    string input = Console.ReadLine();
                    if (input == "team" || input=="Team" || input=="1")
                    {
                        Console.WriteLine("\nEnter Team name:\n");
                        var teamname = Console.ReadLine();
                        getTeamStats(teamname);
                    }
                    else
                    {
                        if (input == "Sloth" || input == "sloth" || input == "2")
                        {
                            Console.WriteLine("This feature is not yet implemented.");
                        }
                    }

                    Console.WriteLine("\n\nPress x to terminate or any other key to continue.");
                }while(Console.ReadKey().Key != ConsoleKey.X);
            }
        }

        static void getTeamStats(string team)
        {
            Console.WriteLine("Getting Stats for " + team);
            List<string> IDs = getTeamID(team);
            foreach(var id in IDs)
            {
                Console.WriteLine("\n");
                Console.WriteLine(APIhandler.getTeamName(Convert.ToInt32(id)));                
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
    }    
}
