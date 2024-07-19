using BanGiay.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Models
{
    public class CartItem
    {
        public SanPham _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }

    }

    /// Giỏ hàng
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(SanPham _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.maSP == _pro.maSP);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity

                });
            }
            else
            {
                item._shopping_quantity += _quantity;

            }
        }
        public void Update_Quantity_Shopping(int id, int _quantity)
        {
            var item = items.Find(s => s._shopping_product.maSP == id);
            if (item != null)
            {
                item._shopping_quantity = _quantity;
            }
        }
    }
}