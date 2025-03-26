﻿using MusicShopApp.Data;
using MusicShopApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicShopApp.Core.Contracts;

namespace MusicShopApp.Core.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Brand GetBrandById(int brandId) 
        {
            return _context.Brands.Find(brandId);

        }
        public List<Brand> GetBrands()
        {
            List<Brand> brands = _context.Brands.ToList();
            return brands;
        }
        public List<Product> GetProductsByBrand(int brandId) 
        {
            return _context.Products
                .Where(x => x.BrandId == brandId)
                .ToList();
        }
    }
    

}
