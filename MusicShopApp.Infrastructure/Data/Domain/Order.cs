﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShopApp.Infrastructure.Data.Domain
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;


        [Required]
        public string UserId { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [Range(0,100)]
        public decimal Discount { get; set; }
        public decimal TotalPrice { get { return Quantity * Price - Quantity * Price * Discount / 100; } }
    }
}
