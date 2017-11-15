namespace HLAPIteams
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class HLAPIteams
    {
        [J("current_page")] public long CurrentPage { get; set; }
        [J("data")] public List<Team> Teams { get; set; }
        [J("from")] public long From { get; set; }
        [J("last_page")] public long LastPage { get; set; }
        [J("next_page_url")] public string NextPageUrl { get; set; }
        [J("per_page")] public long PerPage { get; set; }
        [J("prev_page_url")] public object PrevPageUrl { get; set; }
        [J("to")] public long To { get; set; }
        [J("total")] public long Total { get; set; }       
    }

    public partial class Team
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

    public partial class HLAPIteams
    {
       public static HLAPIteams FromJson(string json) => JsonConvert.DeserializeObject<HLAPIteams>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this HLAPIteams self) => JsonConvert.SerializeObject(self, Converter.Settings);
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