using System.Collections.Generic;
using SitefinityWebApp.Custom.Search.Models.Interfaces;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityWebApp.Custom.Search.Models
{
    /// <summary>
    /// The base class for classes performing searches.
    /// 
    /// </summary>
    public abstract class Searcher : ISearcher
    {
        /// <summary>
        /// The search service.
        /// 
        /// </summary>
        protected ISearchService Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SitefinityWebApp.Custom.Search.Models.Searcher"/> class.
        /// 
        /// </summary>
        /// <param name="service">The search service.</param>
        public Searcher(ISearchService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Returns a collection of documents using the given parameters.
        /// 
        /// </summary>
        /// <param name="query">The search query.</param><param name="catalogue">The search catalogue.</param><param name="skip">Items to skip.</param><param name="take">Items to take.</param><param name="hitCount">Number of result documents.</param>
        /// <returns/>
        public abstract IEnumerable<IDocument> Search(string query, string catalogue, int skip, int take, out int hitCount);
    }

    /// <summary>
    /// Performs a backward compatible search.
    /// 
    /// </summary>
    public class Pre51Searcher : Searcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SitefinityWebApp.Custom.Search.Models.Pre51Searcher"/> class.
        /// 
        /// </summary>
        /// <param name="service">The search service.</param>
        public Pre51Searcher(ISearchService service)
            : base(service)
        {
        }

        /// <summary>
        /// Returns a collection of documents using the given parameters.
        /// 
        /// </summary>
        /// <param name="query">The search query.</param><param name="catalogue">The search catalogue.</param><param name="skip">Items to skip.</param><param name="take">Items to take.</param><param name="hitCount">Number of result documents.</param>
        /// <returns/>
        public override IEnumerable<IDocument> Search(string query, string catalogue, int skip, int take, out int hitCount)
        {
            var resultSet = Service.Search(catalogue, query, null, skip, take, null);
            hitCount = resultSet.HitCount;
            return resultSet;
        }
    }

    /// <summary>
    /// Performs a search using the new functionality of the search service.
    /// 
    /// </summary>
    public class NewSearcher : Searcher
    {
        public string[] SearchFields { get; set; }
        public string[] HighlightedFields { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SitefinityWebApp.Custom.Search.Models.NewSearcher"/> class.
        /// 
        /// </summary>
        /// <param name="service">The search service.</param>
        public NewSearcher(ISearchService service)
            : base(service)
        {
            SearchFields = new string[2]
            {
                "Title",
                "Content"
            };
            
            HighlightedFields = new string[2]
            {
                "Title",
                "Content"
            };
        }

        /// <summary>
        /// Builds a formatted query from the passed <paramref name="searchQuery"/> using the search service.
        ///             The result query will be used by the search service to perform a search.
        ///             Override this method if you want to build a custom query.
        /// 
        /// </summary>
        /// <param name="searchQuery">The search query.</param><param name="service">The service.</param>
        /// <returns/>
        protected string BuildSearchQuery(string searchQuery, ISearchService service)
        {
            return service.BuildQuery(searchQuery, SearchFields, SystemManager.CurrentContext.AppSettings.Multilingual);
        }

        /// <summary>
        /// Returns a collection of documents using the given parameters.
        /// 
        /// </summary>
        /// <param name="query">The search query.</param><param name="catalogue">The search catalogue.</param><param name="skip">Items to skip.</param><param name="take">Items to take.</param><param name="hitCount">Number of result documents.</param>
        /// <returns/>
        public override IEnumerable<IDocument> Search(string query, string catalogue, int skip, int take, out int hitCount)
        {
            var query1 = BuildSearchQuery(query, Service);
            var resultSet = Service.Search(catalogue, query1, HighlightedFields, skip, take, null);
            hitCount = resultSet.HitCount;
            return resultSet.SetContentLinks();
        }
    }
}