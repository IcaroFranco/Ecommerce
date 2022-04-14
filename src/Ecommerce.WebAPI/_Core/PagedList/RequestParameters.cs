namespace Ecommerce.WebAPI._Core.PagedList
{
    public abstract class RequestParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public abstract bool HasParameters();
        public abstract void SetParameters(string value);
    }
}
