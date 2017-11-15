// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using GetStats;
//
//    var data = HLAPIteam.FromJson(jsonString);
//
using HLAPIteams;
namespace HLAPIteam
{

    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class HLAPIteam
    {
        [J("accepting_apps")] public long AcceptingApps { get; set; }
        [J("created_at")] public string CreatedAt { get; set; }
        [J("deleted_at")] public object DeletedAt { get; set; }
        [J("disbanded")] public long Disbanded { get; set; }
        [J("facebook_url")] public string FacebookUrl { get; set; }
        [J("id")] public long Id { get; set; }
        [J("short_description")] public string ShortDescription { get; set; }
        [J("slothrating")] public long Slothrating { get; set; }
        [J("slug")] public string Slug { get; set; }
        [J("title")] public string Title { get; set; }
        [J("twitch_url")] public string TwitchUrl { get; set; }
        [J("twitter_url")] public string TwitterUrl { get; set; }
        [J("updated_at")] public string UpdatedAt { get; set; }
        [J("website_url")] public string WebsiteUrl { get; set; }
        [J("youtube_url")] public string YoutubeUrl { get; set; }
    }

   

    public partial class HLAPIteam
    {
        public static HLAPIteam FromJson(string json) => JsonConvert.DeserializeObject<HLAPIteam>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this HLAPIteam self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static HLAPIteam TeamsToTeam(HLAPIteams.Team team)
        {
            HLAPIteam dummy = new HLAPIteam();
            dummy.Id = team.Id;
            dummy.AcceptingApps = team.AcceptingApps;
            dummy.CreatedAt = team.CreatedAt;
            dummy.DeletedAt = team.DeletedAt;
            dummy.Disbanded = team.Disbanded;
            dummy.FacebookUrl = team.FacebookUrl;
            dummy.ShortDescription = team.ShortDescription;
            dummy.Slug = team.Slug;
            dummy.Slothrating = team.Slothrating;
            dummy.Title = team.Title;
            dummy.TwitchUrl = team.TwitchUrl;
            dummy.TwitterUrl = team.TwitterUrl;
            dummy.UpdatedAt = team.UpdatedAt;
            dummy.WebsiteUrl = team.WebsiteUrl;
            dummy.YoutubeUrl = team.YoutubeUrl;
            return dummy;
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
