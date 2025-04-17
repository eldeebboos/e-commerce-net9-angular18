using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications;

namespace Core.Specification;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification()
    {
        ApplySelect(x => x.Brand);
        ApplyDistinct();
    }
}
