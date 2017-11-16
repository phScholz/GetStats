// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using HLAPIteamMatches;
//
//    var data = HLAPIteamMatches.FromJson(jsonString);
//
namespace HLAPIteamMatches
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
        
    public partial class HLAPIteamMatches
    {
        [J("channel_id")] public long? ChannelId { get; set; }
        [J("created_at")] public string CreatedAt { get; set; }
        [J("deleted_at")] public object DeletedAt { get; set; }
        [J("div_id")] public string DivId { get; set; }
        [J("id")] public long Id { get; set; }
        [J("is_played")] public long IsPlayed { get; set; }
        [J("pivot")] public Pivot Pivot { get; set; }
        [J("playoff_id")] public object PlayoffId { get; set; }
        [J("playoff_loser_next")] public object PlayoffLoserNext { get; set; }
        [J("playoff_position")] public object PlayoffPosition { get; set; }
        [J("playoff_winner_next")] public object PlayoffWinnerNext { get; set; }
        [J("round")] public string Round { get; set; }
        [J("schedule_date")] public string ScheduleDate { get; set; }
        [J("tbp")] public string Tbp { get; set; }
        [J("updated_at")] public string UpdatedAt { get; set; }
        [J("wbp")] public string Wbp { get; set; }
        [J("winner_id")] public long? WinnerId { get; set; }
    }

    public partial class Pivot
    {
        [J("match_id")] public long MatchId { get; set; }
        [J("team_id")] public long TeamId { get; set; }
        [J("team_score")] public long TeamScore { get; set; }
    }

    public partial class HLAPIteamMatches
    {
        public static List<HLAPIteamMatches> FromJson(string json) => JsonConvert.DeserializeObject<List<HLAPIteamMatches>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<HLAPIteamMatches> self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
