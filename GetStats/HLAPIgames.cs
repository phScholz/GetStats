// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using HLAPIgames;
//
//    var data = HLAPIgames.FromJson(jsonString);
//
namespace HLAPIgames
{
    
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class HLAPIgames
    {
        [J("Duration")] public string Duration { get; set; }
        [J("Players")] public List<Player> Players { get; set; }
        [J("Team One")] public string TeamOne { get; set; }
        [J("TeamOneFirstBan")] public string TeamOneFirstBan { get; set; }
        [J("TeamOneLevel")] public long TeamOneLevel { get; set; }
        [J("TeamOneSecondBan")] public string TeamOneSecondBan { get; set; }
        [J("Team Two")] public string TeamTwo { get; set; }
        [J("TeamTwoFirstBan")] public string TeamTwoFirstBan { get; set; }
        [J("TeamTwoLevel")] public long TeamTwoLevel { get; set; }
        [J("TeamTwoSecondBan")] public string TeamTwoSecondBan { get; set; }
        [J("Winner")] public string Winner { get; set; }
    }

    public partial class Player
    {
        [J("Assists")] public long Assists { get; set; }
        [J("Damage Taken")] public long DamageTaken { get; set; }
        [J("Deaths")] public long Deaths { get; set; }
        [J("Draft Position")] public long DraftPosition { get; set; }
        [J("Experience Contribution")] public long ExperienceContribution { get; set; }
        [J("Healing")] public long Healing { get; set; }
        [J("Hero")] public string Hero { get; set; }
        [J("Hero Damage")] public long HeroDamage { get; set; }
        [J("Kills")] public long Kills { get; set; }
        [J("Name")] public string Name { get; set; }
        [J("Siege Damage")] public long SiegeDamage { get; set; }
        [J("Team")] public string Team { get; set; }
    }

    public partial class HLAPIgames
    {
        public static List<HLAPIgames> FromJson(string json) => JsonConvert.DeserializeObject<List<HLAPIgames>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<HLAPIgames> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
