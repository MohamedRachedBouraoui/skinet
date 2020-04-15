namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public string SortBy { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        private string _searchBy;
        public string SearchBy
        {
            get { return _searchBy; }
            set { _searchBy = value.ToLower(); }
        }


    }
}