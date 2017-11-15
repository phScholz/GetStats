// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using GetStats;
//
//    var data = HLAPIsloths.FromJson(jsonString);
//
namespace HLAPIsloths
{

    using System.Collections.Generic;
    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;

    public partial class HLAPIsloths
    {
        [J("current_page")] public long CurrentPage { get; set; }
        [J("data")] public List<Datum> Data { get; set; }
        [J("from")] public long From { get; set; }
        [J("last_page")] public long LastPage { get; set; }
        [J("next_page_url")] public string NextPageUrl { get; set; }
        [J("per_page")] public long PerPage { get; set; }
        [J("prev_page_url")] public object PrevPageUrl { get; set; }
        [J("to")] public long To { get; set; }
        [J("total")] public long Total { get; set; }
    }

    public partial class Datum
    {
        [J("all_mmr")] public long AllMmr { get; set; }
        [J("battle_tag")] public string BattleTag { get; set; }
        [J("birthday")] public object Birthday { get; set; }
        [J("created_at")] public string CreatedAt { get; set; }
        [J("deleted_at")] public object DeletedAt { get; set; }
        [J("discord_tag")] public string DiscordTag { get; set; }
        [J("facebook_url")] public string FacebookUrl { get; set; }
        [J("hotslogs_id")] public long? HotslogsId { get; set; }
        [J("id")] public long Id { get; set; }
        [J("is_captain")] public long IsCaptain { get; set; }
        [J("mmr")] public long Mmr { get; set; }
        [J("role_id")] public long RoleId { get; set; }
        [J("short_description")] public string ShortDescription { get; set; }
        [J("team_id")] public long TeamId { get; set; }
        [J("title")] public string Title { get; set; }
        [J("twitch_url")] public string TwitchUrl { get; set; }
        [J("twitter_url")] public string TwitterUrl { get; set; }
        [J("updated_at")] public string UpdatedAt { get; set; }
        [J("website_url")] public string WebsiteUrl { get; set; }
        [J("youtube_url")] public string YoutubeUrl { get; set; }
    }

    public partial class HLAPIsloths
    {
        public static HLAPIsloths FromJson(string json) => JsonConvert.DeserializeObject<HLAPIsloths>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this HLAPIsloths self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
