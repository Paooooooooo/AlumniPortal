using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AlumniPortal.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString ConvertUrlsToLinks(this HtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new HtmlString(text);

            // Updated regex to match URLs with or without http/https
            string pattern = @"((http|https):\/\/)?(www\.[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3})([a-zA-Z0-9\-\._~:/?#[\]@!$&'()*+,;=]*)";
            string replacement = "<a href=\"http://$3$4\" target=\"_blank\">$3$4</a>";

            string result = Regex.Replace(text, pattern, replacement);

            return new HtmlString(result);
        }
    }
}