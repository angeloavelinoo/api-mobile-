namespace api_mobile.DTOs
{
    public class PaginationDTO(int totalItems, int page, int pageSize)
    {
        public int TotalItems { get; set; } = totalItems;
        public int Page { get; set; } = page;
        public int PageSize { get; set; } = pageSize;
        public int TotalPages { get { return TotalItems / PageSize == 0 ? 1 : (int)Math.Ceiling((double)totalItems / pageSize); } }
        public int StartPage { get { return Pagination.START_PAGE_DEFAULT; } }
        public int EndPage { get { return TotalPages; } }
        public bool HasPreviousPage { get { return Page > StartPage; } }
        public bool HasNextPage { get { return Page < TotalPages; } }
        public IEnumerable<int> Pages { get { return Enumerable.Range(StartPage, TotalPages); } }
    }
}
