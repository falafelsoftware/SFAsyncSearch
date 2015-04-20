using System.Collections.Generic;
using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityWebApp.Custom.Search.Models.Interfaces
{
    /// <summary>
    /// Represents an interface for performing searches.
    /// 
    /// </summary>
    public interface ISearcher
    {
        /// <summary>
        /// Returns a collection of documents using the given parameters.
        /// 
        /// </summary>
        /// <param name="query">The search query.</param><param name="catalogue">The search catalogue.</param><param name="skip">Items to skip.</param><param name="take">Items to take.</param><param name="hitCount">Number of result documents.</param>
        /// <returns/>
        IEnumerable<IDocument> Search(string query, string catalogue, int skip, int take, out int hitCount);
    }
}
