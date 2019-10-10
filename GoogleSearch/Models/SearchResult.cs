using System;

namespace GoogleSearch.Models
{
    /// <summary>
    /// The result type.
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Normal search result.
        /// </summary>
        Normal,

        /// <summary>
        /// Ad search result.
        /// </summary>
        Ad
    }

    /// <summary>
    /// The search result.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0"), Serializable(),
     System.ComponentModel.DesignerCategoryAttribute("code")]
    public class SearchResult
    {
        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public ResultType Type { get; set; }

        /// <summary>
        /// Gets or sets the search date time.
        /// </summary>
        public DateTime SearchDateTime { get; set; }
    }
}

