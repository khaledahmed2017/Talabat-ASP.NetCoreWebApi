using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Talabat.Helper;
using TalabatCore.ITokenService;
using TalabatCore.OrderService;
using TalabatCore.Repositories;
using TalabatRepository.GenericRepository;
using TalabatService.Services;

namespace Talabat.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService,PaymentService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<ApiBehaviorOptions>
                (options => {
                    options.InvalidModelStateResponseFactory = (ActionContext) =>
                    {
                        var Errors = ActionContext.ModelState.Where(m => m.Value.Errors.Count() > 0).SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage).ToArray();
                        var validationErrorResponse = new ApiValidationErrorResponse()
                        {
                            Errors = Errors
                        };
                        return new BadRequestObjectResult(validationErrorResponse);
                    };
                });
            
            return services;
       
        }
    }
}
