using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityWebApp.Custom.Search.Models
{
    [Serializable]
    public class SearchDocumentModel
    {
        private readonly IEnumerable<IField> _fields;

        private readonly List<string> _mainCategories = new List<string>()
        {
            "Life",
            "Marriage",
            "Family",
            "Church",
            "Free Speech",
            "Culture & Society",
            "Education",
            "International"
        }; 

        public SearchDocumentModel(IEnumerable<IField> fields)
        {
            _fields = fields;
        }

        public Guid Id
        {
            get
            {
                var val = GetFieldValue("Id");

                return string.IsNullOrEmpty(val) ? Guid.Empty : new Guid(val);
            }
        }

        public string ContentType
        {
            get
            {
                return GetFieldValue("ContentType");
            }
        }

        public string Title
        {
            get
            {
                return GetFieldValue("Title");
            }
        }

        public string Content
        {
            get
            {
                return GetFieldValue("Content");
            }
        }

        public string Link
        {
            get
            {
                return GetFieldValue("Link");
            }
        }

        public string Summary
        {
            get
            {
                return GetFieldValue("Summary");
            }
        }

        public string[] Categories
        {
            get
            {
                string[] categories = {};
                var strCategories = GetFieldValue("Categories");
                if (!string.IsNullOrEmpty(strCategories))
                {
                    categories = strCategories.Split(',').Select(s => s.ToTitleCase().Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
                }

                return categories;
            }
        }

        public string MainCategory
        {
            get
            {
                var common = Categories.Intersect(_mainCategories).ToList();

                return common.Any() ? common.First() : string.Empty;
            }
        }

        public DateTime PublicationDate
        {
            get
            {
                var s = GetFieldValue("PublicationDate");

                DateTime result;
                DateTime.TryParse(s, out result);

                return result;
            }
        }

        private string HighLighterResult
        {
            get
            {
                return GetFieldValue("HighLighterResult");
            }
        }
        
        public string GetFieldValue(string name)
        {
            if (_fields == null) return string.Empty;

            var field = _fields.FirstOrDefault(s => s.Name == name);

            return field != null ? field.Value : string.Empty;
        }
    }
}