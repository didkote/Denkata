using MusicShopApp.Core.Contracts;
using MusicShopApp.Data;
using MusicShopApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShopApp.Core.Services
{
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string name, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount, string description)
        {
            Product item = new Product
            {
                ProductName = name,
                Brand = _context.Brands.Find(brandId),
                Category = _context.Categories.Find(categoryId),
                Picture = picture,
                Description=description,
                Quantity = quantity,
                Price = price,
                Discount = discount
            };

            _context.Products.Add(item);
            return _context.SaveChanges() != 0;
        }

        public Product GetPRoductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName, string searchStringProductName)
        {
            List<Product> products = _context.Products.ToList();
            if (!String.IsNullOrEmpty(searchStringCategoryName) && !String.IsNullOrEmpty(searchStringBrandName) && !String.IsNullOrEmpty(searchStringProductName))
            {
                products = products.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())
                && x.Brand.BrandName.ToLower().Contains(searchStringBrandName.ToLower()) && x.ProductName.ToLower().Contains(searchStringProductName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringCategoryName))
            {
                products = products.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBrandName))
            {
                products = products.Where(x => x.Brand.BrandName.ToLower().Contains(searchStringBrandName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringProductName))
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(searchStringProductName.ToLower())).ToList();
            }
            return products;
        }

        public bool RemoveById(int productId)
        {
            var product = GetPRoductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            _context.Remove(product);
            return _context.SaveChanges() != 0;
        }

        public bool Update(int productId, string name, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount, string description   )
        {
            var product = GetPRoductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            product.ProductName = name;

            product.Brand = _context.Brands.Find(brandId);
            product.Category = _context.Categories.Find(categoryId);
            product.Picture = picture;
            product.Description = description;
            product.Quantity = quantity;
            product.Price = price;
            product.Discount = discount;
            _context.Update(product);
            return _context.SaveChanges() != 0;
        }


    }
}
