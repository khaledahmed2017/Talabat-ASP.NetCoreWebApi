using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;

namespace TalabatCore.Specification
{
    public class ProductWithFilterForCountSpecification:BaseSpecification<Product>
        
    {
        public ProductWithFilterForCountSpecification(SpecificationParams specificationParams):base(
            p => 
               (string.IsNullOrEmpty(specificationParams.Search) || p.Name.ToLower().Contains(specificationParams.Search))
            && (!specificationParams.BrandId.HasValue || p.ProductBrandId == specificationParams.BrandId.Value)
            &&(!specificationParams.TypeId.HasValue || p.ProductTypeId == specificationParams.TypeId.Value)
            )
        {
            
        }
    }
}
