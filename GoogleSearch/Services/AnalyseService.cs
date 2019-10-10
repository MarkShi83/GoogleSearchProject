namespace GoogleSearch.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using GoogleSearch.Models;

    /// <summary>
    /// The analyse service.
    /// This service will find the results from the response html block based on some hard coded logic.
    /// </summary>
    public class AnalyseService : IAnalyseService
    {
        /// <summary>
        /// The find ad results.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="keywords">
        /// The keywords.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<SearchResult> FindAdResults(string response, string keywords)
        {
            var searchResults = new List<SearchResult>();

            const string PatternAd = "<span class=\\\"rtDDKc VqFMTc NceN9e\\\">Ad<\\/span><span class=\\\"qzEoUe\\\">.*?<\\/span>";

            var regexAd = new Regex(PatternAd);

            var matchesAd = regexAd.Matches(response);

            for (var i = 0; i < matchesAd.Count; i++)
            {
                var match = matchesAd[i].Value;
                var startIndex = match.IndexOf("class=\"qzEoUe\">", StringComparison.Ordinal);

                var endIndex = match.LastIndexOf("</span>", StringComparison.Ordinal);

                var output = match.Substring(startIndex + 15, endIndex - startIndex - 15);

                if (output.StartsWith("www."))
                {
                    output = output.Replace("www.", "http://www.");
                }

                var uri = new Uri(output);

                var searchResult = new SearchResult
                {
                    Keywords = keywords,
                    Rank = i + 1,
                    Uri = output,
                    Type = ResultType.Ad,
                    Host = uri.Host,
                    Path = uri.AbsolutePath,
                    SearchDateTime = DateTime.Now
                };
                searchResults.Add(searchResult);
            }

            return searchResults;
        }

        /// <summary>
        /// The find normal results.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="keywords">
        /// The keywords.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<SearchResult> FindNormalResults(string response, string keywords)
        {
            var searchResults = new List<SearchResult>();

            const string PatternNormal = "<div class=\\\"kCrYT\\\"><a href=\\\"\\/url\\?q=.*?>";

            var regexNormal = new Regex(PatternNormal);

            var matchesNormal = regexNormal.Matches(response);

            for (var i = 0; i < matchesNormal.Count; i++)
            {
                var match = matchesNormal[i].Value;
                var startIndex = match.IndexOf("http", StringComparison.Ordinal);

                var endIndex = match.IndexOf("&amp;", StringComparison.Ordinal);

                var output = match.Substring(startIndex, endIndex - startIndex);
                var uri = new Uri(output);

                var searchResult = new SearchResult
                {
                    Keywords = keywords,
                    Rank = i + 1,
                    Uri = output,
                    Type = ResultType.Normal,
                    Host = uri.Host,
                    Path = uri.AbsolutePath,
                    SearchDateTime = DateTime.Now
                };
                searchResults.Add(searchResult);
            }

            return searchResults;
        }
    }
}