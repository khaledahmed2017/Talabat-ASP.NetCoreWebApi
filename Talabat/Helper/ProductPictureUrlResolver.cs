using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.DTOs;
using TalabatCore.Entities;

namespace Talabat.Helper
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                //return $"https://https://localhost:5001/{source.PictureUrl}";
                return $"{configuration["BaseApiUrl"]}{source.PictureUrl}";
            }
            return null;
        }
    }
}
