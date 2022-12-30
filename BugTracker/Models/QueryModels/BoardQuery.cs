namespace BugTracker.Models.QueryModels
{
    public class BoardQuery
    {
        public string? SearchPhrase { get; set; } = null;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortOrder { get; set; }
    }
}