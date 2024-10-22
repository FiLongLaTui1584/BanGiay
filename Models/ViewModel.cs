﻿using BanGiay.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanGiay.Models
{
    public class ViewModel
    {
        public IEnumerable<SanPham> SanPham { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<ThuongHieuSP> ThuongHieuSP { get; set; }
        public SanPham SanPham1 { get; set; }
        public string TenThuongHieu { get; set; }
        public Category Category1 { get; set; }
    }
}