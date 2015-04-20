using System.Linq;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityWebApp.Custom.Search.Models
{
    public static class ModelFactory
    {
        public static SearchDocumentModel Create(IDocument document)
        {
            return new SearchDocumentModel(document.Fields);
        }
    }
}