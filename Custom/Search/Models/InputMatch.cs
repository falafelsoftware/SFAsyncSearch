using System.Text.RegularExpressions;

namespace SitefinityWebApp.Custom.Search.Models
{
    /// <summary>
    /// Defines an object holding the search/searchInputValidation section values.
    /// 
    /// </summary>
    public class InputMatch
    {
        private string _message;
        private string _replacement;
        private Regex _regEx;

        public string Replacement
        {
            get
            {
                return _replacement;
            }
        }

        public Regex RegEx
        {
            get
            {
                return _regEx;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public InputMatch(string replacement, Regex regEx, string message)
        {
            _replacement = replacement;
            _regEx = regEx;
            _message = message;
        }
    }
}