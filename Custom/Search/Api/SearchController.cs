using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http;
using SitefinityWebApp.Custom.Search.Models;
using SitefinityWebApp.Custom.Search.Models.Interfaces;
using SitefinityWebApp.Custom.Widgets;
using Telerik.Microsoft.Practices.ObjectBuilder2;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;
using Telerik.Sitefinity.Services.Search.Configuration;
using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityWebApp.Custom.Search.Api
{
    public class SearchController : BaseApiController
    {
        private static List<InputMatch> _rules;
        private const string Catalogue = "Global";

        /// <summary>
        /// Returns all Regex rules for search input validation
        /// 
        /// </summary>
        protected virtual List<InputMatch> Rules
        {
            get
            {
                if (_rules == null)
                {
                    RegexOptions options = RegexOptions.Compiled;
                    _rules = new List<InputMatch>();
                    foreach (SearchValidationElement validationElement in Config.Get<SearchConfig>().SearchInputValidation)
                    {
                        bool result;
                        bool.TryParse(validationElement.Enabled, out result);
                        if (validationElement.MatchPattern != null && result)
                            _rules.Add(new InputMatch(validationElement.ReplacementString, new Regex(validationElement.MatchPattern, options), validationElement.MatchAlert));
                    }
                }
                return _rules;
            }
        }
        
        [HttpGet]
        public SearchResultModel Get(string searchTerm, int skip = 0, int take = 5)
        {
            var model = new SearchResultModel {Term = searchTerm};
            int hitCount;

            var results = new List<SearchDocumentModel>();
            
            Search(searchTerm, Catalogue, skip, take, out hitCount).ForEach(s => results.Add(Models.ModelFactory.Create(s)));
            
            model.Count = hitCount;
            model.Results = results;

            return model;
        }

        protected IEnumerable<IDocument> Search(string query, string catalogue, int skip, int take, out int hitCount)
        {
            string message;
            if (ValidateQuery(ref query, out message))
            {
                var enumerable = GetSearcher().Search(query, catalogue, skip, take, out hitCount);

                return enumerable;
            }

            hitCount = 0;
            return null;
        }

        protected ISearchService GetSearchService()
        {
            return ServiceBus.ResolveService<ISearchService>();
        }

        /// <summary>
        /// Validates the passed user input against the defined validation rules
        /// 
        /// </summary>
        /// <param name="searchQuery">user input</param><param name="message">message from the config section if input is invalid</param>
        /// <returns>
        /// the modified user input according the rules
        ///             defined in the config section and the message for the rule applied
        /// 
        /// </returns>
        protected bool ValidateQuery(ref string searchQuery, out string message)
        {
            message = "";
            foreach (var inputMatch in Rules)
            {
                if (inputMatch.RegEx != null && inputMatch.RegEx.Match(searchQuery).Success)
                {
                    message = inputMatch.Message;
                    if (string.IsNullOrEmpty(inputMatch.Replacement))
                        return false;
                    searchQuery = inputMatch.RegEx.Replace(searchQuery, inputMatch.Replacement);
                }
            }
            return true;
        }

        /// <summary>
        /// Gets a searcher class that will be used to get a result of documents.
        ///             Override this method if you want to use a custom Searcher class.
        /// 
        /// </summary>
        /// 
        /// <returns/>
        protected ISearcher GetSearcher()
        {
            var service = GetSearchService();
            
            if (Config.Get<SearchConfig>().Pre51Compatible)
                return new Pre51Searcher(service);
            return new NewSearcher(service);
        }
    }
}