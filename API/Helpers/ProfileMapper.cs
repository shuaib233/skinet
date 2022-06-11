using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Product,ProductDTO>()
            .ForMember(x=>x.ProductBrand,o=>o.MapFrom(x=>x.ProductBrand.Name))
            .ForMember(x=>x.ProductType,o=>o.MapFrom(x=>x.ProductType.Name))
            .ForMember(x=>x.PictureURL,o=>o.MapFrom<ProductURLResolver>());

        }
    }
}