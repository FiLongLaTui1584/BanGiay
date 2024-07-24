using BanGiay.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BanGiay.Models
{
    public class CartItem
    {
        public SanPham Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();

        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void AddProductToCart(SanPham product, int quantity)
        {
            var item = items.FirstOrDefault(p => p.Product.maSP == product.maSP);
            if (item == null)
            {
                items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public int TotalQuantity()
        {
            return items.Sum(s => s.Quantity);
        }

        public decimal TotalMoney()
        {
            return (decimal)items.Sum(s => s.Quantity * s.Product.GiaGiam);
        }

        public void UpdateQuantity(int id, int newQuantity)
        {
            var item = items.Find(s => s.Product.maSP == id);
            if (item != null)
                item.Quantity = newQuantity;
        }

        public void RemoveCartItem(int id)
        {
            items.RemoveAll(s => s.Product.maSP == id);
        }

        public void ClearCart()
        {
            items.Clear();
        }
    }
}
