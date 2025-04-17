using System;
using Core.Entities;
using Core.Specifications;

namespace Core.Specification;

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification()
    {
        ApplySelect(x => x.Type);
        ApplyDistinct();
    }
}
