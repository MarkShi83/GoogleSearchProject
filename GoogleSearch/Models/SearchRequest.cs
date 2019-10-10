namespace GoogleSearch.Models
{
    /// <summary>
    /// The search request.
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the base address.
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public ResultType Type { get; set; }

        /// <summary>
        /// Gets or sets the target url.
        /// </summary>
        public string TargetUrl { get; set; }
    }
}
