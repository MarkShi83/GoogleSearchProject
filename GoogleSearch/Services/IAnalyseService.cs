namespace GoogleSearch.Services
{
    using System.Collections.Generic;

    using GoogleSearch.Models;

    /// <summary>
    /// The AnalyseService interface.
    /// </summary>
    public interface IAnalyseService
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
        IEnumerable<SearchResult> FindAdResults(string response, string keywords);

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
        IEnumerable<SearchResult> FindNormalResults(string response, string keywords);
    }
}
