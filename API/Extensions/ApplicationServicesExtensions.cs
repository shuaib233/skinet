using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        
        public static IServiceCollection AppApplicationServices(this IServiceCollection services)
        {
            
               services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
            
             services.Configure<ApiBehaviorOptions>(options=>
            {
            options.InvalidModelStateResponseFactory = ActionContext => 
            {
                var errors = ActionContext.ModelState
                             .Where(x=>x.Value.Errors.Count > 0)
                             .SelectMany(x=>x.Value.Errors)
                             .Select(x=>x.ErrorMessage).ToArray();
                var errorResponse = new APIValidationErrorRespone()
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            };
        });
         
         return services;
        }


    }
}