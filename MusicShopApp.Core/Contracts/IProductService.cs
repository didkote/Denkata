using MusicShopApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShopApp.Core.Contracts
{
    public interface IProductService
    {
        bool Create(string name, int brandId, int categoruId, string picture, int quantity, decimal price, decimal discount, string description);
        bool Update(int productId, string name, int brandId, int categoruId, string picture, int quantity, decimal price, decimal discount, string description);
        List<Product> GetProducts();
        Product GetPRoductById(int productId);
        bool RemoveById(int dogproductId);
        List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName, string searchStringProductName);
    }
}
