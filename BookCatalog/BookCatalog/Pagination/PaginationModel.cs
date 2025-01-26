namespace BookCatalog.Pagination
{
    // adding pagination model
    public class PaginationModel
    {
        public int PaginationId { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int MaxPageSize { get; set; } = 3;

        public int RealPageSize
        {
            get => PageSize;
            set => PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        } 
    }
}
