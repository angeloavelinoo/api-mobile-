namespace api_mobile.DTOs
{
    public class BaseDTOPagination<T>(PaginationDTO pagination, IEnumerable<T> items, string? link = null)
    {
        public PaginationDTO Pagination { get; set; } = pagination;
        public IEnumerable<T> Items { get; set; } = items;
        public string? Link { get; set; } = link;
    }
}
