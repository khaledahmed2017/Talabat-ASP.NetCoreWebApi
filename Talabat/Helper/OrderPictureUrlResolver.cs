using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.DTOs;
using TalabatCore.Entities.OrderAggregate;

namespace Talabat.Helper
{
    public class OrderPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrder.PictureUrl))
                return $"{configuration["BaseApiUrl"]}{source.ProductItemOrder.PictureUrl}";
            return null;
        }
    }
}
