using MusicShopApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShopApp.Core.Contracts
{
    public interface IOrderService
    {
        bool Create(int productId, string userId, int quantity);
        List<Order> GetOrdersByUser(string userId);
        List<Order> GetOrders();

        Order GetOrderById(int orderId);

        bool RemoveById(int orderId);

        bool Update(int orderId, int productId, string userId, int quantity);


    }
}
