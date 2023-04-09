namespace IPAustralia.Models
{
    public class Config
    {
        public string Domain { get; set; }

        public string CountUrl { get; set; }
        public string CountFullAddress => $"{Domain}/{CountUrl}";

        public string SearchUrl { get; set; }
        public string SearchFullAddress => $"{Domain}/{SearchUrl}";

        public string SearchPostFormUrl { get; set; }
        public string SearchPostFormFullAddress => $"{Domain}/{SearchPostFormUrl}";

        public string AdvancedSearchUrl { get; set; }
        public string AdvancedSearchFullAddress => $"{Domain}/{AdvancedSearchUrl}";
    }
}
