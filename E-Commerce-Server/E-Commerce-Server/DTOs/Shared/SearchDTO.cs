using ECom.Constants;

namespace ECom.API.DTOs.Templates
{
    /// <summary>
    /// Represents a search query with limit and offset parameters.
    /// </summary>
    public class SearchDTO
    {
        /// <summary>
        /// Gets or sets the maximum number of items to retrieve.
        /// </summary>
        public int Limit { get; set; } = DefaultValuesConsts.DEFAULT_PAGE_LIMIT;

        /// <summary>
        /// Gets or sets the number of items to skip before retrieving the results.
        /// </summary>
        public int Offset { get; set; } = DefaultValuesConsts.DEFAULT_PAGE_OFFSET;
    }
}
