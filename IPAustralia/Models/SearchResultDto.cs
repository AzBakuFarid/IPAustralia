using IPAustralia.Domain;

namespace IPAustralia.Models
{
    public class SearchResultDto
    {
        public int Total { get; set; }
        public IEnumerable<Trademark> Items { get; set; }
        public SearchResultDto()
        {
            Items = new List<Trademark>();
        }
    }
}
