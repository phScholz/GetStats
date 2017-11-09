using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace GetStats
{
    class main
    {
        static void Main(string[] args)
        {
            if (args.Length < 4) {
                Console.WriteLine("Usage:getStats team/sloth name");
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

            Console.WriteLine("Press any key to ... you know what.");
            Console.ReadLine();               
        }

        static void getTeamStats(string team)
        {
            Console.WriteLine("Getting Stats for " + team);
        }

        static void getSlothStats(string sloth)
        {
            Console.WriteLine("Getting Stats for " + sloth);
        }

        static int getSlothID(string sloth)
        {
            return 1;
        }

        static int getTeamID(string team)
        {
            return 1;
        }

        static string getFromURL(string url)
        {
            WebClient n = new WebClient();
            return n.DownloadString(url);            
        }
    }

    
}
