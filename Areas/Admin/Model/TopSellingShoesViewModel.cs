using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanGiay.Areas.Admin.Model
{
    
        public class TopSellingShoesViewModel
        {
            public string ProductName { get; set; }
            public int QuantitySold { get; set; }
            public decimal TotalRevenue { get; set; }
        }
    
}