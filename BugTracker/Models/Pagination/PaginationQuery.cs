using BugTracker.Models.Pagination;

namespace BugTracker.Models.Pagination
{
    public class PaginationQuery
    {
        public string? SearchPhrase { get; set; } = null;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortOrder { get; set; }
    }
}