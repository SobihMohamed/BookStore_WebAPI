namespace Application.Common
{
    public class PaginationResponse<TData>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public IReadOnlyList<TData> Data { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public PaginationResponse(int index, int size, int count, IReadOnlyList<TData> data)
        {
            PageIndex = index;
            PageSize = size;
            TotalItems = count;
            Data = data;
        }
    }
}