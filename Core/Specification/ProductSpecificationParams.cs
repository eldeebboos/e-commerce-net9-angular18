namespace Core.Specification;

public class ProductSpecificationParams
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;
    private int _pageSize=6;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
    }

    private List<string> _brands = [];
    public List<string> Brands
    {
        get => _brands;
        //getting the brands from the query params and splitting it to list
        set => _brands = value.SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList();
    }
    private List<string> _types = [];
    public List<string> Types
    {
        get => _types;
        //getting the types from the query params and splitting it to list
        set => _types = value.SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList();
    }

    public string? Sort { get; set; }

    private string? _search;
    public string Search
    {
        get { return _search ?? ""; }
        set { _search = value.ToLower(); }
    }

}
