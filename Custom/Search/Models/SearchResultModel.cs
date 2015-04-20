using System.Collections.Generic;

namespace SitefinityWebApp.Custom.Search.Models
{
    public class SearchResultModel
    {
        public string Term { get; set; }
        public int Count { get; set; }
        public IEnumerable<SearchDocumentModel> Results { get; set; } 
    }
}