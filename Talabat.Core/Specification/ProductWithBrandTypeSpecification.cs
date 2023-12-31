using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;

namespace TalabatCore.Specification
{
    public class ProductWithBrandTypeSpecification:BaseSpecification<Product>
    {
        // the main task of it to assign fields of BaseSepcifiaction
        public ProductWithBrandTypeSpecification(SpecificationParams specificationParams) :base(
            p=>(string.IsNullOrEmpty(specificationParams.Search)||p.Name.ToLower().Contains(specificationParams.Search))
            &&(!specificationParams.BrandId.HasValue|| p.ProductBrandId== specificationParams.BrandId.Value)
            &&(!specificationParams.TypeId.HasValue|| p.ProductTypeId== specificationParams.TypeId.Value)
            )
        //base here is the BaseSpecification Constructor which take >>>>> Criteria <<<<<
        {
            Includes.Add(p => p.ProductBrand);//Includes is List
            Includes.Add(p => p.ProductType);         //skip                                  ,      //take
            ApplyPagination(specificationParams.PageSize * (specificationParams.PageIndex - 1),specificationParams.PageSize);
            if (!string.IsNullOrEmpty(specificationParams.sort))
            {
                switch(specificationParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDec":
                        AddOrderByDec(p=>p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }
        public ProductWithBrandTypeSpecification(int id):base(p=>p.Id==id) //baseSpecification(Func)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }

        
    }
}
