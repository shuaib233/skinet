using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductURLResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductURLResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember,
         ResolutionContext context)
        {
            return configuration["ApiURL"] +source.PictureURL;
        }
    }
}