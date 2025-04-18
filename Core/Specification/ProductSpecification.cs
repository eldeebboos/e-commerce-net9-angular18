using System;
using Core.Entities;
using Core.Specifications;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParams specParams) : base(x =>
    (string.IsNullOrEmpty(specParams.Search)
            || x.Name.ToLower().Contains(specParams.Search.ToLower())) &&
    (specParams.Brands.Count() == 0 || specParams.Brands.Contains(x.Brand)) &&
    (specParams.Types.Count() == 0 || specParams.Types.Contains(x.Type))
    )
    {
        ApplyPaginated(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}
